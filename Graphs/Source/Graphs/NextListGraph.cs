using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs {
    class NextListGraph : Graph {

        List<int>[] vertices;

        public NextListGraph(bool[,] array) : base(array) {
        }

        public NextListGraph() : base() {
        }

        public override List<int> GetNextList(int vertex) {
            return vertices[vertex];
        }


        protected override void Init(bool[,] array) {
            vertices = new List<int>[size];
            for (int x = 0; x < size; x++) {
                vertices[x] = new List<int>();
            }
            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    if (array[x, y]) {
                        vertices[x].Add(y);
                    }
                }
            }
        }

        public void Print() {
            for(int i = 0; i < vertices.Length; i++) {
                List<int> vertex = vertices[i];
                Console.Write(i + ". ");
                foreach(int next in vertex) {
                    Console.Write(next + " ");
                }
                Console.Write("\n");
            }
        }

        public override List<Arc> GetArcs() {
            List<Arc> result = new List<Arc>();
            for(int i = 0; i < size; i++) {
                List<int> vertex = vertices[i];
                foreach(int next in vertex) {
                    result.Add(new Arc(i, next));
                }
            }
            return result;
        }

        public override string GetName() {
            return "List of next vertices";
        }
    }
}
