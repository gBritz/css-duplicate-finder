using CssDupFinder.Models;
using CssDupFinder.Templates;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace CssDupFinder
{
    public class ReportGenerator
    {
        public virtual void GenerateDependences(String path)
        {
            var jsPath = Path.Combine(path, "js");
            var cssPath = Path.Combine(path, "css");

            Directory.CreateDirectory(jsPath);
            Directory.CreateDirectory(cssPath);

            var assembly = Assembly.GetExecutingAssembly();
            WriteEmbeddedFile("diff.css", cssPath, assembly);
            WriteEmbeddedFile("diff.js", jsPath, assembly);
            WriteEmbeddedFile("jquery-1.4.1.min.js", jsPath, assembly);
            WriteEmbeddedFile("main.css", cssPath, assembly);
        }

        public virtual void GenerateCssReport(String outputDirectory, CssFileReportModel model)
        {
            RenderTemplate(outputDirectory, writer => new CssFileReportTemplate(writer, model));
        }

        public virtual void GenerateDashboardReport(String outputDirectory, DashboardReportModel model)
        {
            RenderTemplate(outputDirectory, writer => new DashboardReportTemplate(writer, model));
        }

        protected virtual void RenderTemplate<T>(String outputDirectory, Func<TextWriter, TemplateBaseWriter<T>> template) where T : class
        {
            using (var writer = new StreamWriter(outputDirectory, false, Encoding.UTF8))
            {
                template(writer).Execute();
            }
        }

        private static void WriteEmbeddedFile(String fileName, String toFolder, Assembly assembly)
        {
            var resourceName = "CssDupFinder.Content." + fileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            using (var writer = new StreamWriter(Path.Combine(toFolder, fileName)))
            {
                reader.BaseStream.CopyTo(writer.BaseStream);
            }
        }
    }
}