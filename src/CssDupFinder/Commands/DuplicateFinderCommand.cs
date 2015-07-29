using CssDupFinder.Extensions;
using CssDupFinder.Models;
using DiffPlex;
using DiffPlex.DiffBuilder;
using System;
using System.Collections.Generic;
using System.IO;

namespace CssDupFinder.Commands
{
    public class DuplicateFinderCommand : ICommand
    {
        private readonly String outputDirectory;
        private readonly FolderContentModel[] folders;
        private readonly ReportGenerator generator;

        public DuplicateFinderCommand(String outputDirectory, FolderContentModel[] folders, ReportGenerator generator)
        {
            outputDirectory.ThrowIfNull("outputDirectory");
            folders.ThrowIfNull("folders");
            generator.ThrowIfNull("generator");

            if (!Directory.Exists(outputDirectory))
            {
                var msg = String.Format("Directory '{0}' is not found.", outputDirectory);
                throw new ArgumentException(msg, "outputDirectory", new DirectoryNotFoundException(msg));
            }

            this.outputDirectory = outputDirectory;
            this.folders = folders;
            this.generator = generator;
        }

        public CommandType Type
        {
            get { return CommandType.FindDuplicates; }
        }

        public void Execute()
        {
            var path = CreateDirectoryName(this.outputDirectory);
            
            generator.GenerateDependences(path);

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

                    var fileReport = new CssFileReportModel
                    {
                        FileFullPath = cssFileFullPath,
                        TotalSelectors = walker.CountSelectors,
                        TotalDuplicates = walker.CountDuplicates,
                        Blocks = CreateIterable(walker.Duplicates)
                    };

                    generator.GenerateCssReport(outputFullPath, fileReport);
                    dashboard.AddAnalysis(reportFileName, walker.CountSelectors, walker.CountDuplicates);
                }
            }

            var dashboardFileName = Path.Combine(path, "index.html");
            generator.GenerateDashboardReport(dashboardFileName, dashboard);
        }

        protected virtual String CreateDirectoryName(String basePath)
        {
            var folderName = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss");
            return Path.Combine(basePath, folderName);
        }

        private static IEnumerable<CssFileReportModel.CssBlock> CreateIterable(IDictionary<String, List<CssSelectorModel>> duplicates)
        {
            var diffBuilder = new SideBySideDiffBuilder(new Differ());

            foreach (var duplicate in duplicates)
            {
                var diffResult = diffBuilder.BuildDiffModel(duplicate.Value[0].Text, duplicate.Value[1].Text);
                yield return new CssFileReportModel.CssBlock
                {
                    Diff = diffResult,
                    CountDuplicates = duplicate.Value.Count
                };
            }
        }
    }
}