﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day3 : TaskBase
    {
        public Day3()
        {
            base.ReadInput(3);
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        public new void GetResultPart1()
        {
            //101001011000
            string gammaRateBinary = "";
            string epsilonRateBinary = "";
            for (int i = 0; i < InputAsString[0].Length; i++)
            {
                int amountZero = 0, amountOne = 0;
                foreach (string reading in InputAsString)
                {
                    string c = reading.Substring(i, 1);

                    if (c == "0")
                    {
                        amountZero++;
                    }
                    else
                    {
                        amountOne++;
                    }
                }

                if (amountOne > amountZero)
                {
                    gammaRateBinary += "1";
                    epsilonRateBinary += "0";
                }
                else
                {
                    gammaRateBinary += "0";
                    epsilonRateBinary += "1";
                }
            }
            Console.WriteLine("Part 1:");
            Console.WriteLine("Gamma Binary: " + gammaRateBinary);
            Console.WriteLine("Epsilon Binary: " + epsilonRateBinary);
            var gammaRate = Convert.ToInt32(gammaRateBinary, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

            Console.WriteLine(String.Format("Answer: {0} x {1} = {2}", gammaRate, epsilonRate, gammaRate * epsilonRate));
            Console.WriteLine();
        }

        public new void GetResultPart2()
        {
            List<string> InputAsStringCopy = InputAsString;

            //find oxygen rating
            for (int i = 0; i < InputAsString[0].Length; i++)
            {
                InputAsString = CleanListAtPos(InputAsString, i, true);

                if (InputAsString.Count == 1)
                    break;
            }

            //find co2 scrubber rating
            for (int i = 0; i < InputAsStringCopy[0].Length; i++)
            {
                InputAsStringCopy = CleanListAtPos(InputAsStringCopy, i, false);

                if (InputAsStringCopy.Count == 1)
                    break;
            }

            string oxygenRatingBinary = InputAsString[0];
            string co2ScrubberBinary = InputAsStringCopy[0];

            Console.WriteLine("Part 2:");
            Console.WriteLine("oxygen generator rating: " + oxygenRatingBinary);
            Console.WriteLine("co2 scrubber rating: " + co2ScrubberBinary);
            var oxygenRating = Convert.ToInt32(oxygenRatingBinary, 2);
            var co2Scrubber = Convert.ToInt32(co2ScrubberBinary, 2);

            Console.WriteLine(String.Format("Answer: {0} x {1} = {2}", oxygenRating, co2Scrubber, oxygenRating * co2Scrubber));
        }

        private List<string> CleanListAtPos(List<string> list, int i, bool oxygen)
        {
            var cleanedList = new List<string>();
            int amountZero = 0, amountOne = 0;
            foreach (string reading in list)
            {
                string c = reading.Substring(i, 1);

                if (c == "0")
                {
                    amountZero++;
                }
                else
                {
                    amountOne++;
                }
            }


            if (oxygen)
            {
                if (amountOne >= amountZero)
                {
                    cleanedList = list.Where(x => x.Substring(i, 1) == "1").ToList();
                }
                else
                {
                    cleanedList = list.Where(x => x.Substring(i, 1) == "0").ToList();
                }
            }
            else
            {

                if (amountZero <= amountOne)
                {
                    cleanedList = list.Where(x => x.Substring(i, 1) == "0").ToList();
                }
                else
                {
                    cleanedList = list.Where(x => x.Substring(i, 1) == "1").ToList();
                }
            }


            return cleanedList;
        }

        private void RemoveFromInput(int pos, string bitToRemove)
        {
            foreach (String item in InputAsString)
            {
                if (item.Substring(pos, 1) == bitToRemove)
                    InputAsString.Remove(item);
            }
        }
    }
}
