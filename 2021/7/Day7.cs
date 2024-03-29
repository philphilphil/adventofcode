﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

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
            GetResultPart2();
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

                    if (posFuelUsage > lowestFuel) break;
                }

                if (posFuelUsage < lowestFuel)
                    lowestFuel = posFuelUsage;
            }

            Log.Information("Part 1 answer: {0}", lowestFuel);
            var expectedResult = Demo ? 37 : 345197;
            Assert(lowestFuel, expectedResult);
        }

        private new void GetResultPart2()
        {
            var crabPositions = InputAsString[0].Split(",").Select(int.Parse).ToList();

            int lowestFuel = int.MaxValue;
            int maxFishDistance = crabPositions.Max();

            for (int hPos = 0; hPos < maxFishDistance; hPos++)
            {
                int posFuelUsage = 0;
                crabPositions.ForEach(crab =>
                {
                    var movesNeeded = Math.Abs(crab - hPos);
                    posFuelUsage += (movesNeeded * (movesNeeded + 1)) / 2;

                    if (posFuelUsage > lowestFuel) return;
                });

                if (posFuelUsage < lowestFuel) lowestFuel = posFuelUsage;
            }

            Log.Information("Part 2 answer: {0}", lowestFuel);
            var expectedResult = Demo ? 168 : 96361606;
            Assert(lowestFuel, expectedResult);
        }
    }
}
