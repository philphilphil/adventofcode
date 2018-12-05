using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] inputFile = File.ReadAllText(Directory.GetCurrentDirectory() + @"\input.txt").ToCharArray();

            //Part 1
            int answer1 = ReactPolymer(inputFile);

            //Part 2
            Dictionary<char, int> unitTypes = GenerateUnitTypeDict();
            int answer2 = GetShortestReactedPolymer(inputFile, unitTypes);

            Console.WriteLine(answer1);
            Console.WriteLine(answer2);
            Console.Read();
        }

        private static int GetShortestReactedPolymer(char[] inputFile, Dictionary<char, int> unitTypes)
        {
            Dictionary<char, int> reactedUnitTypes = new Dictionary<char, int>();
            foreach (var item in unitTypes)
            {
                if (inputFile.Contains(item.Key))
                {
                    //remove all instances of this from the input
                    var cleanedInput = inputFile.Where((val, id) => val != item.Key && val != Char.ToUpper(item.Key)).ToArray();
                    reactedUnitTypes.Add(item.Key, ReactPolymer(cleanedInput));
                }
            }

            return reactedUnitTypes.Where(x => x.Value != 0).OrderBy(x => x.Value).Select(x => x.Value).First();
        }

        private static int ReactPolymer(char[] inputFile)
        {
            int i = 0;
            int endOfFile = inputFile.Length;
            while (true)
            {
                //if entire file was searched and none was found we are done
                if (i != 0 && i == inputFile.Length - 1)
                    break;

                var currentChar = inputFile[i];
                var nextChar = inputFile[i + 1];

                //If upper, check if next Char is same char in lower
                if (Char.IsUpper(currentChar))
                {
                    if (Char.IsLower(nextChar) && Char.ToUpper(nextChar) == currentChar)
                    {
                        //match found, remove both chars
                        inputFile = inputFile.Where((val, id) => id != i && id != (i + 1)).ToArray();
                        i = 0;
                        continue;
                    }
                }

                //other way arround
                if (Char.IsLower(currentChar))
                {
                    if (Char.IsUpper(nextChar) && Char.ToLower(nextChar) == currentChar)
                    {
                        //match found, remove both chars
                        inputFile = inputFile.Where((val, id) => id != i && id != (i + 1)).ToArray();
                        i = 0;
                        continue;
                    }

                }
                i++;
            }

            return inputFile.Length;
        }

        private static Dictionary<char, int> GenerateUnitTypeDict()
        {
            Dictionary<char, int> d = new Dictionary<char, int>();
            string allUnitTypes = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in allUnitTypes)
            {
                d.Add(c, 0);
            }
            return d;
        }
    }
}
