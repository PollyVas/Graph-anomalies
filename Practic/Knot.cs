using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practic
{
    public class Knot : ICloneable
    {
        public int number;
        public int level;
        
        public int x;
        public int y;

        public int weight;

        public bool avaible;
        public bool processed;
        public Knot() { }
        public Knot(int number, int level)
        {
            this.number = number;
            this.level = level;
            this.avaible = false;
            this.processed = false;
            this.weight = -1;
        }
        public object Clone() => MemberwiseClone();
    }
}
