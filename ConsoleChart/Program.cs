using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleChart
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] splitedLine;
            string toGroup = args[0];
            bool maxRowsGiven = args.Length > 2;
            int maxLinesToShow = -1;
            if (maxRowsGiven) { maxLinesToShow = int.Parse(args[2]); }
            int indexGroup = -1;
            int indexSum = -1;

            int tempOriHeight = Console.WindowHeight;
            Console.SetWindowSize(150, tempOriHeight);

            Dictionary<string, int> grouped = new Dictionary<string, int>();
            Dictionary<string, int> result = new Dictionary<string, int>();

            splitedLine = Console.ReadLine().Split('\t');
            string[] headLine = new string[splitedLine.Length];
            for (int i = 0; i < splitedLine.Length; i++)
            {
                headLine[i] = splitedLine[i];
                if (toGroup == headLine[i]) { indexGroup = i; }
                else if (args[1] == headLine[i]) { indexSum = i; }
            }
            for (int i = 1; true; i++)
            {
                try { splitedLine = Console.ReadLine().Split('\t'); }
                catch { break; }

                if (!grouped.ContainsKey(splitedLine[indexGroup]))
                {
                    grouped.Add(splitedLine[indexGroup], int.Parse(splitedLine[indexSum]));
                }
                else
                {
                    grouped[splitedLine[indexGroup]] += int.Parse(splitedLine[indexSum]);
                }
            }

            var trimmedList = grouped.ToList();
            trimmedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            if (maxRowsGiven)
            {
                if (maxLinesToShow < trimmedList.Count) 
                { 
                    trimmedList.RemoveRange(maxLinesToShow, trimmedList.Count - (maxLinesToShow)); 
                }
            }
            

            //calculating percentages
            int max = trimmedList[0].Value;
            foreach (var item in trimmedList)
            {
                result.Add(item.Key, (100 * item.Value) / max);
            }

            //output
            foreach (var item in result)
            {
                Console.Write($"{item.Key, 41} | ");
                Console.BackgroundColor = ConsoleColor.Red;
                for (int i = 0; i < item.Value; i++)
                {
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
    }
}
