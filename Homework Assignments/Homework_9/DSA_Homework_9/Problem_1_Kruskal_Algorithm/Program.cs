using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_4_Kruskal_Algorithm
{
    public class Edge
    {
        public int Node1 { get; set; }
        public int Node2 { get; set; }
        public int Weight { get; set; }

        public Edge(int node1, int node2, int weight)
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
    public class WeightedUndirectedGraphNode<T, W>
    {
        private readonly Dictionary<T, WeightedGraphConnection<T, W>> m_Neighbors;
        public T Data { get; set; }
        public bool IsVisited { get; set; }

        public WeightedUndirectedGraphNode(T data)
        {

            m_Neighbors = new Dictionary<T, WeightedGraphConnection<T, W>>();
            Data = data;
        }

        public IReadOnlyDictionary<T, WeightedGraphConnection<T, W>> GetNeighbors()
        {
            return m_Neighbors;
        }

        public WeightedUndirectedGraphNode(WeightedUndirectedGraphNode<T, W> initialNeighbor, T data, W connectionWeight) : this(data)
        {
            initialNeighbor.AddNeighbor(this, connectionWeight);
        }

        public void AddNeighbor(WeightedUndirectedGraphNode<T, W> neighborToAdd, W connectionWeight)
        {
            if (!m_Neighbors.ContainsKey(neighborToAdd.Data))
            {
                WeightedGraphConnection<T, W> newConnection = new WeightedGraphConnection<T, W>(neighborToAdd, connectionWeight);
                m_Neighbors.Add(neighborToAdd.Data, newConnection);
                neighborToAdd.AddNeighbor(this, connectionWeight);
            }
        }

        public void Print()
        {
            Console.Write("{0} ", Data);
        }
    }
    public class WeightedGraphConnection<T, W>
    {
        public WeightedUndirectedGraphNode<T, W> ConnectionNeighbor { get; set; }
        public W ConnectionWeight { get; set; }

        public WeightedGraphConnection(WeightedUndirectedGraphNode<T, W> connectionNeighbor, W weight)
        {
            ConnectionNeighbor = connectionNeighbor;
            ConnectionWeight = weight;
        }

    }
    public class WeightedUndirectedGraph<T>
    {
        private readonly int m_InfinityWeightValue;
        private readonly int m_ZeroWeightValue;
        public Dictionary<T, WeightedUndirectedGraphNode<T, int>> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public int NumberOfComponents { get; set; }

        public WeightedUndirectedGraph()
        {
            Edges = new List<Edge>();
            Nodes = new Dictionary<T, WeightedUndirectedGraphNode<T, int>>();
            m_InfinityWeightValue = int.MaxValue;
            m_ZeroWeightValue = 0;
        }

        public WeightedUndirectedGraph(T initialNodeData)
        {

            Nodes = new Dictionary<T, WeightedUndirectedGraphNode<T, int>>();
            WeightedUndirectedGraphNode<T, int> initialNode = new WeightedUndirectedGraphNode<T, int>(initialNodeData);
            Nodes.Add(initialNodeData, initialNode);
            NumberOfComponents = 0;
            m_InfinityWeightValue = int.MaxValue;
            m_ZeroWeightValue = 0;
        }

        public void AddEdge(T vertexOne, T vertexTwo, int connectionWeight)
        {
            if (vertexOne == null) throw new ArgumentNullException(nameof(vertexOne));
            if (vertexTwo == null) throw new ArgumentNullException(nameof(vertexTwo));

            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);

            if (!v1Index)
            {
                WeightedUndirectedGraphNode<T, int> nodeOne = new WeightedUndirectedGraphNode<T, int>(vertexOne);
                Nodes.Add(vertexOne, nodeOne);
            }

            if (!v2Index)
            {

                WeightedUndirectedGraphNode<T, int> nodeTwo = new WeightedUndirectedGraphNode<T, int>(vertexTwo);
                Nodes.Add(vertexTwo, nodeTwo);
            }

            Edge newEdge = new Edge(Convert.ToInt32(vertexOne), Convert.ToInt32(vertexTwo), connectionWeight);
            Edges.Add(newEdge);
            Nodes[vertexTwo].AddNeighbor(Nodes[vertexOne], connectionWeight);
        }

        public void AddVertex(T vertex)
        {
            bool v2Index = Nodes.ContainsKey(vertex);

            if (!v2Index)
            {
                WeightedUndirectedGraphNode<T, int> newNode = new WeightedUndirectedGraphNode<T, int>(vertex);
                Nodes.Add(vertex, newNode);
            }
        }

        /// <summary>
        /// Generic DFS function. Can be used to solve any problem that requires DFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        public void Dfs(Func<WeightedUndirectedGraphNode<T, int>, bool> inspectFunc)
        {
            if (Nodes.Count == 0)
            {
                return;
            }

            foreach (var node in Nodes)
            {
                if (node.Value.IsVisited)
                {
                    InternalDfs(Nodes, node.Value, inspectFunc);
                }
            }
        }
        private void InternalDfs(Dictionary<T, WeightedUndirectedGraphNode<T, int>> nodes, WeightedUndirectedGraphNode<T, int> node, Func<WeightedUndirectedGraphNode<T, int>, bool> inspectFunc)
        {
            if (inspectFunc(node))
            {
                return;
            }
            foreach (var neighbor in node.GetNeighbors())
            {
                if (!neighbor.Value.ConnectionNeighbor.IsVisited)
                {
                    InternalDfs(nodes, neighbor.Value.ConnectionNeighbor, inspectFunc);
                }
            }
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="countComponents">Bool that when enabled counts the number of components in a graph.</param>
        public void Bfs(Func<WeightedUndirectedGraphNode<T, int>, bool> inspectFunc, bool countComponents)
        {
            if (Nodes.Count == 0)
            {
                return;
            }

            Queue<WeightedUndirectedGraphNode<T, int>> nodeQueue = new Queue<WeightedUndirectedGraphNode<T, int>>();
            foreach (var undirectedGraphNode in Nodes)
            {

                if (!undirectedGraphNode.Value.IsVisited)
                {
                    if (countComponents)
                    {
                        NumberOfComponents++;
                    }
                    nodeQueue.Enqueue(undirectedGraphNode.Value);
                    undirectedGraphNode.Value.IsVisited = true;
                    while (nodeQueue.Count != 0)
                    {
                        WeightedUndirectedGraphNode<T, int> currentNode = nodeQueue.Dequeue();

                        if (inspectFunc != null && inspectFunc(currentNode))
                        {
                            return;
                        }

                        foreach (var neighbor in currentNode.GetNeighbors())
                        {
                            if (!neighbor.Value.ConnectionNeighbor.IsVisited)
                            {
                                neighbor.Value.ConnectionNeighbor.IsVisited = true;
                                nodeQueue.Enqueue(neighbor.Value.ConnectionNeighbor);
                            }
                        }
                    }
                }
            }
        }

        public void FindPath(T source, Func<SortedDictionary<WeightedUndirectedGraphNode<T, int>, int>, bool> resultHandler)
        {
            WeightedUndirectedGraphNode<T, int> sourceNode = Nodes[source];
            SortedDictionary<WeightedUndirectedGraphNode<T, int>, int> distances = new SortedDictionary<WeightedUndirectedGraphNode<T, int>, int>();
            distances[sourceNode] = m_ZeroWeightValue;
            foreach (var node in Nodes)
            {
                distances[node.Value] = m_InfinityWeightValue;
            }

            while (distances.Count > 0)
            {
                WeightedUndirectedGraphNode<T, int> currentVertex = Nodes[distances.Min().Key.Data];
                distances.Remove(currentVertex);

                foreach (var neighbor in currentVertex.GetNeighbors())
                {
                    int alternativeDist = distances[currentVertex] + neighbor.Value.ConnectionWeight;
                    if (alternativeDist < distances[neighbor.Value.ConnectionNeighbor])
                    {
                        distances[neighbor.Value.ConnectionNeighbor] = alternativeDist;
                    }
                }
            }
        }

        public int CountComponents()
        {
            Bfs(null, true);
            return NumberOfComponents;
        }
    }
    class DisjointSet
    {
        private Dictionary<int, int> Parent { get; set; }
        public Dictionary<int, int> Rank { get; set; }

        public DisjointSet()
        {
            Parent = new Dictionary<int, int>();
            Rank = new Dictionary<int, int>();
        }


        public void MakeSet(int x)
        {
            Parent[x] = x;
            Rank[x] = 0;
        }

        public int FindSet(int x)
        {
            if (Parent[x] != x)
            {
                Parent[x] = FindSet(Parent[x]);
            }

            return Parent[x];
        }
        public void Union(int x, int y)
        {
            int representativeX = FindSet(x);
            int representativeY = FindSet(y);

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

        public int FindImmediateParent(int x)
        {
            return Parent[x];
        }

        public int FindRank(int x)
        {
            return Rank[x];
        }

    }
    public static class MST_Algorithms
    {
        public static int Kruskal(WeightedUndirectedGraph<int> graph)
        {
            DisjointSet set = new DisjointSet();
            foreach (var nodesKey in graph.Nodes.Keys)
            {
                set.MakeSet(nodesKey);
            }

            List<Edge> sortedEdges = graph.Edges.OrderBy(x => x.Weight).ToList();
            int result = 0;
            foreach (var edge in sortedEdges)
            {
                if (set.FindSet(edge.Node1) != set.FindSet(edge.Node2))
                {
                   // edge.PrintEdge();
                    result += edge.Weight;
                    set.Union(edge.Node1, edge.Node2);
                }
            }

            return result;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            string[] gNodesEdges = Console.ReadLine().TrimEnd().Split(' ');

            int gNodes = Convert.ToInt32(gNodesEdges[0]);
            int gEdges = Convert.ToInt32(gNodesEdges[1]);

            List<int> gFrom = new List<int>();
            List<int> gTo = new List<int>();
            List<int> gWeight = new List<int>();
            WeightedUndirectedGraph<int> graph = new WeightedUndirectedGraph<int>();
            for (int i = 0; i < gEdges; i++)
            {
                string[] gFromToWeight = Console.ReadLine().TrimEnd().Split(' ');

                graph.AddEdge(Convert.ToInt32(gFromToWeight[0]), Convert.ToInt32(gFromToWeight[1]), Convert.ToInt32(gFromToWeight[2]));                

            }

            int res = MST_Algorithms.Kruskal(graph);
            Console.WriteLine(res.ToString());
        }
    }
}
