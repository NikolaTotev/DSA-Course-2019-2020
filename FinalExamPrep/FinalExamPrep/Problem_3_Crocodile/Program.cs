using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_3_Crocodile
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstSentence = Console.ReadLine().Trim().Split();
            Dictionary<string, int> firstWords = new Dictionary<string, int>();
            foreach (var s in firstSentence)
            {
                if (!firstWords.ContainsKey(s))
                {
                    firstWords.Add(s, 1);
                }
                else
                {
                    firstWords[s]++;
                }
            }


            string[] secondSentence = Console.ReadLine().Trim().Split();
            Dictionary<string, int> secondWords = new Dictionary<string, int>();

            foreach (var s in secondSentence)
            {
                if (!secondWords.ContainsKey(s))
                {
                    secondWords.Add(s,1);
                }
                else
                {
                    secondWords[s]++;
                }
            }

            List<string> differentWords = new List<string>();

            foreach (var s in secondSentence)
            {
                if (!firstWords.ContainsKey(s) && secondWords[s]==1)
                {
                    differentWords.Add(s);
                }
            }

            foreach (var s in firstSentence)
            {
                if (!secondWords.ContainsKey(s) && firstWords[s] == 1)
                {
                    differentWords.Add(s);
                }
            }

            differentWords.Sort();

            foreach (var differentWord in differentWords)
            {
                Console.WriteLine(differentWord);
            }
        }
    }
}
