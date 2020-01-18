using System;
using System.Collections.Generic;

namespace Darts
{
    class Program
    {

        static void Main(string[] args)
        {
            if (!int.TryParse(Console.ReadLine(), out int neededPoints))
            {
                Console.WriteLine(0);
                return;
            }

            int[] validPoints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 24, 25, 26, 27, 28, 30, 32, 33, 34, 36, 38, 39, 40, 42, 45, 48, 50, 51, 54, 57, 60 };
            List<int> doublePoints = new List<int>() { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 50 };


            int numberOfOptions = 0;
            List<string> displayOptions = new List<string>();

            foreach (int lastPoint in doublePoints)
            {

                //with single arrow (and 0s)
                if (lastPoint == neededPoints)
                {
                    numberOfOptions += 3;
                    displayOptions.Add($"D{lastPoint / 2}");
                    displayOptions.Add($"0, D{lastPoint / 2}");
                    displayOptions.Add($"0, 0, D{lastPoint / 2}");
                }
                else
                {
                    int diff = neededPoints - lastPoint;
                    if (diff < 0 || diff > 120)
                    {
                        //no possible combinations
                        continue;
                    }

                    if (IsValidSingle(diff))
                    {
                        //can be done with 2 darts (and 0s)
                        numberOfOptions += 3;
                        displayOptions.Add($"S{diff}, D{lastPoint / 2}");
                        displayOptions.Add($"0, S{diff}, D{lastPoint / 2}");
                        displayOptions.Add($"S{diff}, 0, D{lastPoint / 2}");
                    }

                    if (IsValidDouble(diff))
                    {
                        //can be done with 2 darts (and 0s)
                        numberOfOptions += 3;
                        displayOptions.Add($"D{diff / 2}, D{lastPoint / 2}");
                        displayOptions.Add($"0, D{diff / 2}, D{lastPoint / 2}");
                        displayOptions.Add($"D{diff / 2}, 0, D{lastPoint / 2}");
                    }

                    if (IsValidTriple(diff))
                    {
                        //options with 2 darts (and 0s)
                        numberOfOptions += 3;
                        displayOptions.Add($"T{diff / 3}, D{lastPoint / 2}");
                        displayOptions.Add($"0, T{diff / 3}, D{lastPoint / 2}");
                        displayOptions.Add($"T{diff / 3}, 0, D{lastPoint / 2}");
                    }


                    foreach (int firstPoint in validPoints)
                    {
                        int secondPoint = diff - firstPoint;
                        if (IsNotValid(secondPoint))
                        {
                            //no possible combinations
                            continue;
                        }

                        //options with 3 darts
                        if (IsValidSingle(firstPoint))
                        {
                            if (IsValidSingle(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"S{firstPoint}, S{secondPoint}, D{lastPoint / 2}");
                            }

                            if (IsValidDouble(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"S{firstPoint}, D{secondPoint / 2}, D{lastPoint / 2}");
                            }

                            if (IsValidTriple(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"S{firstPoint}, T{secondPoint / 3}, D{lastPoint / 2}");
                            }

                        }


                        if (IsValidDouble(firstPoint))
                        {
                            if (IsValidSingle(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"D{firstPoint / 2}, S{secondPoint}, D{lastPoint / 2}");
                            }

                            if (IsValidDouble(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"D{firstPoint / 2}, D{secondPoint / 2}, D{lastPoint / 2}");
                            }

                            if (IsValidTriple(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"D{firstPoint / 2}, T{secondPoint / 3}, D{lastPoint / 2}");
                            }

                        }

                        if (IsValidTriple(firstPoint))
                        {
                            if (IsValidSingle(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"T{firstPoint / 3}, S{secondPoint}, D{lastPoint / 2}");
                            }

                            if (IsValidDouble(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"T{firstPoint / 3}, D{secondPoint / 2}, D{lastPoint / 2}");
                            }

                            if (IsValidTriple(secondPoint))
                            {
                                numberOfOptions++;
                                displayOptions.Add($"T{firstPoint / 3}, T{secondPoint / 3}, D{lastPoint / 2}");
                            }
                        }
                    }
                }
            }

            /*for (var index = 0; index < displayOptions.Count; index++)
            {
                string option = displayOptions[index];
                Console.WriteLine(option);
            }*/


            //Console.WriteLine($"Number of possible options: {numberOfOptions}");
            Console.WriteLine(numberOfOptions);
            //Console.ReadKey();
        }

        static bool IsNotValid(int point)
        {
            return !IsValidSingle(point) && !IsValidDouble(point) && !IsValidTriple(point);
        }

        static bool IsValidSingle(int point)
        {
            return (point >= 1 && point <= 20) || (point == 25);
        }

        static bool IsValidDouble(int point)
        {
            return point <= 50 && point % 2 == 0 && IsValidSingle(point / 2);
        }

        static bool IsValidTriple(int point)
        {
            return point <= 60 && point % 3 == 0 && IsValidSingle(point / 3);
        }


    }
}