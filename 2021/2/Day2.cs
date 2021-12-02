using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day2 : TaskBase
    {
        public Day2()
        {
            base.ReadInput(2);
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        public new void GetResultPart1()
        {
            int horizontalPos = 0;
            int depth = 0;

            foreach (string command in InputAsString)
            {
                string cmd = command.Split(" ")[0];
                int amount = int.Parse(command.Split(" ")[1]);

                switch (cmd)
                {
                    case "forward":
                        horizontalPos += amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(String.Format("Answer: {0}", horizontalPos * depth));
        }

        public new void GetResultPart2()
        {
            int horizontalPos = 0;
            int depth = 0;
            int aim = 0;

            foreach (string command in InputAsString)
            {
                string cmd = command.Split(" ")[0];
                int amount = int.Parse(command.Split(" ")[1]);

                switch (cmd)
                {
                    case "forward":
                        horizontalPos += amount;
                        depth += aim * amount;
                        break;
                    case "down":
                        aim += amount;
                        break;
                    case "up":
                        aim -= amount;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(String.Format("Answer: {0}", horizontalPos * depth));
        }
    }
}
