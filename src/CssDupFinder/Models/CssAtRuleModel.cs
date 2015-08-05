using System;
using System.Collections.Generic;

namespace CssDupFinder.Models
{
    public class CssAtRuleModel
    {
        private readonly List<CssSelectorModel> selectors;

        public CssAtRuleModel(String name)
        {
            this.Name = name;
            this.selectors = new List<CssSelectorModel>();
        }

        public String Name { get; private set; }

        public Int32 Order { get; set; }

        public List<CssSelectorModel> Selectors
        {
            get { return selectors; }
        }
    }
}