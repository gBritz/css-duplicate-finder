using CssDupFinder.Commands;
using CssDupFinder.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ShellProgress;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;

namespace CssDupFinder.Test.Commands
{
    [TestClass]
    public class DiscoveryCommandTest
    {
        [TestMethod]
        public void ContructorArgumentsShouldBeNotNullOrEmpty()
        {
            Action arg1Null = () => new DiscoveryCommand(null, "a", new MockFileSystem(), new ProgressBarFactory());
            Action arg1Empty = () => new DiscoveryCommand(String.Empty, "a", new MockFileSystem(), new ProgressBarFactory());
            Action arg2Null = () => new DiscoveryCommand(" ", null, new MockFileSystem(), new ProgressBarFactory());
            Action arg2Empty = () => new DiscoveryCommand(" ", String.Empty, new MockFileSystem(), new ProgressBarFactory());
            Action arg3Null = () => new DiscoveryCommand(" ", " ", null, new ProgressBarFactory());

            arg1Null.ShouldThrow<ArgumentNullException>().And
                .ParamName.Should().Be("topFolder");
            arg1Empty.ShouldThrow<ArgumentNullException>().And
                .ParamName.Should().Be("topFolder");

            arg2Null.ShouldThrow<ArgumentNullException>().And
                .ParamName.Should().Be("outputDiscoveryResult");
            arg2Empty.ShouldThrow<ArgumentNullException>().And
                .ParamName.Should().Be("outputDiscoveryResult");

            arg3Null.ShouldThrow<ArgumentNullException>().And
                .ParamName.Should().Be("fileSystem");
        }

        [TestMethod]
        public void WhenTopFolderNotExists_OutputDiscoveryResultShouldBeEmpty()
        {
            var mockFileSystem = new MockFileSystem();
            var outputFileResult = "c:\\tools\\css-duplicate-finder\\discovery.json";
            var instance = new DiscoveryCommand("c:\\", outputFileResult, mockFileSystem, new ProgressBarFactory());

            instance.Execute();

            mockFileSystem.AllFiles.Should().BeEmpty();
        }

        [TestMethod]
        public void OutputDiscoveryFileResultShouldBeNotEmpty()
        {
            var topFolder = "c:\\work\\projectname\\";
            var mockFileSystem = new MockFileSystem(
                new Dictionary<String, MockFileData>
                {
                    { topFolder + "css\\default.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\print.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\site.css", new MockFileData(String.Empty) }
                });
            var outputFileResult = "c:\\tools\\css-duplicate-finder\\discovery.json";
            var instance = new DiscoveryCommand(topFolder, outputFileResult, mockFileSystem, new ProgressBarFactory());

            instance.Execute();

            var discoveryFile = mockFileSystem.GetFile(outputFileResult);
            discoveryFile.Should().NotBeNull();
            discoveryFile.TextContents.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public void DiscoveryModelShouldHaveCountThreeFilesAndOneFolder()
        {
            var topFolder = "c:\\work\\projectname\\";
            var mockFileSystem = new MockFileSystem(
                new Dictionary<String, MockFileData>
                {
                    { topFolder + "css\\default.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\print.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\site.css", new MockFileData(String.Empty) }
                });
            var outputFileResult = "c:\\tools\\css-duplicate-finder\\discovery.json";
            var instance = new DiscoveryCommand(topFolder, outputFileResult, mockFileSystem, new ProgressBarFactory());

            var model = instance.DiscoveryCssFiles();

            model.Count.Should().Be(3);
            model.Folders.Should().HaveCount(1);
            model.Folders[0].Name.Should().Be(topFolder+"css");
            model.Folders[0].FileNames.ShouldBeEquivalentTo(new[]
            {
                "default.css", "print.css", "site.css"
            });
        }

        [TestMethod]
        public void DiscoveryModelShouldHaveCountFiveFilesAndTwoFolders()
        {
            var topFolder = "c:\\work\\projectname\\";
            var mockFileSystem = new MockFileSystem(
                new Dictionary<String, MockFileData>
                {
                    { topFolder + "css\\default.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\print.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\site.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\vendor\\font-awesome.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\vendor\\jquery-ui.css", new MockFileData(String.Empty) }
                });
            var outputFileResult = "c:\\tools\\css-duplicate-finder\\discovery.json";
            var instance = new DiscoveryCommand(topFolder, outputFileResult, mockFileSystem, new ProgressBarFactory());

            var model = instance.DiscoveryCssFiles();

            model.Count.Should().Be(5);
            model.Folders.Should().HaveCount(2);
            model.Folders[0].Name.Should().Be(topFolder + "css");
            model.Folders[0].FileNames.ShouldBeEquivalentTo(new[]
            {
                "default.css", "print.css", "site.css"
            });

            model.Folders[1].Name.Should().Be(topFolder + "css\\vendor");
            model.Folders[1].FileNames.ShouldBeEquivalentTo(new[]
            {
                "font-awesome.css", "jquery-ui.css"
            });
        }

        [TestMethod]
        public void ContentSerializedShouldBeEqualObjectModelDeserialized()
        {
            var topFolder = "c:\\work\\projectname\\";
            var mockFileSystem = new MockFileSystem(
                new Dictionary<String, MockFileData>
                {
                    { topFolder + "css\\default.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\print.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\site.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\vendor\\font-awesome.css", new MockFileData(String.Empty) },
                    { topFolder + "css\\vendor\\jquery-ui.css", new MockFileData(String.Empty) }
                });
            var outputFileResult = "c:\\tools\\css-duplicate-finder\\discovery.json";
            var instance = new DiscoveryCommand(topFolder, outputFileResult, mockFileSystem, new ProgressBarFactory());

            instance.Execute();

            var jsonContent = mockFileSystem.GetFile(outputFileResult).TextContents;
            jsonContent = jsonContent.Substring(1, jsonContent.Length - 1);
            var model = JsonConvert.DeserializeObject<DiscoveryModel>(jsonContent);

            model.Should().NotBeNull();
            model.ShouldBeEquivalentTo(new DiscoveryModel
            {
                Folders = new[]
                {
                    new FolderContentModel
                    {
                        Name = topFolder + "css", 
                        FileNames = new [] { "default.css", "print.css", "site.css" }
                    },
                    new FolderContentModel
                    {
                        Name = topFolder + "css\\vendor", 
                        FileNames = new [] { "font-awesome.css", "jquery-ui.css" }
                    }
                },
                Count = 5,
                CompactAllInOneFile = true
            });
        }
    }
}