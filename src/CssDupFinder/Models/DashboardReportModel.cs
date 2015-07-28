using CssDupFinder.Extensions;
using System;
using System.Collections.Generic;

namespace CssDupFinder.Models
{
    public class DashboardReportModel
    {
        private readonly List<String> links = new List<String>();

        public String[] Links
        {
            get { return links.ToArray(); }
        }

        public Int32 TotalSelectors { get; private set; }

        public Int32 TotalDuplicated { get; private set; }

        public void AddAnalysis(String link, Int32 countSelectors, Int32 countDuplicated)
        {
            link.ThrowIfNull("link");

            TotalSelectors += countSelectors;
            TotalDuplicated += countDuplicated;

            links.Add(link);
        }
    }
}