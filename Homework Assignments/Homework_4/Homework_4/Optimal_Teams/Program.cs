using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using longBoi = System.Int32;

struct studentInfo
{
    public longBoi position;
    public longBoi numberOfSkills;

}
namespace Optimal_Teams
{
    class Program
    {
        static void Main(string[] args)
        {
            string initValues = Console.ReadLine();
            string initFixed = initValues.Trim(' ');

            string[] initNumbers = initFixed.Split(' ');
            int[] numbersConverted = Array.ConvertAll(initNumbers, int.Parse);
            longBoi numberOfStudents = numbersConverted[0];
            longBoi selectionRange = numbersConverted[1];

            string rawNumbers = Console.ReadLine();
            string Fixed = rawNumbers.Trim(' ');
            string[] numbers = Fixed.Split(' ');

            List<longBoi> studentSkills = Array.ConvertAll(numbers, longBoi.Parse).ToList();
            List<longBoi> sortedSkills = Array.ConvertAll(numbers, longBoi.Parse).ToList();
            sortedSkills.Sort();

            Dictionary<int, longBoi> studentInfo = new Dictionary<int, longBoi>();
            for (int i = 0; i < numberOfStudents; i++)
            {
                studentInfo.Add(sortedSkills[i], studentSkills.IndexOf(sortedSkills[i]));
            }

            bool isIvansTurn = true;

            List<longBoi> team1 = new List<int>();
            List<longBoi> team2 = new List<int>();

            longBoi studentsPicked = 0;
            longBoi currentMax = sortedSkills[sortedSkills.Count - 1];
            longBoi currentMaxIndex;
            studentInfo.TryGetValue(currentMax, out currentMaxIndex);
            longBoi studentsCurrentlyPicked = 0;
            bool currentMaxChanged = false;
            while (studentsPicked < numberOfStudents)
            {
                if (!team1.Contains(currentMax) && !team2.Contains(currentMax))
                {
                    if (!isIvansTurn)
                    {
                        team2.Add(currentMax);
                    }
                    else
                    {
                        team1.Add(currentMax);
                    }

                    studentsPicked++;
                }

                for (int i = currentMaxIndex + 1; studentsCurrentlyPicked < selectionRange && i < studentSkills.Count; i++)
                {
                    if (!team1.Contains(studentSkills[i]) && !team2.Contains(studentSkills[i]))
                    {
                        if (!isIvansTurn)
                        {
                            team2.Add(studentSkills[i]);

                        }
                        else
                        {
                            team1.Add(studentSkills[i]);
                        }

                        studentsCurrentlyPicked++;
                        studentsPicked++;
                    }
                }

                studentsCurrentlyPicked = 0;
                for (int i = currentMaxIndex - 1; studentsCurrentlyPicked < selectionRange && i >= 0; i--)
                {
                    if (!team1.Contains(studentSkills[i]) && !team2.Contains(studentSkills[i]))
                    {
                        if (!isIvansTurn)
                        {
                            team2.Add(studentSkills[i]);

                        }
                        else
                        {
                            team1.Add(studentSkills[i]);
                        }

                        studentsCurrentlyPicked++;
                        studentsPicked++;
                    }
                }
                studentsCurrentlyPicked = 0;
                currentMaxChanged = false;
                if (studentsPicked < numberOfStudents || !currentMaxChanged)
                {
                    for (int i = sortedSkills.Count - 1; i >= 0; i--)
                    {
                        if (!team1.Contains(sortedSkills[i]) && !team2.Contains(sortedSkills[i]))
                        {
                            if (!currentMaxChanged)
                            {
                                currentMax = sortedSkills[i];
                                currentMaxChanged = true;
                                studentInfo.TryGetValue(currentMax, out currentMaxIndex);
                            }
                        }
                    }
                }

                isIvansTurn = !isIvansTurn;
            }
            foreach (var skill in studentSkills)
            {
                if (team1.Contains(skill))
                {
                    Console.Write(1);
                }
                else
                {
                    Console.Write(2);
                }
            }
        }

    }
}
