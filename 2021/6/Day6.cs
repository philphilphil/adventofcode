using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day6 : TaskBase
    {

        private bool Demo { get; set; }

        private List<int> Fishes { get; set; }

        public Day6(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(6, Demo);
            this.Fishes = InputAsString[0].Split(",").Select(int.Parse).ToList();
        }

        internal void GetResults()
        {
            //GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            int daysToSimulate = 80;

            for (int i = 0; i < daysToSimulate; i++)
            {
                for (int x = 0; x < Fishes.Count(); x++)
                {
                    if (Fishes[x] == 0) {
                        Fishes[x] = 6;
                        Fishes.Add(9); // 9 because it will get -- at the end of this day cycle
                        continue;
                    }
                    Fishes[x]--;
                }
            }

            var answer = Fishes.Count();
            Console.WriteLine("Part 1 answer: {0}", answer);
            Assert(answer, 393019);
        }

        private new void GetResultPart2()
        {
        int daysToSimulate = 256;

            for (int i = 0; i < daysToSimulate; i++)
            {
                for (int x = 0; x < Fishes.Count(); x++)
                {
                    if (Fishes[x] == 0) {
                        Fishes[x] = 6;
                        Fishes.Add(9); // 9 because it will get -- at the end of this day cycle
                        continue;
                    }
                    Fishes[x]--;
                }
            }

            var answer = Fishes.Count();
            Console.WriteLine("Part 2 answer: {0}", answer);
            //Assert(answer, 26984457539);
        }
    }
}
