using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CssDupFinder.Models
{
    public class CssSelectorModel
    {
        private readonly RangePositionModel position = new RangePositionModel();

        public RangePositionModel Position
        {
            get { return this.position; }
        }

        public String Text { get; set; }
    }
}