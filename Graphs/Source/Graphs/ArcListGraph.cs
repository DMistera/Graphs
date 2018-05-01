using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs {
    class ArcListGraph : Graph {

        List<Arc> arcs;

        public ArcListGraph() {
            arcs = new List<Arc>();
        }

        public ArcListGraph(bool[,] array) : base(array) {
        }

        public override List<Arc> GetArcs() {
            return arcs;
        }

        public override string GetName() {
            return "List of Arcs";
        }

        public override List<int> GetNextList(int vertex) {
            List<int> result = new List<int>();
            foreach(Arc a in arcs) {
                if(a.From == vertex) {
                    result.Add(a.To);
                }
            }
            return result;
        }

        protected override void Init(bool[,] array) {
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    if(array[x, y]) {
                        arcs.Add(new Arc(x, y));
                    }
                }
            }
        }
    }
}
