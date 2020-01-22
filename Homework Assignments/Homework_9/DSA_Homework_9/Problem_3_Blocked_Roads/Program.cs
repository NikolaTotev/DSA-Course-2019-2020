using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_3_Blocked_Roads
{
    class DisjointSet<T> where T : IComparable
    {
        private Dictionary<T, T> Parent { get; set; }
        public Dictionary<T, int> Rank { get; set; }

        public DisjointSet()
        {
            Parent = new Dictionary<T, T>();
            Rank = new Dictionary<T, int>();
        }


        public void MakeSet(T x)
        {
            Parent[x] = x;
            Rank[x] = 0;
        }

        public T FindSet(T x)
        {
            if (!Parent[x].Equals(x)) // Parent[x] != x
            {
                Parent[x] = FindSet(Parent[x]);
            }

            return Parent[x];
        }
        public void Union(T x, T y)
        {
            T representativeX = FindSet(x);
            T representativeY = FindSet(y);

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

        public T FindImmediateParent(T x)
        {
            return Parent[x];
        }

        public int FindRank(T x)
        {
            return Rank[x];
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            DisjointSet<int> roadInfo = new DisjointSet<int>();

            int[] inputInfo = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
            int numberOfTowns = inputInfo[0];

            for (int i = 1; i < numberOfTowns + 1; i++)
            {
                roadInfo.MakeSet(i);
            }

            int numberOfInputs = inputInfo[1];
            for (int i = 0; i < numberOfInputs; i++)
            {
                int[] pairInfo = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

                if (roadInfo.FindSet(pairInfo[0]) != roadInfo.FindSet(pairInfo[1]))
                {
                    roadInfo.Union(roadInfo.FindSet(pairInfo[0]), roadInfo.FindSet(pairInfo[1]));
                }


            }
            StringBuilder sb = new StringBuilder(1);

            int numberOfQueries = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse)[0];

            for (int i = 0; i < numberOfQueries; i++)
            {
                int[] queryInfo = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

                if (queryInfo[0] == 1)
                {
                    if (roadInfo.FindSet(queryInfo[1]) == roadInfo.FindSet(queryInfo[2]))
                    {
                        sb.Append('1');
                    }
                    else
                    {
                        sb.Append('0');
                    }

                }
                if (queryInfo[0] == 2)
                {
                    if (roadInfo.FindSet(queryInfo[1]) != roadInfo.FindSet(queryInfo[2]))
                    {
                        roadInfo.Union(roadInfo.FindSet(queryInfo[1]), roadInfo.FindSet(queryInfo[2]));
                    }
                }
            }
            Console.Write(sb.ToString());

        }

    }
}
