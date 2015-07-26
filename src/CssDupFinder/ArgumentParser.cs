using CssDupFinder.Commands;
using CssDupFinder.Extensions;
using System;
using System.IO;

namespace CssDupFinder
{
    public class ArgumentParser
    {
        private readonly String[] args;

        public ArgumentParser(String[] arguments)
        {
            arguments.ThrowIfNull("arguments");

            this.args = arguments;
        }

        public String Folder
        {
            get { return Path.GetDirectoryName(DiscoveredFullPath); }
        }

        public String Output
        {
            get { return args[2]; }
        }

        public String DiscoveredFullPath
        {
            get { return args[1]; }
        }

        public CommandType Command
        {
            get { return (CommandType)args[0][1]; }
        }
    }
}