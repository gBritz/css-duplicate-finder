using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;

namespace CssDupFinder.Models
{
    public class CssFileReportModel
    {
        public String FileFullPath { get; set; }

        public Int32 TotalSelectors { get; set; }

        public Int32 TotalDuplicates { get; set; }

        public IEnumerable<CssBlock> Blocks { get; set; }

        public Decimal DuplicatePercentage 
        {
            get { return (TotalDuplicates * 100) / (TotalSelectors <= 0 ? 1 : TotalSelectors); }
        }

        public class CssBlock
        {
            public SideBySideDiffModel Diff { get; set; }
            public Int32 CountDuplicates { get; set; }
        }
    }
}