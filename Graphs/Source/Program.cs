using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Graphs;

namespace Graphs {
    class Program {

        private const string outputPath = @".\excel";

        public void Start() {
            Directory.CreateDirectory(outputPath);
            Ex2();
            Ex3();
            Ex4();
            Console.WriteLine("Press any key to continue");
            Console.Read();
        }

        private void Ex2() {
            Console.WriteLine("Exercise 2");
            const int measures = 20;
            Graphics.Graph ex2Plot = new Graphics.Graph("Sorting Time", "Number of elements", "Time");
            for (int j = 0; j < 2; j++) {
                float d = j == 0 ? 0.2f : 0.4f;
                Console.WriteLine("Density: " + d);
                Graphics.Graph.Data ex2Data = new Graphics.Graph.Data("Density " + d);
                for (int i = 0; i < measures; i++) {
                    int n = (i + 1) * 200;
                    bool[,] data = GenerateGraph(n, d);
                    Graph g = new NextListGraph(data);
                    double time = Profile(g.VSort);
                    ex2Data.AddPoint(n, (float)(time));
                    Console.WriteLine("Completed " + (i + 1) + "/" + measures);
                }
                ex2Plot.AddData(ex2Data);
            }
            ex2Plot.StartWithNewThread();
            ex2Plot.WriteToFile(outputPath);
        }

        private void Ex3() {
            Console.WriteLine("Exercise 3");
            const int measures = 20;
            Graphics.Graph ex3Plot = new Graphics.Graph("Returning arcs", "Number of elements", "Number of returning arcs");
            for (int j = 0; j < 2; j++) {
                float d = j == 0 ? 0.2f : 0.4f;
                Console.WriteLine("Density: " + d);
                Graph g = new NextListGraph();
                Graphics.Graph.Data ex3Data = new Graphics.Graph.Data("Density " + d);
                for (int i = 0; i < measures; i++) {
                    int n = (i + 1) * 200;
                    bool[,] data = GenerateGraph(n, d);
                    g.Initialize(data);
                    g.VSort();
                    int arcCount = g.GetReturnArcs().Count;
                    ex3Data.AddPoint(n, (float)(arcCount));
                    Console.WriteLine("Completed " + (i + 1) + "/" + measures);
                }
                ex3Plot.AddData(ex3Data);
            }
            ex3Plot.StartWithNewThread();
            ex3Plot.WriteToFile(outputPath);
        }

        private void Ex4() {
            Console.WriteLine("Exercise 4");
            const int measures = 20;
            for (int j = 0; j < 2; j++) {
                float d = j == 0 ? 0.2f : 0.4f;
                Console.WriteLine("Density: " + d);
                Graphics.Graph ex4Plot = new Graphics.Graph("Search time of returning arcs, density " + d, "Number of elements", "Time");
                List<Graphics.Graph.Data> plotDatas = new List<Graphics.Graph.Data>();
                List<Graph> graphs = new List<Graph>();
                var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Graph));
                foreach (var x in types) {
                    Graph g = (Graph)Activator.CreateInstance(x);
                    graphs.Add(g);
                    plotDatas.Add(new Graphics.Graph.Data(g.GetName()));
                }
                for (int i = 0; i < measures; i++) {
                    int n = (i + 1) * 200;
                    bool[,] data = GenerateGraph(n, d);
                    for(int k = 0; k < graphs.Count; k++) {
                        Graph g = graphs.ElementAt(k);

                        g.Initialize(data);
                        g.VSort();
                        double time = Profile(g.VGetReturnArcs);
                        plotDatas.ElementAt(k).AddPoint(n, (float)(time));
                    }
                    Console.WriteLine("Completed " + (i + 1) + "/" + measures);
                }
                foreach(Graphics.Graph.Data data in plotDatas) {
                    ex4Plot.AddData(data);
                }
                ex4Plot.StartWithNewThread();
                ex4Plot.WriteToFile(outputPath);
            }
        }

        private bool[,] GenerateGraph(int size, float density) {
            bool[,] array = new bool[size, size];
            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    array[x, y] = false;
                }
            }
            int arcCount = (int)(size * size * density);
            Random rand = new Random();
            for (int i = 0; i < arcCount;) {
                int x = rand.Next() % size;
                int y = rand.Next() % size;
                if (array[x, y] == false) {
                    array[x, y] = true;
                    i++;
                }
            }
            return array;
        }



        static double Profile(Action func) {
            //Run at highest priority to minimize fluctuations caused by other processes/threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            // warm up 
            func();

            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            watch.Start();
            func();
            watch.Stop();
            return watch.Elapsed.TotalMilliseconds;
        }
    }
}
