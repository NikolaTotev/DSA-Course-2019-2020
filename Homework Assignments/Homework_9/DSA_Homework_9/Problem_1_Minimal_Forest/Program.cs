using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_1_Minimal_Forest
{
    class DisjointSet
    {
        private Dictionary<MultiGraphNode, MultiGraphNode> Parent { get; set; }
        public Dictionary<MultiGraphNode, int> Rank { get; set; }

        public DisjointSet()
        {
            Parent = new Dictionary<MultiGraphNode, MultiGraphNode>();
            Rank = new Dictionary<MultiGraphNode, int>();
        }


        public void MakeSet(MultiGraphNode x)
        {
            Parent[x] = x;
            Rank[x] = 0;
        }

        public MultiGraphNode FindSet(MultiGraphNode x)
        {
            if (Parent[x] != x)
            {
                Parent[x] = FindSet(Parent[x]);
            }

            return Parent[x];
        }
        public void Union(MultiGraphNode x, MultiGraphNode y)
        {
            MultiGraphNode representativeX = FindSet(x);
            MultiGraphNode representativeY = FindSet(y);

            if (Rank[representativeX] == Rank[representativeY])
            {
                Rank[representativeY]++;
                Parent[representativeX] = representativeY;
            }

            if (Rank[representativeX] > Rank[representativeY])
            {
                Parent[representativeY] = representativeX;
            }
            else
            {
                Parent[representativeX] = representativeY;
            }
        }

        public MultiGraphNode FindImmediateParent(MultiGraphNode x)
        {
            return Parent[x];
        }

        public int FindRank(MultiGraphNode x)
        {
            return Rank[x];
        }

    }
    public class Edge<T>
    {
        public T Node1 { get; set; }
        public T Node2 { get; set; }
        public int Weight { get; set; }

        public Edge(T node1, T node2, int weight)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }

        public void PrintEdge()
        {
            Console.WriteLine("{0} : {1} - {2}", Node1, Node2, Weight.ToString());
        }
    }
    class MultiGraphNode
    {
        public int Data { get; set; }
        public bool IsVisited { get; set; }
        public int ComponentIndex { get; set; }
        public List<Edge<MultiGraphNode>> Connections { get; set; }
        public MultiGraphNode(int data)
        {
            Data = data;
            Connections = new List<Edge<MultiGraphNode>>();
        }
    }
    class WeightedMultiGraph
    {
        public Dictionary<int, MultiGraphNode> Nodes = new Dictionary<int, MultiGraphNode>();
        public List<Edge<MultiGraphNode>> Edges { get; set; }

        public int NumberOfComponents { get; set; }
        public int MSFWeight { get; set; }

        public WeightedMultiGraph()
        {
            NumberOfComponents = 0;
            Edges = new List<Edge<MultiGraphNode>>();
        }
        public void AddEdge(int vertex1, int vertex2, int edgeWeight)
        {

            MultiGraphNode node1 = new MultiGraphNode(vertex1);
            MultiGraphNode node2 = new MultiGraphNode(vertex2);
            Edge<MultiGraphNode> newEdge = new Edge<MultiGraphNode>(node1, node2, edgeWeight);
            if (!Nodes.ContainsKey(vertex1))
            {
                Nodes.Add(vertex1, node1);
            }

            if (!Nodes.ContainsKey(vertex2))
            {
                Nodes.Add(vertex2, node2);
            }

            Nodes[vertex1].Connections.Add(newEdge);
            Nodes[vertex2].Connections.Add(newEdge);
            Edges.Add(newEdge);
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="startId">Starting node, by default it is 0.</param>

        public void SetComponenets(bool countComponents)
        {
            if (Nodes.Count == 0)
            {
                return;
            }

            Queue<MultiGraphNode> nodeQueue = new Queue<MultiGraphNode>();
            foreach (var node in Nodes)
            {

                if (!node.Value.IsVisited)
                {
                    if (countComponents)
                    {
                        NumberOfComponents++;
                    }
                    nodeQueue.Enqueue(node.Value);
                    node.Value.IsVisited = true;
                    node.Value.ComponentIndex = NumberOfComponents;
                    while (nodeQueue.Count != 0)
                    {
                        MultiGraphNode currentNode = nodeQueue.Dequeue();

                        foreach (var connection in currentNode.Connections)
                        {
                            if (!connection.Node2.IsVisited)
                            {
                                connection.Node2.IsVisited = true;
                                connection.Node2.ComponentIndex = NumberOfComponents;

                                nodeQueue.Enqueue(connection.Node1);
                            }
                        }

                    }
                }
            }
        }

        public void Kruskal()
        {
            DisjointSet set = new DisjointSet();
            foreach (var nodesKey in Nodes.Keys)
            {

                set.MakeSet(Nodes[nodesKey]);


            }

            List<Edge<MultiGraphNode>> sortedEdges = Edges.OrderBy(x => x.Weight).ToList();

            foreach (var edge in sortedEdges)
            {

                if (set.FindSet(Nodes[edge.Node1.Data]) != set.FindSet(Nodes[edge.Node2.Data]))
                {
                    MSFWeight += edge.Weight;
                    set.Union(Nodes[edge.Node1.Data], Nodes[edge.Node2.Data]);
                }
            }
        }

        public int CalculateWeight()
        {
            Kruskal();
            return MSFWeight;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] gNodesEdges = Console.ReadLine().TrimEnd().Split(' ');

            int gNodes = Convert.ToInt32(gNodesEdges[0]);
            int gEdges = Convert.ToInt32(gNodesEdges[1]);

            WeightedMultiGraph graph = new WeightedMultiGraph();
            for (int i = 0; i < gEdges; i++)
            {
                string[] gFromToWeight = Console.ReadLine().TrimEnd().Split(' ');

                graph.AddEdge(Convert.ToInt32(gFromToWeight[0]), Convert.ToInt32(gFromToWeight[1]), Convert.ToInt32(gFromToWeight[2]));

            }

            int res = graph.CalculateWeight();
            Console.WriteLine(res.ToString());
        }
    }
}
