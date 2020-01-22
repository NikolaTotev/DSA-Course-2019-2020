using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_4_CyclicGraph
{
    public class Node
    {
        public int Data { get; set; }
        public bool IsVisited { get; set; }
        public List<DirectedConnection> Connections = new List<DirectedConnection>();

        public Node(int data)
        {
            Data = data;
        }
    }
    public class DirectedConnection
    {
        public Node Tail { get; set; }
        public int Weight { get; set; }

        public DirectedConnection(Node tail, int weight)
        {
            Tail = tail;
            Weight = weight;
        }
    }

    class Graph
    {
        Dictionary<int, Node> Nodes = new Dictionary<int, Node>();

        public void AddConnection(int head, int tail, int weight)
        {
            Node headNode = new Node(head);
            Node tailNode = new Node(tail);
            if (!Nodes.ContainsKey(head))
            {
                Nodes.Add(head, headNode);
            }

            if (!Nodes.ContainsKey(tail))
            {
                Nodes.Add(tail, tailNode);
            }
            DirectedConnection newConnection = new DirectedConnection(Nodes[tail], weight);
            Nodes[head].Connections.Add(newConnection);
        }

        bool internalCycleCheck(Dictionary<int, Node> nodes, Node currentNode)
        {
            currentNode.IsVisited = true;
            foreach (var currentNodeConnection in currentNode.Connections)
            {
                if (!currentNodeConnection.Tail.IsVisited)
                {
                    
                    internalCycleCheck(nodes, currentNodeConnection.Tail);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
        public bool ContainsCycle()
        {
            if (Nodes.Count == 0)
            {
                return false;
            }

            foreach (var node in Nodes)
            {
                if (!node.Value.IsVisited)
                {
                    return internalCycleCheck(Nodes, node.Value);
                }
            }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int numberOfQueries = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse)[0];
            for (int i = 0; i < numberOfQueries; i++)
            {
                Graph myGraph = new Graph();
                int[] graphInfo = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
                int numberOfNodes = graphInfo[0];
                int numberOfConnections = graphInfo[1];

                for (int j = 0; j < numberOfConnections; j++)
                {
                    int[] connectionInfo = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
                    int head = connectionInfo[0];
                    int tail = connectionInfo[1];
                    int weight = connectionInfo[2];
                    myGraph.AddConnection(head,tail, weight);
                }

                bool hasCycle = myGraph.ContainsCycle();

                Console.Write(!hasCycle ? "true " : "false ");
            }

        }
    }
}
