using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentFrequency = 0;

            var inputFile = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\input.txt").Select(x => int.Parse(x)).ToList();
            List<int> frequencysFound = new List<int>();

            int i = 0;
            while (true)
            {
                currentFrequency += inputFile[i];

                if (frequencysFound.Contains(currentFrequency))
                {
                    //we found our duplicate frequency
                    break;
                }
                else
                {
                    frequencysFound.Add(currentFrequency);
                }

                //when end of list is reached, start from the beginning
                if (i == inputFile.Count - 1)
                {
                    i = 0;
                    continue;
                }

                i++;
            }

            Console.WriteLine(currentFrequency.ToString());
            Console.Read();
        }
    }
}
