using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_Ex1_ChristmasMarkets
{
    //General node structure not important. 
    public class DirectedGraphNode<T>
    {
        private readonly Dictionary<T, DirectedGraphNode<T>> m_Neighbors;
        public T Data { get; set; }

        public bool IsVisited { get; set; }

        public DirectedGraphNode(T data)
        {
            m_Neighbors = new Dictionary<T, DirectedGraphNode<T>>();
            Data = data;
        }

        public DirectedGraphNode(DirectedGraphNode<T> initialNeighbor, T data) : this(data)
        {
            m_Neighbors.Add(data, initialNeighbor);
        }

        public IReadOnlyDictionary<T, DirectedGraphNode<T>> GetNeighbors()
        {
            return m_Neighbors;
        }

        public void AddNeighbor(DirectedGraphNode<T> neighborToAdd)
        {
            if (!m_Neighbors.ContainsKey(neighborToAdd.Data))
            {
                m_Neighbors.Add(neighborToAdd.Data, neighborToAdd);
            }
        }
    }


    class Program
    {
        //Path variable to store the path that I've traversed.
        public static Stack<string> Path = new Stack<string>();

        //Pathfound variable used to indicate if I've found a valid path. Used to avoid further checks.
        public static bool PathFound = false;

        //Program start (Entry point)
        static void Main(string[] args)
        {
            //Graph structure. <Key, Value> pair where Key = graphNode value & Value = the instance of the node.
            Dictionary<string, DirectedGraphNode<string>> holidayMarkets =
                new Dictionary<string, DirectedGraphNode<string>>();

            //Get the number of tickets that you have.
            int numberOfTickets = int.Parse(Console.ReadLine()?.Trim(' ').Split().ToArray()[1]);


            //Loop used to add nodes & connections between nodes in the graph.
            for (int i = 0; i < numberOfTickets; i++)
            {
                //Get console input - the two string values representing towns
                string[] currentTownPair = Console.ReadLine().Trim(' ').Split();


                //Check if graph contains the first town
                if (!holidayMarkets.ContainsKey(currentTownPair[0]))
                {
                    //Add it if its not in the graph.
                    holidayMarkets[currentTownPair[0]] = new DirectedGraphNode<string>(currentTownPair[0]);
                }

                //Check if graph contains the second town
                if (!holidayMarkets.ContainsKey(currentTownPair[1]))
                {
                    //Add it if its not in the graph.
                    holidayMarkets[currentTownPair[1]] = new DirectedGraphNode<string>(currentTownPair[1]);
                }

                //Add the connection between the two towns (check graph node class above for more info)
                holidayMarkets[currentTownPair[0]].AddNeighbor(holidayMarkets[currentTownPair[1]]);
            }

            //Read the starting city from the console.
            string startingCity = Console.ReadLine()?.Trim(' ');

            //Check if the holiday market has elements. Return if it doesn't.
            if (holidayMarkets.Count == 0)
            {
                return;
            }

            //Check if the graph has the given starting city. If it doesn't return -1.
            if (!holidayMarkets.ContainsKey(startingCity))
            {
                Console.WriteLine("-1");

            }

            //Check if the path variable has the starting city. If it doesn't add it.
            if (!Path.Contains(holidayMarkets[startingCity].Data))
            {
                Path.Push(holidayMarkets[startingCity].Data);
            }

            //Run DFS(SearchPath) for the starting city.
            SearchPath(holidayMarkets, holidayMarkets[startingCity], startingCity);

            if (!PathFound)
            {
                Console.WriteLine("-1");
            }
        }


        /// <summary>
        /// Search path - DFS algorithm to determine if there is a path from starting city to itself.
        /// </summary>
        /// <param name="nodes">Graph to search in</param>
        /// <param name="node">Node object to start from</param>
        /// <param name="startCity">Node name to start from</param>
        public static void SearchPath(Dictionary<string, DirectedGraphNode<string>> nodes, DirectedGraphNode<string> node, string startCity)
        {
            //Get the list of neighbors of the starting node.
            //And for each neighbor starting from the first one perform the following instructions
            //GetNeighbors returns a dictionary <string, DirectedGraphNode<string>>
            foreach (var neighbor in node.GetNeighbors())
            {
                //Check if the first neighbor is visited
                if (!neighbor.Value.IsVisited)
                {
                    //Check if path contains the data of the current neighbor (the value of the node)
                    if (!Path.Contains(neighbor.Value.Data))
                    {
                        //If it doesn't add the data to the path of visited nodes.
                        Path.Push(neighbor.Value.Data);
                    }

                    //Set the neighbor as visited.
                    neighbor.Value.IsVisited = true;

                    //Call SearchPath recursively for this neighbor. 
                    SearchPath(nodes, neighbor.Value, startCity);
                }

                //If node is visited perform the following checks.

                //Check if the current node is the starting city and check if a path hasn't already been found.
                if (node.Data == startCity && !PathFound)
                {
                    //If both conditions are true then a path has been found and can be written to console.
                    Console.WriteLine(string.Join(" ", Path.Reverse()) + " " + startCity);
                    //Because we are in recursion this value must be set to false in order to avoid printing more info to the console.
                    PathFound = true;
                    return;
                }

                //Independent from the node being visited this row is reached when the node in question has no more unvisited neighbors 
                //Or has no neighbors. This is where we remove from the Path variable the nodes that are not important to the final answer.
                /*
                 *For example we are at node NYC it has 2 kids PAR and TOK. We go down TOK and the path becomes: NYC -> TOK ->LAX->SFO & SFO has no kids.
                 * What we want to do is go back to NYC to check the path through PAR but we dont want TOK->LAX->SFO in the path of visited nodes because
                 * its not a correct path. That is why we pop from the stack.
                 * So we get to SFO and reach this line, we Pop it from the stack and  go back to LAX, we pop it as well and so on until we get to NYC
                 * we dont pop that. And then we go down the route via PAR.
                 */
                if (Path.Count > 0 && Path.Peek() != node.Data && !PathFound)
                {
                    Path.Pop();
                }
            }
        }
    }
}
