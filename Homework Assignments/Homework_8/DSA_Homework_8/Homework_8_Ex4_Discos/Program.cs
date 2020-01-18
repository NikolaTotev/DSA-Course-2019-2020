using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_Ex4_Discos
{
    public class PriorityQueue<T> where T : IComparable
    {
        private List<T> data;
        private int size { get; set; }

        public int Count()
        {
            return data.Count;
        }

        public T Front()
        {
            return data[0];
        }

        public PriorityQueue(int numberOfExpectedElements)
        {
            data = new List<T>(numberOfExpectedElements);

        }

        public int Parent(int i) => (i - 1) / 2;

        public int LeftChild(int i) => (2 * i + 1);

        public int RightChild(int i) => (2 * i + 2);


        private void SwapT(int childIndex, int parentIndex)
        {
            T tmp = data[childIndex];
            data[childIndex] = data[parentIndex];
            data[parentIndex] = tmp;
        }

        public void Enqueue(T itemToAdd)
        {
            int childIndex = data.Count - 1;
            data.Add(itemToAdd);
            while (childIndex > 0)
            {
                if (data[childIndex].CompareTo(data[Parent(childIndex)]) > 0)
                {
                    break;
                }

                T tmp = data[childIndex];
                data[childIndex] = data[Parent(childIndex)];
                data[Parent(childIndex)] = tmp;
                childIndex = Parent(childIndex);
            }
        }

        public void Dequeue()
        {
            if (Count() > 0)
            {
                int lastIndex = data.Count - 1;
                T frontItem = data[0];
                data[0] = data[lastIndex];
                data.RemoveAt(lastIndex);

                MinHeapify(0);
            }
        }

        void MinHeapify(int i)
        {
            int leftIndex = LeftChild(i);
            int rightIndex = RightChild(i);
            int smallest = i;

            if (leftIndex < Count() && data[leftIndex].CompareTo(data[i]) == -1)
            {
                smallest = leftIndex;
            }

            if (rightIndex < Count() && data[rightIndex].CompareTo(data[smallest]) == -1)
            {
                smallest = rightIndex;
            }

            if (smallest != i)
            {
                SwapT(i, smallest);
                MinHeapify(smallest);
            }
        }

        public void Sort()
        {
            int lastIndex = Count();
            for (int i = lastIndex / 2 - 1; i >= 0; i--)
            {
                Heapify(data, lastIndex, i);
            }

            for (int i = lastIndex - 1; i >= 0; i--)
            {
                T temp = data[0];
                data[0] = data[i];
                data[i] = temp;
                Heapify(data, i, 0);
            }
        }

        private static void Heapify(List<T> arr, int lastIndex, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < lastIndex && arr[left].CompareTo(arr[largest]) == 1)
            {
                largest = left;
            }

            if (right < lastIndex && arr[right].CompareTo(arr[largest]) == 1)
            {
                largest = right;
            }

            if (largest != i)
            {
                T swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                Heapify(arr, lastIndex, largest);
            }
        }
    }
    public class WeightedGraphConnection<T>
    {
        public WeightedUndirectedGraphNode<T> ConnectionNeighbor { get; set; }
        public int ConnectionWeight { get; set; }

        public WeightedGraphConnection(WeightedUndirectedGraphNode<T> connectionNeighbor, int weight)
        {
            ConnectionNeighbor = connectionNeighbor;
            ConnectionWeight = weight;
        }

    }
    public class WeightedUndirectedGraphNode<T> : IComparable
    {
        private readonly Dictionary<WeightedGraphConnection<T>, T> m_Neighbors;
        public T Data { get; set; }
        public bool IsVisited { get; set; }
        public int DistanceToThis { get; set; }

        /// <summary>
        /// Constructor for WeightedNode
        /// </summary>
        /// <param name="data"></param>
        public WeightedUndirectedGraphNode(T data)
        {
            //Initialize the dictionary
            m_Neighbors = new Dictionary<WeightedGraphConnection<T>, T>();

            //Set the data.
            Data = data;
        }

        //Returns a read only 
        public IReadOnlyDictionary<WeightedGraphConnection<T>, T> GetNeighbors()
        {
            return m_Neighbors;
        }

        public void OneWayAdd(WeightedUndirectedGraphNode<T> neighborToAdd, int connectionWeight)
        {
            var myKey = m_Neighbors.FirstOrDefault(x => x.Value.Equals(neighborToAdd.Data)).Key;
            if (connectionWeight != myKey.ConnectionWeight)
            {

                //Create a new connection object. This is used for the weighted graph.
                WeightedGraphConnection<T> newConnection = new WeightedGraphConnection<T>(neighborToAdd, connectionWeight);

                //Add the new connection to the list of connections.
                m_Neighbors.Add(newConnection, neighborToAdd.Data);

            }
        }

        /// <summary>
        /// Adds a neighbor to the node.
        /// </summary>
        /// <param name="neighborToAdd"></param>
        /// <param name="connectionWeight"></param>
        public void AddNeighbor(WeightedUndirectedGraphNode<T> neighborToAdd, int connectionWeight)
        {

            //First check if the connection doesn't already exist.
            if (!m_Neighbors.ContainsValue(neighborToAdd.Data))
            {
                //Create a new connection object. This is used for the weighted graph.
                WeightedGraphConnection<T> newConnection = new WeightedGraphConnection<T>(neighborToAdd, connectionWeight);

                //Add the new connection to the list of connections.
                m_Neighbors.Add(newConnection, neighborToAdd.Data);

                //Tell the neighbor to add this connection to it's list of connections.
                //This is done because the graph is undirected.
                neighborToAdd.AddNeighbor(this, connectionWeight);
            }
            else
            {
                var myKey = m_Neighbors.FirstOrDefault(x => x.Value.Equals(neighborToAdd.Data)).Key;
                if (connectionWeight != myKey.ConnectionWeight)
                {
                    WeightedGraphConnection<T> newConnection = new WeightedGraphConnection<T>(neighborToAdd, connectionWeight);

                    m_Neighbors.Add(newConnection, neighborToAdd.Data);
                    neighborToAdd.OneWayAdd(this, connectionWeight);
                }
            }
        }

        //CompareTo method required by the IComparable interface.
        public int CompareTo(object obj)
        {
            if (obj is WeightedUndirectedGraphNode<T> compObj)
            {
                //If DistanceToThis is smaller than the DistanceToThis of the object it's being compared to return -1.
                if (DistanceToThis < compObj.DistanceToThis)
                {
                    return -1;
                }

                //If DistanceToThis is the same as the DistanceToThis of the object it's being compared to return 0.
                if (compObj.DistanceToThis == DistanceToThis)
                {
                    return 0;
                }

                //If DistanceToThis is larger than DistanceToThis of the object it's being compared to return 1.
                if (DistanceToThis > compObj.DistanceToThis)
                {
                    return 1;
                }
            }

            //used only if the check up above is false.
            return 42;
        }
    }
    public class WeightedUndirectedGraph<T>
    {
        public Dictionary<T, WeightedUndirectedGraphNode<T>> Nodes { get; set; }
        public int NumberOfComponents { get; set; }

        public WeightedUndirectedGraph(T initialNodeData)
        {
            Nodes = new Dictionary<T, WeightedUndirectedGraphNode<T>>();
            WeightedUndirectedGraphNode<T> initialNode = new WeightedUndirectedGraphNode<T>(initialNodeData);
            Nodes.Add(initialNodeData, initialNode);
            NumberOfComponents = 0;
        }

        /// <summary>
        /// Function for adding an edge. 
        /// </summary>
        /// <param name="vertexOne"></param>
        /// <param name="vertexTwo"></param>
        /// <param name="connectionWeight"></param>
        public void AddEdge(T vertexOne, T vertexTwo, int connectionWeight)
        {
            //Check if vertex one and two are null.
            if (vertexOne == null) throw new ArgumentNullException(nameof(vertexOne));
            if (vertexTwo == null) throw new ArgumentNullException(nameof(vertexTwo));

            //Check if the nodes aren't already in the graph.
            bool v2Index = Nodes.ContainsKey(vertexTwo);
            bool v1Index = Nodes.ContainsKey(vertexOne);


            //The following segment guarantees that the two nodes are in the graph before making a connection between them.

            //Case: First node isn't in the graph.
            if (!v1Index)
            {
                //Create a new node that has the value of vertex one.
                WeightedUndirectedGraphNode<T> nodeOne = new WeightedUndirectedGraphNode<T>(vertexOne);

                //Add the node to the dictionary of the graph.
                Nodes.Add(vertexOne, nodeOne);
            }

            //Case: Second node isn't in the graph.
            if (!v2Index)
            {
                //Create a new node that has the value of vertex two.
                WeightedUndirectedGraphNode<T> nodeTwo = new WeightedUndirectedGraphNode<T>(vertexTwo);

                //Add the node to the dictionary of the graph.
                Nodes.Add(vertexTwo, nodeTwo);
            }

            //Finally establish the connection between the two nodes.
            Nodes[vertexTwo].AddNeighbor(Nodes[vertexOne], connectionWeight);
        }

        //Algorithm for finding the shortest paths to all nodes from a given source node.
        public Dictionary<WeightedUndirectedGraphNode<T>, int> FindPath(T source)
        {
            //Get the source node.
            WeightedUndirectedGraphNode<T> sourceNode = Nodes[source];

            //Create dictionary that will hold the distances to all nodes from source node.
            //This dictionary also contains the distance to the source itself.
            Dictionary<WeightedUndirectedGraphNode<T>, int> distances = new Dictionary<WeightedUndirectedGraphNode<T>, int>();

            //Create priority queue that will always have the next smallest distance to at the front.
            PriorityQueue<WeightedUndirectedGraphNode<T>> distQueue = new PriorityQueue<WeightedUndirectedGraphNode<T>>(Nodes.Count);

            //As the previous loop set the distance from source to source to infinity
            //it now has to be changed to 0 as it should be

            //Set in the distance dictionary the distance to source as zero.
            distances[sourceNode] = 0;

            //Set the property Distance to this of the node itself as 0.
            //This will also reset any previous values of DistanceToThis.
            sourceNode.DistanceToThis = 0;

            //Add source to to priority queue.
            distQueue.Enqueue(sourceNode);

            //Initialize the the distances to nodes.
            foreach (var node in Nodes)
            {
                if (node.Value != sourceNode)
                {
                    //Set the DistanceToThis property of the current node to infinity.
                    node.Value.DistanceToThis = int.MaxValue;

                    //Set the value in the distances dictionary.
                    distances[node.Value] = int.MaxValue;

                    //Enqueue the current node in the priority queue.
                    distQueue.Enqueue(node.Value);
                }
            }

            //Finding the shortest distances can now begin. 

            //The following while loop runs while the priority queue has element.
            //Once it is empty that means we have found the shortest path to all nodes.
            while (distQueue.Count() > 0)
            {
                //Get the value of the path to the node to which the distance is the shortest.
                int currentMin = distQueue.Front().DistanceToThis;

                //The the actual node.
                WeightedUndirectedGraphNode<T> currentVertex = distQueue.Front();

                //Remove that node from the priority queue as it has now been visited.
                distQueue.Dequeue();

                //Now each neighbor of the current node is visited.
                foreach (var neighbor in currentVertex.GetNeighbors())
                {
                    //Calculate an alternative distance to current neighbor.
                    //The formula is as follows:
                    //altDistance = (cost of the path to the current node) + (cost of the path to neighbor node);
                    int alternativeDist = currentMin + neighbor.Key.ConnectionWeight;

                    //Now a check must be performed to determine if a better route to current neighbor has been found.
                    //That is why we check the value in the distances dictionary.
                    if (alternativeDist < distances[neighbor.Key.ConnectionNeighbor])
                    {
                        //First change the value in the distances dictionary.
                        distances[neighbor.Key.ConnectionNeighbor] = alternativeDist;

                        //Change the value of DistanceToThis of the neighbor node.
                        neighbor.Key.ConnectionNeighbor.DistanceToThis = alternativeDist;

                        //Resort the distance queue,
                        //as the values have now changed and a smaller value is in the queue.
                        distQueue.Sort();
                    }

                    //Repeat the steps above for each neighbor 
                }

                //Repeat the steps above for each element in the priority queue.
            }

            //Return the final dictionary of distances.
            return distances;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Get first row of input
            int[] discoPathInfo = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse);

            //First number is the number of distinct nodes in the graph.
            int numberOfDiscos = discoPathInfo[0];

            //Number of known paths between nodes in the graph.
            int numberOfKnownRoads = discoPathInfo[1];

            //Create the graph with the first node being 0.
            WeightedUndirectedGraph<int> discoGraph = new WeightedUndirectedGraph<int>(0);

            //Adding all the connections and nodes in the graph.
            for (int i = 0; i < numberOfKnownRoads; i++)
            {
                //Get row of input.
                int[] discoInfo = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse);

                //First number is one node.
                int dicsoOne = discoInfo[0];

                //Second number is the other node.
                int discoTwo = discoInfo[1];

                //Third number is the weight of the connection between two nodes.
                int connectionWeight = discoInfo[2];

                //Add the edge to the graph.
                discoGraph.AddEdge(dicsoOne, discoTwo, connectionWeight);

                //Repeat previous steps for each line of input.
            }

            //Get the number of discos that are in the graph.
            int numberOfDiscosOfInterest = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse)[0];

            //Get the line that contains the numbers that tell us which nodes are discos. By default all others are dorm rooms.
            int[] numbers = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse);

            foreach (var number in numbers)
            {
                int dummy = -1;
                discoGraph.AddEdge(-1, number, 0);
            }

            var results = discoGraph.FindPath(-1);

            //Get the line that tells us how many requests for distances we will receive.
            int numberOfRequests = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse)[0];

            //Processing requests.
            for (int i = 0; i < numberOfRequests; i++)
            {
                //Get the position the request is coming from.
                int requestPosition = Array.ConvertAll(Console.ReadLine()?.Trim(' ').Split(), int.Parse)[0];

                //Get the dictionary containing the results. 

                //After results have been received finding the closest disco takes place.

                //minDist set to max int value for the comparison to work.
                int minDist = int.MaxValue;

                //For each known disco check the distance

                //Get the distance to current disco.
                int discoDist = results[discoGraph.Nodes[requestPosition]];




                //Write out the smallest distance for this request.
                Console.WriteLine(discoDist);
            }
        }
    }
}

