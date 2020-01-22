using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_2_Floyd
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');

            int numberOfNodes = Convert.ToInt32(input[0]);
            int numberOfConnections = Convert.ToInt32(input[1]);

            var directedGraph = new int[numberOfNodes, numberOfNodes];

            for (var i = 0; i < numberOfNodes; i++)
            {
                for (var j = 0; j < numberOfNodes; j++)
                {
                    directedGraph[i, j] = int.MaxValue;
                }
            }

            for (var i = 0; i < numberOfNodes; i++)
            {
                directedGraph[i, i] = 0;
            }

            for (var i = 0; i < numberOfConnections; i++)
            {
                input = Console.ReadLine().Split(' ');

                var head = Convert.ToInt32(input[0]);
                var tail = Convert.ToInt32(input[1]);
                var connectionWeight = Convert.ToInt32(input[2]);

                directedGraph[head - 1, tail - 1] = connectionWeight;
            }

            for (var i = 0; i < numberOfNodes; i++)
            {
                for (var j = 0; j < numberOfNodes; j++)
                {
                    for (var k = 0; k < numberOfNodes; k++)
                    {
                        if (directedGraph[j, k] > (long)directedGraph[j, i] + directedGraph[i, k])
                        {
                            directedGraph[j, k] = directedGraph[j, i] + directedGraph[i, k];
                        }
                    }
                }
            }

            int numberOfQueries = Convert.ToInt32(Console.ReadLine());

            for (var i = 0; i < numberOfQueries; i++)
            {
                input = Console.ReadLine().Split(' ');

                int startPoint = Convert.ToInt32(input[0]);
                int endPoint = Convert.ToInt32(input[1]);

                if (directedGraph[startPoint - 1, endPoint - 1] == int.MaxValue)
                {
                    Console.WriteLine(-1);
                }
                else
                {
                    Console.WriteLine(directedGraph[startPoint - 1, endPoint - 1]);
                }
            }

        }
    }
}
