using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day3 : TaskBase
    {
        public Day3()
        {
            base.ReadInput(3);
        }

        public void GetResults()
        {
            GetResultPart1();
            Console.WriteLine();
            GetResultPart2();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void GetResultPart1()
        {
            int pos = 0;
            int trees = 0;

            foreach (string checkLine in Input)
            {
                List<char> chars = checkLine.ToCharArray().ToList();

                int originalPos = pos;
                if (chars.Count <= pos) //reset the pos, strings are the same
                {
                    pos = pos % chars.Count;
                }

                if (chars[pos] == '#')
                    trees++;

                pos = originalPos + 3;
            }

            Console.Write(String.Format("Part 1 trees on the way: {0}", trees.ToString()));
        }

        private void GetResultPart2()
        {
            //Right 1, down 1.
            //Right 3, down 1. (This is the slope you already checked.)           
            //Right 5, down 1.
            //Right 7, down 1.
            //Right 1, down 2.
            List<long> slopeResults = new List<long>();
            List<Tuple<int, int>> slopes = new List<Tuple<int, int>>();
            slopes.Add(new Tuple<int, int>(1, 1));
            slopes.Add(new Tuple<int, int>(3, 1));
            slopes.Add(new Tuple<int, int>(5, 1));
            slopes.Add(new Tuple<int, int>(7, 1));
            slopes.Add(new Tuple<int, int>(1, 2));

            foreach (var slope in slopes)
            {
                int pos = 0;
                int trees = 0;
                int currentLine = 0;

                foreach (string checkLine in Input)
                {

                    if (currentLine % slope.Item2 != 0)
                    {
                        currentLine++;
                        continue;
                    }

                    currentLine++;
                    List<char> chars = checkLine.ToCharArray().ToList();

                    int originalPos = pos;
                    if (chars.Count <= pos) //reset the pos, strings are the same
                    {
                        pos = pos % chars.Count;
                    }

                    if (chars[pos] == '#')
                        trees++;

                    pos = originalPos + slope.Item1;
                }

                slopeResults.Add((uint)trees);
            }
            long calc = slopeResults[0] * slopeResults[1] * slopeResults[2] * slopeResults[3] * slopeResults[4];
            Console.Write(String.Format("Part 2 result: {0}", calc.ToString()));
        }
    }
}
