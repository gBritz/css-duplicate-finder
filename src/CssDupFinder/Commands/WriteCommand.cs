using CssDupFinder.Extensions;
using System;
using System.IO;

namespace CssDupFinder.Commands
{
    public class WriteCommand : ICommand
    {
        private readonly String message;
        private readonly TextWriter writer;

        public WriteCommand(String message, CommandType type, TextWriter writer)
        {
            message.ThrowIfNull("message");
            writer.ThrowIfNull("writer");

            this.message = message;
            this.Type = type;
            this.writer = writer;
        }

        public CommandType Type { get; private set; }

        public void Execute()
        {
            writer.WriteLine(message);
        }
    }
}