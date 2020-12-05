using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day5 : TaskBase
    {
        public Day5()
        {
            base.ReadInput(5);
        }

        public void GetResults()
        {
            GetResultPart1();
            Console.WriteLine();
            GetResultPart2();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void GetResultPart1()
        {
            int highestSeatId = 0;
            foreach (string item in Input)
            {
                int seatRow = ParseBinarySpacePartitioning(item.Substring(0, 7), 127, 'F', 'B');
                int seatCol = ParseBinarySpacePartitioning(item.Substring(7, 3), 8, 'L', 'R');

                var seatId = seatRow * 8 + seatCol;

                if (seatId > highestSeatId)
                    highestSeatId = seatId;

            }

            Console.Write(String.Format("Part 1 highest seat id: {0}", highestSeatId.ToString()));
        }

        private void GetResultPart2()
        {
            List<int> allSeatIds = new List<int>();
            foreach (string item in Input)
            {
                int seatRow = ParseBinarySpacePartitioning(item.Substring(0, 7), 127, 'F', 'B');
                int seatCol = ParseBinarySpacePartitioning(item.Substring(7, 3), 8, 'L', 'R');

                var seatId = seatRow * 8 + seatCol;

                allSeatIds.Add(seatId);
            }

            allSeatIds = allSeatIds.OrderBy(x => x).ToList();
            int mySeatId = 0;

            for (int i = 0; i < allSeatIds.Count; i++)
            {

                if (allSeatIds[i] + 1 != allSeatIds[i + 1]) //check if next seat is missing
                {
                    //check if next of next is there
                    if (allSeatIds[i] + 2 != allSeatIds[i + 2])
                    {
                        mySeatId = allSeatIds[i] + 1;
                        break;
                    }
                }

            }

            Console.Write(String.Format("Part 2 my seat: {0}", mySeatId.ToString()));
        }

        private int ParseBinarySpacePartitioning(string bsp, int range, char lowerHalf, char upperHalf)
        {
            int[] remainingSeats = Enumerable.Range(0, range + 1).ToArray();
            foreach (char item in bsp)
            {
                if (item == lowerHalf)
                {
                    remainingSeats = remainingSeats.Take(remainingSeats.Length / 2).ToArray();
                }
                else if (item == upperHalf)
                {
                    remainingSeats = remainingSeats.Skip(remainingSeats.Length / 2).Take(remainingSeats.Length / 2).ToArray();
                }
                else
                {
                    throw new Exception("Error in binary space partitioning");
                }
            }

            if (remainingSeats.Length != 1)
                throw new Exception("Error in binary space partitioning");

            return remainingSeats[0];
        }
    }
}
