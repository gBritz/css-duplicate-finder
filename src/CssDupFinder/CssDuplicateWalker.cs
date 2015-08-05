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
        private readonly StringBuilder text = new StringBuilder();

        private readonly List<CssAtRuleModel> rules = new List<CssAtRuleModel>();
        private readonly List<CssSelectorModel> selectors = new List<CssSelectorModel>();

        private CssAtRuleModel currentRule;
        private CssSelectorModel currentSelector;
        private Int32 order;

        public IDictionary<String, List<CssSelectorModel>> Duplicates
        {
            //get { return selectors.Where(s => s.Value.Count() > 1).ToDictionary(s => s.Key, s => s.Value); }
            get
            {
                return selectors.GroupBy(s => s.Name)
                    .Where(g => g.Count() > 1)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
        }

        public Int32 CountSelectors { get; private set; }

        public Int32 CountDuplicates { get; private set; }

        protected override void VisitBeginAtRule(string selector, int line, int column)
        {
            order++;
            selector = selector.Trim();
            var rule = rules.FirstOrDefault(r => r.Name == selector);
            currentRule = rule ?? new CssAtRuleModel(selector) { Order = order };
            if (rule == null)
                rules.Add(currentRule);
        }

        protected override void VisitEndAtRule(string selector, int line, int column)
        {
            currentRule = null;
        }

        protected override void VisitBeginSelector(string selector, int line, int column)
        {
            order++;
            selector = selector.Trim();
            var sels = currentRule != null ? currentRule.Selectors : selectors;
            var select = sels.FirstOrDefault(s => s.Name == selector);
            currentSelector = select ?? new CssSelectorModel(selector) { Order = order };
            currentSelector.Position.Start = new Position(line, column);

            CountSelectors++;

            if (select == null)
            {
                sels.Add(currentSelector);
                CountSelectors++;
            }

            text.Append(Environment.NewLine);
            text.Append(selector);
            text.Append('{');
        }

        protected override void VisitEndSelector(string selector, int line, int column)
        {
            text.Append(Environment.NewLine);
            text.Append('}');

            currentSelector.Position.End = new Position(line, column);
            currentSelector.Text = text.ToString();

            currentSelector = null;
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