using System;
using System.Collections.Generic;
using System.Linq;

namespace Trie
{
    class Program
    {
        static void Main(string[] args)
        {
            //get the input....
            string initValues = Console.ReadLine();
            if (string.IsNullOrEmpty(initValues))
            {
                return;
            }
            string initFixed = initValues.Trim();

            string[] initNumbers = initFixed.Split(' ');
            int[] numbersConverted = Array.ConvertAll(initNumbers, int.Parse);

            int numberOfWords = numbersConverted[0];
            int numberOfBeginnings = numbersConverted[1];

            if (numberOfBeginnings <= 0)
            {
                return;
            }

            string availableWords = null;

            if (numberOfWords > 0)
            {
                availableWords = Console.ReadLine();
            }

            int[] results = new int[numberOfBeginnings];
            results.Initialize();

            if (string.IsNullOrEmpty(availableWords))
            {
                Console.Write(string.Join(Environment.NewLine, results));
                return;
            }

            string[] words = availableWords.Split(' ');
            int maxIndexLength = 10;

            Dictionary<string, List<string>> trie = new Dictionary<string, List<string>>(numberOfWords);
            foreach (string word in words)
            {
                for (int i = 0; i < Math.Min(maxIndexLength, word.Length); i++)
                {
                    string index = word.Substring(0, i + 1);

                    List<string> indexWords;
                    if (trie.TryGetValue(index, out indexWords))
                    {
                        indexWords.Add(word)
;
                    }
                    else
                    {
                        indexWords = new List<string>();
                        indexWords.Add(word)
;
                        trie.Add(index, indexWords);
                    }
                }
            }

            List<string> prefixes = new List<string>();

            for (int i = 0; i < numberOfBeginnings; i++)
            {
                string prefixToCheck = Console.ReadLine();
                if (string.IsNullOrEmpty(prefixToCheck))
                {
                    results[i] = 0;
                    continue;
                }

                if (prefixToCheck.Length == 0)
                {
                    results[i] = 0;
                    continue;
                }

                string indexToCheck = prefixToCheck.Substring(0, Math.Min(maxIndexLength, prefixToCheck.Length));

                List<string> possibleWords;
                if (trie.TryGetValue(indexToCheck, out possibleWords))
                {
                    if (prefixToCheck.Length == indexToCheck.Length)
                    {
                        results[i] = possibleWords.Count;
                    }
                    else
                    {
                        results[i] = possibleWords.Count(item => item.StartsWith(prefixToCheck));
                    }
                }
                else
                {
                    results[i] = 0;
                }
            }

            Console.Write(string.Join(Environment.NewLine, results));
        }
    }
}


