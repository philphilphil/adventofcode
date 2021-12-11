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

        bool[,] AlreadyVisited { get; set; }

        public Day9(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(9, Demo);

            Map = Helpers.Build2DArray(InputAsString);
            AlreadyVisited = new bool[Map.GetLength(0), Map.GetLength(1)];

            //Helpers.Print2DArray(Map);
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
                    int top = getNumberAtPos(i - 1, a);
                    int left = getNumberAtPos(i, a - 1);
                    int right = getNumberAtPos(i, a + 1);
                    int bottom = getNumberAtPos(i + 1, a);

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

        public int RecrusiveCave(int y, int x, int currSize)
        {
            if (AlreadyVisited[y, x]) return 0;

            int current = getNumberAtPos(y, x);
            int top = getNumberAtPos(y - 1, x);
            int left = getNumberAtPos(y, x - 1);
            int right = getNumberAtPos(y, x + 1);
            int bottom = getNumberAtPos(y + 1, x);

            if (current != 9) currSize++;
            AlreadyVisited[y, x] = true;

            currSize += top != 9 ? RecrusiveCave(y - 1, x, 0) : 0;
            currSize += left != 9 ? RecrusiveCave(y, x - 1, 0) : 0;
            currSize += right != 9 ? RecrusiveCave(y, x + 1, 0) : 0;
            currSize += bottom != 9 ? RecrusiveCave(y + 1, x, 0) : 0;

            return currSize;
        }

        private new void GetResultPart2()
        {
            List<int> Caves = new List<int>();

            for (int y = 0; y < Map.GetLength(0); y++)
            {
                for (int x = 0; x < Map.GetLength(1); x++)
                {
                    if (AlreadyVisited[y, x]) continue;

                    var result = RecrusiveCave(y, x, 0);
                    if (result != 0)
                        Caves.Add(result);
                }
            }

            var biggestCaves = Caves.OrderByDescending(x => x).ToList();
            var answer = biggestCaves[0] * biggestCaves[1] * biggestCaves[2];
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1134 : 1397760;
            Assert(answer, expectedResult);
        }
    }
}
