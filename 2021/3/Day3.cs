﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day3 : TaskBase
    {

        private int InputCharacterLenght { get; set; } = 12;

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
            string gammaRateBinary = "";
            string epsilonRateBinary = "";

            for (int i = 0; i < InputCharacterLenght; i++)
            {
                int amountZero = InputAsString.Where(x => x.Substring(i, 1) == "0").Count();
                int amountOne = InputAsString.Where(x => x.Substring(i, 1) == "1").Count();

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
            var answer = gammaRate * epsilonRate;
            Console.WriteLine(String.Format("Answer: {0} x {1} = {2}", gammaRate, epsilonRate, answer));
            Assert(answer, 4138664);
            Console.WriteLine();
        }

        public new void GetResultPart2()
        {
            List<string> InputAsStringCopy = InputAsString;

            //find oxygen rating
            for (int i = 0; i < InputCharacterLenght; i++)
            {
                if (InputAsString.Count != 1)
                    InputAsString = CleanListAtPos(InputAsString, i, true);

                if (InputAsStringCopy.Count != 1)
                    InputAsStringCopy = CleanListAtPos(InputAsStringCopy, i, false);
            }

            string oxygenRatingBinary = InputAsString[0];
            string co2ScrubberBinary = InputAsStringCopy[0];

            Console.WriteLine("Part 2:");
            Console.WriteLine("oxygen generator rating: " + oxygenRatingBinary);
            Console.WriteLine("co2 scrubber rating: " + co2ScrubberBinary);
            var oxygenRating = Convert.ToInt32(oxygenRatingBinary, 2);
            var co2Scrubber = Convert.ToInt32(co2ScrubberBinary, 2);
            var answer = oxygenRating * co2Scrubber;
            Console.WriteLine(String.Format("Answer: {0} x {1} = {2}", oxygenRating, co2Scrubber, answer));
            Assert(answer, 4273224);
        }

        private List<string> CleanListAtPos(List<string> list, int i, bool oxygen)
        {
            List<string> zeroes = list.Where(x => x.Substring(i, 1) == "0").ToList();
            List<string> ones = list.Where(x => x.Substring(i, 1) == "1").ToList();

            if (oxygen)
            {
                if (ones.Count() >= zeroes.Count())
                    return ones;
            }
            else
            {
                if (ones.Count() < zeroes.Count())
                    return ones;
            }

            return zeroes;
        }
    }
}
