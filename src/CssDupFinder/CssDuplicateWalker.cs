using CssDupFinder.Extensions;
using CssDupFinder.Models;
using CssSyntax;
using CssSyntax.SyntaxTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CssDupFinder
{
    public class CssDuplicateWalker : CssWalker
    {
        private readonly IDictionary<String, List<CssSelectorModel>> selectors = new Dictionary<String, List<CssSelectorModel>>();
        private readonly StringBuilder text = new StringBuilder();

        private CssSelectorModel current;

        public IDictionary<String, List<CssSelectorModel>> Duplicates
        {
            get { return selectors.Where(s => s.Value.Count() > 1).ToDictionary(s => s.Key, s => s.Value); }
        }

        public Int32 CountSelectors { get; private set; }

        public Int32 CountDuplicates { get; private set; }

        protected override void VisitBeginSelector(string selector, int line, int column)
        {
            CountSelectors++;
            if (selectors.ContainsKey(selector))
                CountDuplicates++;

            current = new CssSelectorModel();
            current.Position.Start = new Position(line, column);

            text.Append(Environment.NewLine);
            text.Append(selector);
            text.Append('{');
        }

        protected override void VisitEndSelector(string selector, int line, int column)
        {
            text.Append(Environment.NewLine);
            text.Append('}');

            current.Position.End = new Position(line, column);
            current.Text = text.ToString();

            selectors.GetOrNew(selector).Add(current);

            current = null;
            text.Clear();
        }

        protected override void VisitProperty(string name, string value)
        {
            text.Append(Environment.NewLine);
            text.Append("  ");
            text.Append(name);
            text.Append(": ");
            text.Append(value);
            text.Append(';');
        }
    }
}