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

                Assert.IsTrue(parser.Command == cmd);
            }
        }

        [TestMethod]
        public void ShouldThrowIfArgumentIsNullOrEmpty()
        {
            Assert.Fail("TODO:...");
        }

        [TestMethod]
        public void DiscoveryFullPathShouldNotBeNull()
        {
            var parser = new ArgumentParser(argsToFind);
            Assert.IsTrue(parser.DiscoveredFullPath == "c:\\css-dup-finder\\discovery.json");
        }

        [TestMethod]
        public void FolderShouldNotBeNull()
        {
            var parser = new ArgumentParser(argsToDiscovery);
            Assert.IsTrue(parser.Folder == "c:\\workspace-project\\web");
        }

        [TestMethod]
        public void OutputShouldNotBeNull()
        {
            var parser = new ArgumentParser(argsToDiscovery);
            Assert.IsTrue(parser.Output == "c:\\css-dup-finder\\discovery.json");
        }
    }
}