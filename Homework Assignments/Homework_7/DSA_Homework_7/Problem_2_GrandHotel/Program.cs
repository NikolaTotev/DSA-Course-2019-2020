using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongBoi = System.Int32;
namespace Problem_2_GrandHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<LongBoi> roomKeys = new List<long>();
           // List<LongBoi> doorKeys = new List<long>();

            LongBoi numberOfRooms;
            string[] initNumbers = Console.ReadLine().Trim(' ').Split(' ');
            LongBoi[] numbersConverted = Array.ConvertAll(initNumbers, int.Parse);
            numberOfRooms = numbersConverted[0];

            string [] roomKeyInput = Console.ReadLine().Trim(' ').Split(' ');
            List<LongBoi> roomKeys = Array.ConvertAll(roomKeyInput, int.Parse).ToList();

            string[]  doorKeyInput = Console.ReadLine().Trim(' ').Split(' ');
            List<LongBoi> doorKeys = Array.ConvertAll(doorKeyInput, int.Parse).ToList();

           
            LongBoi minimumNumberOfKeys = 0;
            Dictionary<LongBoi,LongBoi> currentKeys = new Dictionary<int, int>();
            for (int i = 0; i < doorKeys.Count; i++)
            {
                if (!currentKeys.ContainsKey(roomKeys[i]))
                {
                    currentKeys.Add(roomKeys[i], 1);
                }
                else
                {
                    currentKeys[roomKeys[i]]++;
                }
                
                LongBoi keyNeededForDoor = doorKeys[i];
                if (currentKeys.ContainsKey(keyNeededForDoor))
                {
                    if (currentKeys[keyNeededForDoor]==1)
                    {
                        currentKeys.Remove(keyNeededForDoor);
                    }
                    else
                    {
                        currentKeys[keyNeededForDoor]--;
                    }
                }
                else
                {
                    minimumNumberOfKeys++;
                }
            }

            Console.WriteLine(minimumNumberOfKeys);
        }
    }
}
