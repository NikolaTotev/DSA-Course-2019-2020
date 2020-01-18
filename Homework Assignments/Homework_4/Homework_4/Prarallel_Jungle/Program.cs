using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using longBoi = System.Int32;

namespace Prarallel_Jungle
{
    struct optimalInfo
    {
        public longBoi index;
         public longBoi valueAtIndex;

         public static bool operator <(optimalInfo left, optimalInfo right)
         {
             return left.valueAtIndex < right.valueAtIndex;
         }

         public static bool operator >(optimalInfo left, optimalInfo right)
         {
             return left.valueAtIndex > right.valueAtIndex;
         }

    }
    class Program
    {
        static void Main(string[] args)
        {

            longBoi numberOfTrees;

            numberOfTrees = Convert.ToInt32(Console.ReadLine());
            longBoi optimalIndex = 0;
            Random rand = new Random();
            optimalIndex = rand.Next(1, optimalIndex+1);
            List<longBoi> treeHeights = new List<longBoi>();

            longBoi currentHeight = 0;
            string[] tokens = Console.ReadLine().Split();
            longBoi[] numbers = Array.ConvertAll(tokens, longBoi.Parse);
            for (longBoi i = 0; i < numberOfTrees; ++i)
            {
                currentHeight = numbers[i];
                treeHeights.Add(currentHeight);
            }
        }
    }
}
