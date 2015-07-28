using CssDupFinder.Extensions;
using CssDupFinder.Models;
using System;
using System.IO;

namespace CssDupFinder.Commands
{
    public class DuplicateFinderCommand : ICommand
    {
        private readonly String outputDirectory;
        private readonly FolderContentModel[] folders;

        public DuplicateFinderCommand(String outputDirectory, FolderContentModel[] folders)
        {
            outputDirectory.ThrowIfNull("outputDirectory");
            folders.ThrowIfNull("folders");

            if (!Directory.Exists(outputDirectory))
            {
                var msg = String.Format("Directory '{0}' is not found.", outputDirectory);
                throw new ArgumentException(msg, "outputDirectory", new DirectoryNotFoundException(msg));
            }

            this.outputDirectory = outputDirectory;
            this.folders = folders;
        }

        public CommandType Type
        {
            get { return CommandType.FindDuplicates; }
        }

        public void Execute()
        {
            var folderName = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss");
            var path = Path.Combine(this.outputDirectory, folderName);

            var dashboard = new DashboardReportModel();

            foreach (var folder in this.folders)
            {
                foreach (var fileName in folder.FileNames)
                {
                    var walker = new CssDuplicateWalker();

                    var fullPath = Path.Combine(folder.Name, fileName);
                    using (var reader = new StreamReader(fullPath))
                    {
                        walker.Visit(reader);
                    }

                    var reportFileName = Path.GetFileNameWithoutExtension(fileName) + ".html";
                    var outputFullPath = Path.Combine(path, reportFileName);
                    var cssFileFullPath = Path.Combine(folder.Name, fileName);

                    dashboard.AddAnalysis(reportFileName, walker.CountSelectors, walker.CountDuplicates);
                }
            }
        }
    }
}