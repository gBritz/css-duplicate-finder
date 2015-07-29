using CssDupFinder.Extensions;
using System;
using System.IO;
using System.Web.WebPages;

namespace CssDupFinder.Templates
{
    public abstract class TemplateBaseWriter<T> where T : class
    {
        private readonly TextWriter writer;

        public TemplateBaseWriter(TextWriter writer, T model)
        {
            writer.ThrowIfNull("writer");
            model.ThrowIfNull("model");

            this.writer = writer;
            this.Model = model;
        }

        public T Model { get; private set; }

        public abstract void Execute();

        protected void WriteLiteral(String textToAppend)
        {
            WriteTo(writer, textToAppend);
        }

        protected void Write(HelperResult result)
        {
            result.ExecuteIn(this.writer);
        }

        protected void Write(Object value)
        {
            WriteTo(writer, value);
        }

        protected void WriteTo(TextWriter writer, HelperResult result)
        {
            result.ExecuteIn(writer);
        }

        protected void WriteTo(TextWriter writer, Object value)
        {
            writer.Write(value);
        }

        protected void WriteLiteralTo(TextWriter writer, Object value)
        {
            writer.Write(value.ToString().Trim());
        }
    }
}