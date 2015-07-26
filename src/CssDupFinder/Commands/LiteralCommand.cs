using CssDupFinder.Extensions;
using System;

namespace CssDupFinder.Commands
{
    public class LiteralCommand : ICommand
    {
        private readonly String message;

        public LiteralCommand(String message, CommandType type)
        {
            message.ThrowIfNull("message");

            this.message = message;
            this.Type = type;
        }

        public CommandType Type { get; private set; }

        public void Execute()
        {
            Console.WriteLine(message);
        }
    }
}