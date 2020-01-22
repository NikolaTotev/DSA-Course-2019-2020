using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_1_Paris_G4G
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfTests = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse)[0];

            for (int i = 0; i < numberOfTests; i++)
            {
                int result = 0;
                int numberOfNumbers = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse)[0];
                List<int> numbers = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse).ToList();
                numbers.Sort();

                int itterationCounter = 0;
                if (numberOfNumbers % 2 == 0)
                {
                    for (int j = 0; itterationCounter < (numberOfNumbers) / 2; j += 2)
                    {
                        if (Math.Abs(numbers[j]) == Math.Abs(numbers[j + 1]))
                        {
                            result += 0;
                        }
                        else
                        { 
                            result += numbers[j] * numbers[j + 1];
                        }
                        itterationCounter++;
                    }
                }

                if (numberOfNumbers % 2 != 0)
                {
                    result += numbers[0];
                    for (int j = 1; itterationCounter < (numberOfNumbers) / 2; j += 2)
                    {
                        if (Math.Abs(numbers[j]) == Math.Abs(numbers[j + 1]))
                        {
                            result += 0;
                        }
                        else
                        {
                            result += numbers[j] * numbers[j + 1];
                        }
                        itterationCounter++;
                    }
                }
                Console.WriteLine(result.ToString());

            }

        }
    }
}