using CssDupFinder.Extensions;
using System;
using System.IO.Abstractions;

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
                    return new DiscoveryCommand(args.Folder, args.Output, new FileSystem());

                case CommandType.Purge:
                    return new WriteCommand("Not implemented", CommandType.Purge, Console.Out);

                case CommandType.FindDuplicates:
                    return new DuplicateFinderCommand(args.Folder, args.Config.Folders, new ReportGenerator());

                case CommandType.Version:
                    return new WriteCommand("0.0.0-15-alpha (experimental version)", CommandType.Version, Console.Out);

                case CommandType.Help:
                    return new WriteCommand("Help!!!", CommandType.Help, Console.Out);

                default:
                    return new WriteCommand("Invalid command", CommandType.None, Console.Out);
            }
        }
    }
}