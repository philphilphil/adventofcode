using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day15 : TaskBase
    {
        int[,] Map { get; set; }

        int TotalRiskOfPath { get; set; } = 0;

        public Day15(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(15, Demo);

            Map = Helpers.Build2DArray(InputAsString);
        }


        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            (int y, int x) start = (0, 0);
            while (true)
            {
              //  start = FindNextStepFrom(start.y, start.x);
            }

            var answer = 0;
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }

        // private (int x, int y) FindNextStepFrom(int y, int x)
        // {
        //     int top = GetNumberAtPos(y - 1, x);
        //     int left = GetNumberAtPos(y, x - 1);
        //     int right = GetNumberAtPos(y, x + 1);
        //     int bottom = GetNumberAtPos(y + 1, x);

        // }

        private int GetNumberAtPos(int row, int col)
        {
            if (row < 0 || row >= Map.GetLength(0) || col < 0 || col >= Map.GetLength(1))
                return -1;

            return Map[row, col];
        }
        private new void GetResultPart2()
        {
            var answer = 0;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }
    }
}
