using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practic
{
    public class Arc : ICloneable
    {
        public Knot knotOut;
        public Knot knotIn;
        public Arc(Knot knotOut, Knot knotIn)
        {
            this.knotIn = knotIn;
            this.knotOut = knotOut;
        }
        public object Clone() => MemberwiseClone();
    }
}
