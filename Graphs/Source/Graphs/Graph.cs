using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs {
    abstract class Graph {

        public Graph() {
        }


        public Graph(bool[,] array) {
            Initialize(array);
        }

        public abstract List<int> GetNextList(int vertex);
        public abstract List<Arc> GetArcs();
        public abstract string GetName();
        protected abstract void Init(bool[,] array);
        protected int size;

        public void Initialize(bool[,] array) {
            size = array.GetLength(0);
            Init(array);
        }

        //DFS

        private int[] d;
        private int[] f;

        public void VSort() {
            Sort();
        }

        public void VGetReturnArcs() {
            GetReturnArcs();
        }

        public int[] Sort() {
            d = new int[size];
            f = new int[size];
            int[] result = new int[size];
            bool[] visited = new bool[size];
            for (int i = 0; i < size; i++) {
                visited[i] = false;
            }
            int resultCount = 0;
            int operationCount = 0;
            while (resultCount < size) {
                int currentVertex = 0;
                for (int i = 0; i < size; i++) {
                    if(!visited[i]) {
                        currentVertex = i;
                        break;
                    }
                }
                SortRec(result, ref resultCount, visited, currentVertex, ref operationCount);
            }
            return result;
        }

        private void SortRec(int[] result, ref int resultCount, bool[] visited, int vertex, ref int operationCount) {
            result[resultCount++] = vertex;
            d[vertex] = operationCount++;
            visited[vertex] = true;
            List<int> nexts = GetNextList(vertex);
            foreach(int v in nexts) {
                if (!visited[v]) {
                    SortRec(result, ref resultCount, visited, v, ref operationCount);
                }
            }
            f[vertex] = operationCount++;
        }

        public List<Arc> GetReturnArcs() {
            List<Arc> result = new List<Arc>();
            foreach(Arc a in GetArcs()) {
                if(d[a.To] < d[a.From] && d[a.From] < f[a.From] && f[a.From] < f[a.To] ) {
                    result.Add(a);
                }
            }
            return result;
        }
    }
}
