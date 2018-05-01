using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs {
    class Arc {
        private int from;
        private int to;

        public Arc(int from, int to) {
            this.from = from;
            this.to = to;
        }

        public int From { get => from; set => from = value; }
        public int To { get => to; set => to = value; }

        public void Print() {
            Console.WriteLine(From + " " + To);
        }
    }
}
