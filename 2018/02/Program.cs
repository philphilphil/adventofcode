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
            GetBoxChecksum();
            GetCorrectBoxId();
            Console.Read();
        }

        private static void GetCorrectBoxId()
        {
            var inputFile = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\input.txt").ToList();
            string foundBoxId = "";

            foreach (string boxId in inputFile)
            {
                if (!String.IsNullOrEmpty(foundBoxId)) break;

                var idChars = boxId.ToCharArray();
                int differenceLocation = 0;

                //compare to all other strings but itself
                foreach (string otherBoxId in inputFile)
                {
                    if (otherBoxId == boxId) continue;

                    //itterate through all chars and check if more than 1 difference
                    int i = 0;
                    int differencesFound = 0;

                    foreach (char c in otherBoxId)
                    {
                        if (c != idChars[i])
                        {
                            differencesFound++;
                            differenceLocation = i;
                        };

                        if (differencesFound > 1) break;
                        i++;
                    }

                    //found the one
                    if (differencesFound == 1)
                    {
                        foundBoxId = boxId.Remove(differenceLocation, 1);
                    }
                }
            }

            Console.WriteLine(foundBoxId);
        }

        private static void GetBoxChecksum()
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
        }
    }
}
