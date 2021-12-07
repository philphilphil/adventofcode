using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day7 : TaskBase
    {
        public List<int> CrabPositions { get; set; }

        public Day7(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(7, Demo);
            this.CrabPositions = InputAsString[0].Split(",").Select(int.Parse).ToList();
        }

        internal void GetResults()
        {
            GetResultPart1();
            //GetResultPart2();
        }

        private new void GetResultPart1()
        {

            int lowestFuel = int.MaxValue;
            int maxFishDistance = CrabPositions.Max();
            for (int hPos = 0; hPos < maxFishDistance; hPos++)
            {
                int posFuelUsage = 0;
                foreach (int crab in CrabPositions)
                {
                    if (crab > hPos)
                    {
                        posFuelUsage += crab - hPos;
                    }
                    else if (crab < hPos)
                    {
                        posFuelUsage += hPos - crab; ;
                    }
                }

                if (posFuelUsage < lowestFuel)
                    lowestFuel = posFuelUsage;
            }

            var answer = lowestFuel;
            Console.WriteLine("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 37 : 345197;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {
            var answer = "answer";
            Console.WriteLine("Part 2 answer: {0}", "answer");
            var expectedResult = Demo ? "a" : "b";
            Assert(answer, expectedResult);
        }
    }
}
