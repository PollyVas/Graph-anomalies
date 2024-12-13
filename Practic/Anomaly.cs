using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practic
{
    public class Anomaly
    {
        public Graph grWithChanges;
        public int type;
        public int residual;
        public string comment;
        public Anomaly(Graph grWithChanges, int type, int residual, string comment) {
        this.grWithChanges = grWithChanges;
        this.type = type;
        this.residual = residual;
        this.comment = comment;
        }
        public Anomaly() { }
    }
}
