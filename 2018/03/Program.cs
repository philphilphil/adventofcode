using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            GetOverlappingInches();
            GetTheOneWichDoesntOverlap();
            Console.Read();
        }

        private static void GetTheOneWichDoesntOverlap()
        {
            List<Pattern> patterns = GetPatterns();
            int[,] arr = BuildArray(patterns);
            bool overlappingFound = false;
            //check each pattern if it has overlappings
            foreach (Pattern p in patterns)
            {
                overlappingFound = false;

                for (int i = p.SpaceToLeft; i < p.SpaceToLeft + p.Width; i++)
                {
                    if(overlappingFound) break;

                    for (int a = p.SpaceToTop; a < p.SpaceToTop + p.Height; a++)
                    {
                       if(arr[a, i] != 1) {
                           overlappingFound = true;
                           break;
                       }
                    }
                }

                if(!overlappingFound) {
                    Console.WriteLine(p.Id);
                    break;
                }
            }

        }

        private static void GetOverlappingInches()
        {
            List<Pattern> patterns = GetPatterns();
            int[,] arr = BuildArray(patterns);

            Console.WriteLine(arr.Cast<int>().Where(x => x >= 2).Count());
        }

        private static int[,] BuildArray(List<Pattern> patterns)
        {
            int[,] arr = new int[1000, 1000]; //very big piece of fabric ;)

            foreach (Pattern p in patterns)
            {
                for (int i = p.SpaceToLeft; i < p.SpaceToLeft + p.Width; i++)
                {
                    for (int a = p.SpaceToTop; a < p.SpaceToTop + p.Height; a++)
                    {
                        arr[a, i]++;
                    }
                }
            }

            return arr;
        }

        private static List<Pattern> GetPatterns()
        {
            List<Pattern> patterns = new List<Pattern>();
            var inputFile = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\input.txt").ToList();

            foreach (var line in inputFile)
            {
                Regex regex = new Regex(@"(\d*) @ (\d*),(\d*): (\d*)x(\d*)");
                Match match = regex.Match(line);

                patterns.Add(new Pattern { Id = int.Parse(match.Groups[1].Value), SpaceToLeft = int.Parse(match.Groups[2].Value), SpaceToTop = int.Parse(match.Groups[3].Value), Width = int.Parse(match.Groups[4].Value), Height = int.Parse(match.Groups[5].Value) });
            }

            return patterns;
        }
    }
    class Pattern
    {
        public int Id { get; set; }
        public int SpaceToLeft { get; set; }
        public int SpaceToTop { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
