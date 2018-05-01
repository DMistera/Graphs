using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs {
    class StandardGraph : Graph {

        private bool[,] array;

        public StandardGraph() : base() {
        }

        public StandardGraph(bool[,] array) : base(array) {
        }

        public override bool Arc(int from, int to) {
            return array[from,to];
        }

        public override List<Arc> GetArcs() {
            List<Arc> result = new List<Arc>();
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    if(array[x, y]) {
                        result.Add(new Arc(x, y));
                    }
                }
            }
            return result;
        }

        public override string GetName() {
            return "Adjacency Matrix";
        }

        public override List<int> GetNextList(int vertex) {
            List<int> result = new List<int>();
            for(int i = 0; i < size; i++) {
                if (array[vertex,i]) {
                    result.Add(i);
                }
            }
            return result;
        }

        public override List<int> GetPrevList(int vertex) {
            List<int> result = new List<int>();
            for (int i = 0; i < size; i++) {
                if (array[i,vertex]) {
                    result.Add(i);
                }
            }
            return result;
        }

        protected override void Init(bool[,] array) {
            this.array = array;
        }
    }
}
