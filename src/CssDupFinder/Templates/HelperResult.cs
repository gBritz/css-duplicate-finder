using CssDupFinder.Extensions;
using System.IO;

namespace System.Web.WebPages
{
    public class HelperResult
    {
        private readonly Action<TextWriter> render;

        public HelperResult(Action<TextWriter> render)
        {
            render.ThrowIfNull("render");

            this.render = render;
        }

        public void ExecuteIn(TextWriter writer)
        {
            writer.ThrowIfNull("writer");
            render(writer);
        }
    }
}