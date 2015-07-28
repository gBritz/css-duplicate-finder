using CssDupFinder.Commands;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CssDupFinder.Test
{
    [TestClass]
    public class CommandFactoryTest
    {
        [TestMethod]
        public void AllCommandTypesShouldBeInstantiable()
        {
            var commands = Enum.GetValues(typeof(CommandType))
                .OfType<CommandType>();
            var args = ArgumentParserTest.MockInstance();

            foreach (var cmd in commands)
            {
                var instance = CommandFactory.CreateInstanceOf(cmd, args);
                    
                instance.Should().NotBeNull();
                instance.Type.Should().Be(cmd);
            }
        }

        [TestMethod]
        public void ShouldThrowIfArgumentParserIsNull()
        {
            Action method = () => CommandFactory.CreateInstanceOf(CommandType.None, null);
            method.ShouldThrow<ArgumentNullException>("arguments");
        }

        [TestMethod]
        public void WhenCommandTypeIsFindDuplicates_InstanceShouldBeDuplicateFinderCommand()
        {
            var args = ArgumentParserTest.MockInstance();
            CommandFactory.CreateInstanceOf(CommandType.FindDuplicates, args)
                .Should().BeOfType<DuplicateFinderCommand>();
        }
    }
}