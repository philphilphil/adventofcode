using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day9 : TaskBase
    {
        public Day9()
        {
            base.ReadInput(9);
            base.InputToLong();
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
            for (int i = 25; i < Input.Count; i++)
            {
                bool numberFound = CheckIfNumberInPrev25Numbers(i);

                if (!numberFound)
                {
                    Console.WriteLine("Part 1, first number: " + Input[i]);

                    return;
                }
            }
        }

        private bool CheckIfNumberInPrev25Numbers(int i)
        {
            List<long> numberRange = InputAsLong.Skip(i - 25).Take(25).ToList();

            for (int i2 = 0; i2 < numberRange.Count(); i2++)
            {
                for (int i3 = 0; i3 < numberRange.Count(); i3++)
                {
                    if (numberRange[i2] + numberRange[i3] == InputAsLong[i])
                        return true;
                }
            }

            return false;
        }

        public void GetResultPart2()
        {
            long invalidNumber = 22477624;
            long calc = 0;
            int foundRangeStart = 0, foundRangeEnd = 0;

            for (int i = 0; i < InputAsLong.Count(); i++)
            {
                calc = InputAsLong[i];

                for (int i2 = i + 1; i2 < InputAsLong.Count(); i2++)
                {
                    calc += InputAsLong[i2];

                    if (calc >= invalidNumber)
                    {
                        foundRangeEnd = i2;
                        break;

                    }
                }

                if (calc == invalidNumber)
                {
                    foundRangeStart = i;
                    break;
                }
            }

            long lowestNumber = InputAsLong.Skip(foundRangeStart).Take(foundRangeEnd- foundRangeStart).Min(x => x);
            long highestNumber = InputAsLong.Skip(foundRangeStart).Take(foundRangeEnd- foundRangeStart).Max(x => x);

            Console.WriteLine("Part 2 Result " + (lowestNumber + highestNumber));
        }
    }
}
