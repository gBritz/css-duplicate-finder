using System;

namespace CssDupFinder.Models
{
    public class CssSelectorModel
    {
        private readonly RangePositionModel position = new RangePositionModel();

        public CssSelectorModel(String name)
        {
            this.Name = name;
        }

        public String Name { get; private set; }

        public Int32 Order { get; set; }

        public RangePositionModel Position
        {
            get { return this.position; }
        }

        public String Text { get; set; }
    }
}