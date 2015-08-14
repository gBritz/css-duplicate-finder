using CssDupFinder.Commands;
using CssDupFinder.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellProgress;
using System;
using System.IO;

namespace CssDupFinder.Test.Commands
{
    [TestClass]
    public class DuplicateFinderCommandTest
    {
        [TestMethod]
        public void ShouldThrowIfOutputDirectoryIsNullOrEmpty()
        {
            Action isNull = () => new DuplicateFinderCommand(null, null, new ReportGenerator(), new ProgressBarFactory());
            Action isEmpty = () => new DuplicateFinderCommand(String.Empty, null, new ReportGenerator(), new ProgressBarFactory());

            isNull.ShouldThrow<ArgumentNullException>()
                .And.ParamName.Should().Be("outputDirectory");
            isEmpty.ShouldThrow<ArgumentNullException>()
                .And.ParamName.Should().Be("outputDirectory");
        }

        [TestMethod]
        public void ShouldThrowIfOutputDirectoryNotFound()
        {
            Action notFound = () => new DuplicateFinderCommand("z:\\asdçlfjasfj\\asçldfjadçl", new FolderContentModel[0], new ReportGenerator(), new ProgressBarFactory());
            
            var expt = notFound.ShouldThrow<ArgumentException>().Which;
               
            expt.ParamName.Should().Be("outputDirectory");
            expt.InnerException.Should().BeOfType<DirectoryNotFoundException>();
        }

        [TestMethod]
        public void ShouldThrowIfCssConfigIsNull()
        {
            Action isNull = () => new DuplicateFinderCommand("c:\\", null, new ReportGenerator(), new ProgressBarFactory());

            isNull.ShouldThrow<ArgumentNullException>()
                .And.ParamName.Should().Be("folders");
        }

        [TestMethod]
        public void ShouldThrowIfReportGeneratorIsNull()
        {
            Action isNull = () => new DuplicateFinderCommand("c:\\", new FolderContentModel[0], null, new ProgressBarFactory());

            isNull.ShouldThrow<ArgumentNullException>()
                .And.ParamName.Should().Be("generator");
        }
    }
}