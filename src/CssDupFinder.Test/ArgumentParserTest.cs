using CssDupFinder.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CssDupFinder.Test
{
    [TestClass]
    public class ArgumentParserTest
    {
        private readonly String[] argsToDiscovery = new[] { "-d", "c:\\workspace-project\\web\\", "c:\\css-dup-finder\\discovery.json" };
        private readonly String[] argsToFind = new[] { "-f", "c:\\css-dup-finder\\discovery.json" };

        [TestMethod]
        public void ArgumentCommandShouldBeEqualCommandType()
        {
            var commands = Enum.GetValues(typeof(CommandType))
                .OfType<CommandType>();

            var args = new String[1];

            foreach (var cmd in commands)
            {
                args[0] = "-" + ((Char)cmd).ToString();
                var parser = new ArgumentParser(args);

                parser.Command.Should().Be(cmd);
            }
        }

        [TestMethod]
        public void ShouldThrowIfArgumentIsNull()
        {
            Action argsIsNull = () => new ArgumentParser(null);

            argsIsNull.ShouldThrow<ArgumentNullException>()
                .And.ParamName.Should().Be("arguments");
        }

        [TestMethod]
        public void DiscoveryFullPathShouldNotBeNull()
        {
            var parser = new ArgumentParser(argsToFind);
            parser.DiscoveredFullPath.Should().Be("c:\\css-dup-finder\\discovery.json");
        }

        [TestMethod]
        public void FolderShouldNotBeNull()
        {
            var parser = new ArgumentParser(argsToDiscovery);
            parser.Folder.Should().Be("c:\\workspace-project\\web");
        }

        [TestMethod]
        public void OutputShouldNotBeNull()
        {
            var parser = new ArgumentParser(argsToDiscovery);
            parser.Output.Should().Be("c:\\css-dup-finder\\discovery.json");
        }
    }
}