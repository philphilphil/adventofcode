using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

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
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            int daysToSimulate = 80;

            for (int i = 0; i < daysToSimulate; i++)
            {
                for (int x = 0; x < Fishes.Count(); x++)
                {
                    if (Fishes[x] == 0)
                    {
                        Fishes[x] = 6;
                        Fishes.Add(9); // 9 because it will get -- at the end of this day cycle
                        continue;
                    }
                    Fishes[x]--;
                }
            }

            var answer = Fishes.Count();
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 5934 : 393019;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {
            int daysToSimulate = 256;

            this.Fishes = InputAsString[0].Split(",").Select(int.Parse).ToList();
            long fish0 = Fishes.Where(x => x == 0).Count();
            long fish1 = Fishes.Where(x => x == 1).Count();
            long fish2 = Fishes.Where(x => x == 2).Count();
            long fish3 = Fishes.Where(x => x == 3).Count();
            long fish4 = Fishes.Where(x => x == 4).Count();
            long fish5 = Fishes.Where(x => x == 5).Count();
            long fish6 = Fishes.Where(x => x == 6).Count();
            long fish7 = Fishes.Where(x => x == 7).Count();
            long fish8 = Fishes.Where(x => x == 8).Count();

            for (int x = 0; x < daysToSimulate; x++)
            {
                var fish0a = fish0;
                fish0 = fish1;
                fish1 = fish2;
                fish2 = fish3;
                fish3 = fish4;
                fish4 = fish5;
                fish5 = fish6;
                fish6 = fish7;
                fish7 = fish8;
                fish8 = 0;

                fish6 += fish0a;
                fish8 += fish0a;
                //fish0 = 0;
            }

            var answer = fish0 + fish1 + fish2 + fish3 + fish4 + fish5 + fish6 + fish7 + fish8;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 26984457539 : 1757714216975;
            Assert(answer, expectedResult);
        }
    }
}
