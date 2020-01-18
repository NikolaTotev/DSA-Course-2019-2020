using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRows = 0;
            numberOfRows = Console.Read();

            Dictionary<int, int> schedule = new Dictionary<int, int>();
            int startTime = 0;
            int activityDuration = 0;
            Console.Clear();

            for (int i = 0; i < numberOfRows; i++)
            {
                int derp = Convert.ToInt32(Console.Read());

                Console.WriteLine(derp);
                
            }

        }
    }
}
