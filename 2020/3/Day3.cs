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
            //  int line = 0;
            int pos = 0;
            int trees = 0;
            int posMultiplier = 1;

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

        }
    }
}
