using CssDupFinder.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CssDupFinder.Test.Commands
{
    [TestClass]
    public class WriteCommandTest
    {
        [TestMethod]
        public void WhenExecute_LiteralCommandShouldWriteLine()
        {
            var writer = new StringWriter();
            var literal = new WriteCommand("Test 123...", CommandType.None, writer);

            literal.Execute();

            writer.GetStringBuilder().ToString().Should().Be("Test 123..." + writer.NewLine);
        }
    }
}