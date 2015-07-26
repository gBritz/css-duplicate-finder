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

        public virtual String Folder
        {
            get { return Path.GetDirectoryName(DiscoveredFullPath); }
        }

        public virtual String Output
        {
            get { return args[2]; }
        }

        public virtual String DiscoveredFullPath
        {
            get { return args[1]; }
        }

        public virtual CommandType Command
        {
            get { return (CommandType)args[0][1]; }
        }
    }
}