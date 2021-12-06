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

            this.Fishes = InputAsString[0].Split(",").Select(int.Parse).ToList(); //reload input
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
            List<long> fishState = new List<long> { 0, 0, 0, 0, 0, 0, 0, 0, 0};

            foreach (var fish in Fishes)
            {
                fishState[fish]++;
            }

            for (int x = 0; x < daysToSimulate; x++)
            {
                var fish0a = fishState[0];
                fishState[0] = fishState[1];
                fishState[1] = fishState[2];
                fishState[2] = fishState[3];
                fishState[3] = fishState[4];
                fishState[4] = fishState[5];
                fishState[5] = fishState[6];
                fishState[6] = fishState[7] + fish0a;
                fishState[7] = fishState[8];
                fishState[8] = fish0a;
            }

            var answer = fishState.Sum();
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 26984457539 : 1757714216975;
            Assert(answer, expectedResult);
        }
    }
}
