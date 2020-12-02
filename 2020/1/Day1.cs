using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day1 : TaskBase
    {
        public Day1()
        {
            base.ReadInput(1);
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
            foreach (var a in Input)
            {
                foreach (var b in Input)
                {
                    if (int.Parse(a) + int.Parse(b) == 2020)
                    {
                        Console.Write(String.Format("Its {0} and {1}, multiplied thei are {2}", a, b, int.Parse(a) * int.Parse(b)));
                        return;
                    }
                }
            }
        }

        public void GetResultPart2()
        {
            foreach (var a in Input)
            {
                foreach (var b in Input)
                {
                    foreach (var c in Input)
                    {
                        if (int.Parse(a) + int.Parse(b) + int.Parse(c) == 2020)
                        {
                            Console.Write(String.Format("Its {0} and {1}, multiplied thei are {2}", a, b, int.Parse(a) * int.Parse(b) * int.Parse(c)));
                            return;
                        }
                    }
                }
            }
        }
    }
}
