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
                        i = CalculateNumber(i, opp, value) - 1;
                        break;
                    case "nop":
                        continue;
                    default:
                        break;
                }
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
            string pattern = @"(jmp|acc|nop) (\+|-)(\d*)";
            int result = 0;


            for (int i = 0; i < Input.Count; i++)
            {
                var match = Regex.Match(Input[i], pattern);
                var instruction = match.Groups[1].Value;
                var opp = match.Groups[2].Value;
                var value = int.Parse(match.Groups[3].Value);

                switch (instruction)
                {
                    case "acc":
                        continue;
                    case "jmp":
                        result = RunBootCodeWithChangedParameter(i, "nop");
                        break;
                    case "nop":
                        result = RunBootCodeWithChangedParameter(i, "jmp");
                        break;
                    default:
                        break;
                }

                if (result != 0)
                    Console.Write(String.Format("Part 2 global accumulator is {0}", result));

            }
        }

        private int RunBootCodeWithChangedParameter(int switchInstructionLnie, string newInstruction)
        {
            int globalAccumulator = 0;
            string pattern = @"(jmp|acc|nop) (\+|-)(\d*)";
            List<int> cmdsExcecuted = new List<int>();

            for (int i = 0; i < Input.Count; i++)
            {
                if (i == Input.Count - 1) //end reached
                {
                    return globalAccumulator;
                }
                else if (cmdsExcecuted.Count > Input.Count)
                {
                    return 0; //reached loop, break
                }


                cmdsExcecuted.Add(i);

                var match = Regex.Match(Input[i], pattern);
                var instruction = match.Groups[1].Value;
                var opp = match.Groups[2].Value;
                var value = int.Parse(match.Groups[3].Value);

                if (i == switchInstructionLnie)
                    instruction = newInstruction;

                switch (instruction)
                {
                    case "acc":
                        globalAccumulator = CalculateNumber(globalAccumulator, opp, value);
                        break;
                    case "jmp":
                        i = CalculateNumber(i, opp, value) - 1;
                        break;
                    case "nop":
                        continue;
                    default:
                        break;
                }
            }

            return 0;
        }
    }
}