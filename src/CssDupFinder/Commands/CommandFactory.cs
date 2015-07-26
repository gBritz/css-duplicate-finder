using CssDupFinder.Extensions;

namespace CssDupFinder.Commands
{
    public sealed class CommandFactory
    {
        public static ICommand CreateInstanceOf(CommandType type, ArgumentParser args)
        {
            args.ThrowIfNull("args");

            switch (type)
            {
                case CommandType.Discovery:
                    return new LiteralCommand("Not implemented", CommandType.Discovery);

                case CommandType.Purge:
                    return new LiteralCommand("Not implemented", CommandType.Purge);

                case CommandType.FindDuplicates:
                    return new LiteralCommand("Not implemented", CommandType.Version);

                case CommandType.Version:
                    return new LiteralCommand("0.0.0-15-alpha (experimental version)", CommandType.Version);

                case CommandType.Help:
                    return new LiteralCommand("Help!!!", CommandType.Help);

                default:
                    return new LiteralCommand("Invalid command", CommandType.None);
            }
        }
    }
}