using CssDupFinder.Extensions;
using CssDupFinder.Models;
using Newtonsoft.Json;
using ShellProgress;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace CssDupFinder.Commands
{
    public class DiscoveryCommand : ICommand
    {
        private readonly String topFolder;
        private readonly String outputDiscoveryResult;
        private readonly IFileSystem fileSystem;
        private readonly IProgressFactory progressFactory;

        private IProgressing progress;

        public DiscoveryCommand(String topFolder, String outputDiscoveryResult, IFileSystem fileSystem, IProgressFactory progressFactory)
        {
            topFolder.ThrowIfNull("topFolder");
            outputDiscoveryResult.ThrowIfNull("outputDiscoveryResult");
            fileSystem.ThrowIfNull("fileSystem");
            progressFactory.ThrowIfNull("progressFactory");

            this.topFolder = topFolder;
            this.outputDiscoveryResult = outputDiscoveryResult;
            this.fileSystem = fileSystem;
            this.progressFactory = progressFactory;
        }

        public CommandType Type
        {
            get { return CommandType.Discovery; }
        }

        public DiscoveryModel DiscoveryCssFiles()
        {
            var allFiles = fileSystem.Directory.GetFiles(topFolder, "*.css", SearchOption.AllDirectories);

            this.progress = progressFactory.CreateInstance(allFiles.Length);

            var folders = allFiles.Select((f, i) =>
                {
                    progress.Update(i);
                    return new
                    {
                        folder = Path.GetDirectoryName(f),
                        file = Path.GetFileName(f)
                    };
                })
                .GroupBy(x => x.folder)
                .Select(g => new FolderContentModel
                    {
                        Name = g.Key,
                        FileNames = g.Select(gr => gr.file).ToArray()
                    })
                .ToArray();

            return new DiscoveryModel
            {
                Folders = folders,
                Count = allFiles.Length,
                CompactAllInOneFile = true
            };
        }

        public void Execute()
        {
            if (!fileSystem.Directory.Exists(topFolder))
            {
                Console.WriteLine("Pasta não encontrada.");
                return;
            }

            var model = DiscoveryCssFiles();

            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            fileSystem.File.WriteAllText(outputDiscoveryResult, json, Encoding.UTF8);

            if (progress != null)
                progress.Complete();
        }
    }
}