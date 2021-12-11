using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day11 : TaskBase
    {
        public int[,] Map { get; set; }
        bool[,] AlreadyFlashed { get; set; }

        public Day11(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(11, Demo);
            Map = Helpers.Build2DArray(InputAsString);
            AlreadyFlashed = new bool[Map.GetLength(0), Map.GetLength(1)];
            Helpers.Print2DArray(Map);
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private void addOneToAll()
        {
            for (int y = 0; y < Map.GetLength(0); y++)
            {
                for (int x = 0; x < Map.GetLength(1); x++)
                {
                    Map[y, x]++;

                }
            }
        }

        private void addOneToAllAdjecent(int y, int x)
        {
            if (getNumberAtPos(y - 1, x - 1) != -1 && !AlreadyFlashed[y - 1, x - 1])
                Map[y - 1, x - 1]++;
            if (getNumberAtPos(y - 1, x) != -1 && !AlreadyFlashed[y - 1, x])
                Map[y - 1, x]++;
            if (getNumberAtPos(y - 1, x + 1) != -1 && !AlreadyFlashed[y - 1, x + 1])
                Map[y - 1, x + 1]++;
            if (getNumberAtPos(y, x - 1) != -1 && !AlreadyFlashed[y, x - 1])
                Map[y, x - 1]++;
            if (getNumberAtPos(y, x + 1) != -1 && !AlreadyFlashed[y, x + 1])
                Map[y, x + 1]++;
            if (getNumberAtPos(y + 1, x - 1) != -1 && !AlreadyFlashed[y + 1, x - 1])
                Map[y + 1, x - 1]++;
            if (getNumberAtPos(y + 1, x) != -1 && !AlreadyFlashed[y + 1, x])
                Map[y + 1, x]++;
            if (getNumberAtPos(y + 1, x + 1) != -1 && !AlreadyFlashed[y + 1, x + 1])
                Map[y + 1, x + 1]++;
        }

        private new void GetResultPart1()
        {
            int flashCount = 0;
            for (int i = 1; i <= 100; i++)
            {
                AlreadyFlashed = new bool[Map.GetLength(0), Map.GetLength(1)];
                addOneToAll();
                // Console.WriteLine("StartStep " + i);
                // Helpers.Print2DArray(Map);

                for (int y = 0; y < Map.GetLength(0); y++)
                {
                    for (int x = 0; x < Map.GetLength(1); x++)
                    {

                        int curr = Map[y, x];
                        //Helpers.Print2DArray(Map);

                        if (curr > 9)
                        {
                            flashCount += Flash(y, x, 0);
                            x = 0;
                            y = 0;
                        }

                        //Helpers.Print2DArray(Map);

                    }
                }

                  for (int y = 0; y < Map.GetLength(0); y++)
                {
                    for (int x = 0; x < Map.GetLength(1); x++)
                    {

                        int curr = Map[y, x];
                        //Helpers.Print2DArray(Map);

                        if (curr > 9)
                        {
                            flashCount += Flash(y, x, 0);
                            x = 0;
                            y = 0;
                        }

                        //Helpers.Print2DArray(Map);

                    }
                }

                Console.WriteLine("After step " + i);
                Helpers.Print2DArray(Map);

            }

            Log.Information("Part 1 answer: {0}", flashCount);
            var expectedResult = Demo ? 1656 : 0;
            Assert(flashCount, expectedResult);
        }
        private int? getNumberAtPos(int row, int col)
        {
            if (row < 0 || row >= Map.GetLength(0) || col < 0 || col >= Map.GetLength(1))
                return -1;

            return Map[row, col];
        }
        public int Flash(int y, int x, int flashes)
        {
            if (AlreadyFlashed[y, x]) return 0;

            int? middle = getNumberAtPos(y, x);
            int? topLeft = getNumberAtPos(y - 1, x - 1);
            int? top = getNumberAtPos(y - 1, x);
            int? topRight = getNumberAtPos(y - 1, x + 1);
            int? left = getNumberAtPos(y, x - 1);
            int? right = getNumberAtPos(y, x + 1);
            int? bottomLeft = getNumberAtPos(y + 1, x - 1);
            int? bottom = getNumberAtPos(y + 1, x);
            int? bottomRight = getNumberAtPos(y + 1, x + 1);

            if (middle > 9)
            {
                flashes++;
                Map[y, x] = 0;
                addOneToAllAdjecent(y, x);
                //Helpers.Print2DArray(Map);
                AlreadyFlashed[y, x] = true;
            }

            flashes += top > 9 ? Flash(y - 1, x, 0) : 0;
            flashes += left > 9 ? Flash(y, x - 1, 0) : 0;
            flashes += right > 9 ? Flash(y, x + 1, 0) : 0;
            flashes += bottom > 9 ? Flash(y + 1, x, 0) : 0;
            flashes += topLeft > 9 ? Flash(y - 1, x - 1, 0) : 0;
            flashes += topRight > 9 ? Flash(y - 1, x + 1, 0) : 0;
            flashes += bottomLeft > 9 ? Flash(y + 1, x - 1, 0) : 0;
            flashes += bottomRight > 9 ? Flash(y + 1, x + 1, 0) : 0;

            return flashes;
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
