using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> letterCounts;
            int twoFound = 0;
            int threeFound = 0;

            var inputFile = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\input.txt").ToList();

            foreach (string boxId in inputFile)
            {
                letterCounts = new Dictionary<char, int>();

                foreach (char c in boxId)
                {
                    if (letterCounts.ContainsKey(c))
                    {
                        letterCounts[c]++;
                    }
                    else
                    {
                        letterCounts.Add(c, 1);
                    }
                }

                bool anyWithTwoFound = letterCounts.Where(x => x.Value == 2).Any();
                bool anyWithThreeFound = letterCounts.Where(x => x.Value == 3).Any();

                if (anyWithTwoFound) twoFound++;
                if (anyWithThreeFound) threeFound++;
            }

           // Console.

            Console.WriteLine(twoFound * threeFound);
            Console.Read();
        }
    }
}
