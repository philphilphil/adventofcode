using System;
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

            Console.WriteLine("Gamma Binary: " + gammaRateBinary);
            Console.WriteLine("Epsilon Binary: " + epsilonRateBinary);
            var gammaRate = Convert.ToInt32(gammaRateBinary, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

            Console.WriteLine(String.Format("Answer: {0} x {1} = {2}", gammaRate, epsilonRate, gammaRate * epsilonRate));
        }

        public new void GetResultPart2()
        {

        }
    }
}
