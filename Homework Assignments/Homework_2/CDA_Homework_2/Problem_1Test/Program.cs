using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Numerics;

namespace Problem_1Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int wordLength;
            string wordOne;
            string wordTwo;

            wordLength = Convert.ToInt32(Console.ReadLine());
            wordOne = Console.ReadLine();
            wordTwo = Console.ReadLine();


            string stringOneSorted = "";
            string stringTwoSorted = "";

            //if (wordLength==0)
            //{
            //    Console.WriteLine("yes");
            //}

            List<Int64> letterCount1 = new List<Int64>();


            for (int i = 0; i < 26; i++)
            {
                letterCount1.Add(0);
            }

            for (int i = 0; i < wordLength; i++)
            {
                int index = char.ToUpper(Convert.ToChar(wordOne[i])) - 64;
                letterCount1[index-1]++;
            }


            List<Int64> letterCount2 = new List<Int64>();

            for (int i = 0; i < 26; i++)
            {
                letterCount2.Add(0);
            }

            for (int i = 0; i < wordLength; i++)
            {
                int index = char.ToUpper(Convert.ToChar(wordTwo[i])) - 64;
                letterCount2[index-1]++;
            }

            for (int i = 0; i < 26; i++)
            {
                if (letterCount1[i] != letterCount2[i])
                {
                    Console.Write("no");
                    return;
                }
            }



            Console.Write("yes");

        }//Start
    }
}


/*
 * static void Main(string[] args)
        {
            int wordLength;
            string wordOne;
            string wordTwo;

            wordLength = Convert.ToInt32(Console.ReadLine());
            wordOne = Console.ReadLine();
            wordTwo = Console.ReadLine();

            if (wordLength == 0)
            {
                Console.WriteLine("yes");
                return;
            }
            string stringOneSorted = "";
            string stringTwoSorted = "";
            char[] arr = wordOne.ToCharArray();

            for (int i = 0; i < wordLength-1; i++)
            {
                for (int j = 0; j < wordLength-i-1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        char temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            wordOne = new string(arr);

            arr = wordTwo.ToCharArray();

            for (int i = 0; i < wordLength - 1; i++)
            {
                for (int j = 0; j < wordLength - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        char temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            wordTwo = new string(arr);
            if (wordOne.Equals(wordTwo))
            {
                Console.WriteLine("yes");
                return;
            }
            Console.WriteLine("no");
        }
 */

