using System;

namespace CssDupFinder.Models
{
    public class DiscoveryModel
    {
        public FolderContentModel[] Files { get; set; }

        public Int32 Count { get; set; }

        public Boolean CompactAllInOneFile { get; set; }
    }
}