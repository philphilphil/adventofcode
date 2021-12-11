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
        bool[,] AlreadyFlashedThisStep { get; set; }

        public Day11(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(11, Demo);
            Map = Helpers.Build2DArray(InputAsString);
            AlreadyFlashedThisStep = new bool[Map.GetLength(0), Map.GetLength(1)];
            //Helpers.Print2DArray(Map);
        }

        internal void GetResults()
        {
            GetResultPart1();
            //GetResultPart2();
        }

        private void AddOneEnergyToAllOctopuses()
        {
            for (int y = 0; y < Map.GetLength(0); y++)
                for (int x = 0; x < Map.GetLength(1); x++)
                    Map[y, x]++;
        }

        private Tuple<int, int>? GetOctopusWithFullEnergy()
        {
            for (int y = 0; y < Map.GetLength(0); y++)
                for (int x = 0; x < Map.GetLength(1); x++)
                    if (Map[y, x] > 9)
                        return new Tuple<int, int>(y, x);

            return null;
        }

        private void AddOneEnergyToAllAdjecentOctopuses(int y, int x)
        {
            if (GetNumberAtPos(y - 1, x - 1) != -1 && !AlreadyFlashedThisStep[y - 1, x - 1])
                Map[y - 1, x - 1]++;

            if (GetNumberAtPos(y - 1, x) != -1 && !AlreadyFlashedThisStep[y - 1, x])
                Map[y - 1, x]++;

            if (GetNumberAtPos(y - 1, x + 1) != -1 && !AlreadyFlashedThisStep[y - 1, x + 1])
                Map[y - 1, x + 1]++;

            if (GetNumberAtPos(y, x - 1) != -1 && !AlreadyFlashedThisStep[y, x - 1])
                Map[y, x - 1]++;

            if (GetNumberAtPos(y, x + 1) != -1 && !AlreadyFlashedThisStep[y, x + 1])
                Map[y, x + 1]++;

            if (GetNumberAtPos(y + 1, x - 1) != -1 && !AlreadyFlashedThisStep[y + 1, x - 1])
                Map[y + 1, x - 1]++;

            if (GetNumberAtPos(y + 1, x) != -1 && !AlreadyFlashedThisStep[y + 1, x])
                Map[y + 1, x]++;

            if (GetNumberAtPos(y + 1, x + 1) != -1 && !AlreadyFlashedThisStep[y + 1, x + 1])
                Map[y + 1, x + 1]++;
        }

        private new void GetResultPart1()
        {
            int flashCount = 0;
            for (int i = 1; i <= 100; i++)
            {
                AlreadyFlashedThisStep = new bool[Map.GetLength(0), Map.GetLength(1)];
                AddOneEnergyToAllOctopuses();

                while (true)
                {
                    Tuple<int, int>? fullEnergyOctopus = GetOctopusWithFullEnergy();

                    if (fullEnergyOctopus == null)
                        break;

                    flashCount += FlashOctopus(fullEnergyOctopus.Item1, fullEnergyOctopus.Item2, 0);
                }

                //Console.WriteLine("After step " + i);
                //Helpers.Print2DArray(Map);
            }

            Log.Information("Part 1 answer: {0}", flashCount);
            var expectedResult = Demo ? 1656 : 1755;
            Assert(flashCount, expectedResult);
        }

        private int GetNumberAtPos(int row, int col)
        {
            if (row < 0 || row >= Map.GetLength(0) || col < 0 || col >= Map.GetLength(1))
                return -1;

            return Map[row, col];
        }

        private int FlashOctopus(int y, int x, int flashes)
        {
            if (AlreadyFlashedThisStep[y, x]) return 0;

            int middle = GetNumberAtPos(y, x);
            int topLeft = GetNumberAtPos(y - 1, x - 1);
            int top = GetNumberAtPos(y - 1, x);
            int topRight = GetNumberAtPos(y - 1, x + 1);
            int left = GetNumberAtPos(y, x - 1);
            int right = GetNumberAtPos(y, x + 1);
            int bottomLeft = GetNumberAtPos(y + 1, x - 1);
            int bottom = GetNumberAtPos(y + 1, x);
            int bottomRight = GetNumberAtPos(y + 1, x + 1);

            if (middle > 9)
            {
                flashes++;
                Map[y, x] = 0;
                AddOneEnergyToAllAdjecentOctopuses(y, x);
                AlreadyFlashedThisStep[y, x] = true;
            }

            flashes += top > 9 ? FlashOctopus(y - 1, x, 0) : 0;
            flashes += left > 9 ? FlashOctopus(y, x - 1, 0) : 0;
            flashes += right > 9 ? FlashOctopus(y, x + 1, 0) : 0;
            flashes += bottom > 9 ? FlashOctopus(y + 1, x, 0) : 0;
            flashes += topLeft > 9 ? FlashOctopus(y - 1, x - 1, 0) : 0;
            flashes += topRight > 9 ? FlashOctopus(y - 1, x + 1, 0) : 0;
            flashes += bottomLeft > 9 ? FlashOctopus(y + 1, x - 1, 0) : 0;
            flashes += bottomRight > 9 ? FlashOctopus(y + 1, x + 1, 0) : 0;

            return flashes;
        }

        private new void GetResultPart2()
        {
           //p2 was looked up from console output after increasnig steps
        }
    }
}
