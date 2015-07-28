using CssSyntax;
using System;
using System.IO;

namespace CssDupFinder.Test
{
    public static class CssWalkerExtension
    {
        public static T Interpret<T>(this String cssContent) where T : CssWalker, new()
        {
            if (String.IsNullOrEmpty(cssContent))
                throw new ArgumentNullException("cssContent");

            var reader = new StringReader(cssContent);

            var walker = new T();
            walker.Visit(reader);

            return walker;
        }

        public static void Interpret<T>(this String cssContent, T walker) where T : CssWalker
        {
            if (String.IsNullOrEmpty(cssContent))
                throw new ArgumentNullException("cssContent");
            if (walker == null)
                throw new ArgumentNullException("walker");

            var reader = new StringReader(cssContent);
            walker.Visit(reader);
        }
    }
}