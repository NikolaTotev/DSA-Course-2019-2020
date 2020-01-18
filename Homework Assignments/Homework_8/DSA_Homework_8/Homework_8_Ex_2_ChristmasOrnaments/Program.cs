using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_Ex_2_ChristmasOrnaments
{
    public class UndirectedGraphNode<T>
    {
        private readonly Dictionary<T, UndirectedGraphNode<T>> m_Neighbors;
        public T Data { get; set; }
        public bool IsVisited { get; set; }

        public UndirectedGraphNode(T data)
        {

            m_Neighbors = new Dictionary<T, UndirectedGraphNode<T>>();
            Data = data;
        }

        public IReadOnlyDictionary<T,UndirectedGraphNode<T>> GetNeighbors()
        {
            return m_Neighbors;
        }

        public UndirectedGraphNode(UndirectedGraphNode<T> initialNeighbor, T data) : this(data)
        {
            initialNeighbor.m_Neighbors.Add(data,this);
        }
        
        public void AddNeighbor(UndirectedGraphNode<T> neighborToAdd)
        {
            if (!m_Neighbors.ContainsKey(neighborToAdd.Data))
            {
                m_Neighbors.Add(neighborToAdd.Data,neighborToAdd);
                neighborToAdd.AddNeighbor(this);
            }
        }
        
        public void Print()
        {
            Console.Write("{0} ", Data);
        }
    }

    public class UndirectedGraph<T>
    {
        public Dictionary<T, UndirectedGraphNode<T>> Nodes { get; set; }
        public int NumberOfComponents { get; set; }

        public UndirectedGraph(T initialNodeData)
        {
            Nodes = new Dictionary<T, UndirectedGraphNode<T>>();
            UndirectedGraphNode<T> initialNode = new UndirectedGraphNode<T>(initialNodeData);
            Nodes.Add(initialNodeData, initialNode);
            NumberOfComponents = 0;
        }

        public void AddEdge(T vertexOne, T vertexTwo)
        {
            if (vertexOne == null) throw new ArgumentNullException(nameof(vertexOne));
            if (vertexTwo == null) throw new ArgumentNullException(nameof(vertexTwo));

            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);

            if (!v1Index)
            {
                UndirectedGraphNode<T> nodeOne = new UndirectedGraphNode<T>(vertexOne);
                Nodes.Add(vertexOne, nodeOne);
            }

            if (!v2Index)
            {

                UndirectedGraphNode<T> nodeTwo = new UndirectedGraphNode<T>(vertexTwo);
                Nodes.Add(vertexTwo, nodeTwo);
            }


            Nodes[vertexTwo].AddNeighbor(Nodes[vertexOne]);
        }

        public void AddVertex(T vertex)
        {
            bool v2Index = Nodes.ContainsKey(vertex);

            if (!v2Index)
            {
                UndirectedGraphNode<T> newNode = new UndirectedGraphNode<T>(vertex);
                Nodes.Add(vertex, newNode);
            }
        }

      /// <summary>
        /// Generic DFS function. Can be used to solve any problem that requires DFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        public void Dfs(Func<UndirectedGraphNode<T>, bool> inspectFunc)
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
        private void InternalDfs(Dictionary<T, UndirectedGraphNode<T>> nodes, UndirectedGraphNode<T> node, Func<UndirectedGraphNode<T>, bool> inspectFunc)
        {

            if (inspectFunc(node))
            {
                return;
            }
            foreach (var neighbor in node.GetNeighbors())
            {
                if (!neighbor.Value.IsVisited)
                {
                    InternalDfs(nodes, neighbor.Value, inspectFunc);
                }
            }
        }

        /// <summary>
        /// Generic BFS function. Can be used to solve any problem that requires BFS, just plug in the inspectionFunc.
        /// </summary>
        /// <param name="inspectFunc">Must return true if the search is completed and no more nodes need to be checked</param>
        /// <param name="startId">Starting node, by default it is 0.</param>
        public void Bfs(Func<UndirectedGraphNode<T>, bool> inspectFunc, bool countComponents)
        {
            if (Nodes.Count == 0)
            {
                return;
            }

            Queue<UndirectedGraphNode<T>> nodeQueue = new Queue<UndirectedGraphNode<T>>();
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
                        UndirectedGraphNode<T> currentNode = nodeQueue.Dequeue();

                        if (inspectFunc != null && inspectFunc(currentNode))
                        {
                            return;
                        }

                        foreach (var neighbor in currentNode.GetNeighbors())
                        {
                            if (!neighbor.Value.IsVisited)
                            {
                                neighbor.Value.IsVisited = true;
                                nodeQueue.Enqueue(neighbor.Value);
                            }
                        }

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

    class Program
    {
        static void Main(string[] args)
        {
            int[] toyInfo = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse);
            int numberOfToys = toyInfo[0];
            int numberOfColorPairs = toyInfo[1];

            UndirectedGraph<int> toys = new UndirectedGraph<int>(1);

            for (int i = 0; i < numberOfColorPairs; i++)
            {
                int[] currentToyPair = Array.ConvertAll(Console.ReadLine().Trim(' ').Split(), int.Parse);
                toys.AddEdge(currentToyPair[0], currentToyPair[1]);
            }

            int remaining = numberOfToys - toys.Nodes.Count;
            
            Console.WriteLine(toys.CountComponents() + remaining);
        }
    }
}
