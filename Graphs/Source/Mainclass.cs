using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graphs {
    class Mainclass {
        public static void Main() {
            Program p = new Program();
            Thread t = new Thread(p.Start, 1000000000);
            t.Start();
        }
    }
}
