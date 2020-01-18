using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Problem_1_AutoComplete
{
    class TrieNode
    {
        private TrieNode[] chidren;
        private int numberOfSubWords;

        public int NumberOfSubWords { get => numberOfSubWords; set => numberOfSubWords = value; }

        public TrieNode[] Children
        {
            get => chidren;
            set => chidren = value;
        }

        public TrieNode()
        {
            initList();
            numberOfSubWords = 0;
        }

        public TrieNode(char data)
        {
            initList();
            numberOfSubWords = 0;
        }

        private void initList()
        {
            TrieNode[] arr = new TrieNode[26];
            arr.Initialize();
            chidren = arr;
        }
    }

    class Trie
    {
        private TrieNode root;

        bool wordExists(string word)
        {
            TrieNode currentNode = root;
            for (int i = 0; i < word.Length; i++)
            {
                char currentLetter = word[i];
                int letterIndex = currentLetter - 'a';


                if (currentNode.Children[letterIndex] == null)
                {
                    return false;
                }

                currentNode = currentNode.Children[letterIndex];
            }
            return true;
        }

        int countWords(string prefix)
        {
            TrieNode currentNode = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                char currentLetter = prefix[i];
                int letterIndex = currentLetter - 'a';


                if (currentNode.Children[letterIndex] == null)
                {
                    return 0;
                }

                currentNode = currentNode.Children[letterIndex];
            }
            return currentNode.NumberOfSubWords;
        }

        void insertWord(string word, bool isWordIn)
        {
            TrieNode currentNode = root;

            if (isWordIn)
            {
                for (int i = 0; i < word.Length; i++)
                {

                    char currentLetter = word[i];
                    int letterIndex = currentLetter - 'a';
                    currentNode = currentNode.Children[letterIndex];
                    currentNode.NumberOfSubWords++;
                }

                return;
            }
            for (int i = 0; i < word.Length; i++)
            {
                char currentLetter = word[i];
                int letterIndex = currentLetter - 'a';

                if (currentNode.Children[letterIndex] == null)
                {
                    currentNode.Children[letterIndex] = new TrieNode(word[i]);

                }

                currentNode = currentNode.Children[letterIndex];
                currentNode.NumberOfSubWords++;
            }
        }
        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word)
        {
            bool doesWordExists = wordExists(word);
            if (doesWordExists)
            {

            }
            insertWord(word, doesWordExists);
        }

        public void WordsStartingWith(string prefix)
        {
            if (!wordExists(prefix))
            {
                Console.WriteLine("0");
                return;
            }
            int result = countWords(prefix);
            Console.WriteLine(result);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            int numberOfWords;
            int numberOfBeginnings;
            string initValues = Console.ReadLine();
            string initFixed = initValues.Trim(' ');

            string[] initNumbers = initFixed.Split(' ');
            int[] numbersConverted = Array.ConvertAll(initNumbers, int.Parse);

            numberOfWords = numbersConverted[0];
            numberOfBeginnings = numbersConverted[1];


            if (numberOfWords == 0 || numberOfBeginnings == 0)
            {
                Console.WriteLine("0");
            }

            string availableWords = Console.ReadLine();
            string[] words = availableWords.Split();
            Trie trie = new Trie();


            foreach (var word in words)
            {
                trie.Insert(word);
            }

            for (int i = 0; i < numberOfBeginnings; i++)
            {
                string inputBeginning = Console.ReadLine();
                trie.WordsStartingWith(inputBeginning);
            }
        }
    }
}
