using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day8 : TaskBase
    {
        public Day8()
        {
            base.ReadInput(8);
        }

        internal void GetResults()
        {
            GetResultPart1();
            Console.WriteLine();
            Console.WriteLine();
            GetResultPart2();
            Console.WriteLine();
        }

        public void GetResultPart1()
        {
            int globalAccumulator = 0;
            string pattern = @"(jmp|acc|nop) (\+|-)(\d*)";
            List<int> cmdsExcecuted = new List<int>();

            for (int i = 0; i < Input.Count; i++)
            {
                if (cmdsExcecuted.Contains(i))
                    break;

                cmdsExcecuted.Add(i);

                //maybe regex is simpler?
                var match = Regex.Match(Input[i], pattern);
                var instruction = match.Groups[1].Value;
                var opp = match.Groups[2].Value;
                var value = int.Parse(match.Groups[3].Value);

                switch (instruction)
                {
                    case "acc":
                        globalAccumulator = CalculateNumber(globalAccumulator, opp, value);
                        break;
                    case "jmp":
                        i = CalculateNumber(i, opp, value)-1;
                        break;
                    case "nop":
                        continue;
                    default:
                        break;
                }

                //Console.WriteLine(instruction + "       " + value + "       " + opp + "       ");

            }

            Console.Write(String.Format("Part 1 global accumulator is {0}", globalAccumulator));

        }

        private int CalculateNumber(int number, string opp, int value)
        {
            if (opp == "+")
            {
                return number + value;
            }
            else if (opp == "-")
            {
                return number - value;
            }

            return 0;
        }

        public void GetResultPart2()
        {
            // foreach (var a in Input)
            // {
            //     foreach (var b in Input)
            //     {
            //         foreach (var c in Input)
            //         {
            //             if (int.Parse(a) + int.Parse(b) + int.Parse(c) == 2020)
            //             {
            //                 Console.Write(String.Format("Its {0} and {1}, multiplied thei are {2}", a, b, int.Parse(a) * int.Parse(b) * int.Parse(c)));
            //                 return;
            //             }
            //         }
            //     }
            // }
        }
    }
}