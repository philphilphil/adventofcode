using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day9 : TaskBase
    {
        public int[,] Map { get; set; }
        public Day9(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(9, Demo);

            Map = BuildMap();
            Helpers.Print2DArray(Map);
        }

        private int[,] BuildMap()
        {
            int[,] map = new int[InputAsString.Count, InputAsString[0].Length];

            for (int i = 0; i < InputAsString.Count; i++)
            {
                var line = InputAsString[i].ToCharArray();
                for (int a = 0; a < line.Length; a++)
                {
                    map[i, a] = line[a] - '0';
                }
            }
            return map;
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            int lowPositionSum = 0;
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int a = 0; a < Map.GetLength(1); a++)
                {
                    int middle = getNumberAtPos(i, a);
                    //int topLeft = getNumberAtPos(i - 1, a - 1);
                    int top = getNumberAtPos(i - 1, a);
                    //int topRight = getNumberAtPos(i - 1, a + 1);
                    int left = getNumberAtPos(i, a - 1);
                    int right = getNumberAtPos(i, a + 1);
                    //int bottomLeft = getNumberAtPos(i + 1, a - 1);
                    int bottom = getNumberAtPos(i + 1, a);
                    //int bottomRight = getNumberAtPos(i + 1, a + 1);

                    if (middle < left && middle < top && middle < right && middle < bottom)
                        lowPositionSum += middle + 1;
                }
            }

            Log.Information("Part 1 answer: {0}", lowPositionSum);
            var expectedResult = Demo ? 15 : 462;
            Assert(lowPositionSum, expectedResult);
        }

        private int getNumberAtPos(int row, int col)
        {
            if (row < 0 || row >= Map.GetLength(0) || col < 0 || col >= Map.GetLength(1))
                return 9; //no number exist there, just return 9 because its the biggest

            return Map[row, col];
        }

        private new void GetResultPart2()
        {
            var answer = 0;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1134 : 0;
            Assert(answer, expectedResult);
        }
    }
}
