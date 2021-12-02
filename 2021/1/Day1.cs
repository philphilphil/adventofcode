using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day1 : TaskBase
    {
        public Day1()
        {
            base.ReadInput(1);
            base.InputToInt();
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        public new void GetResultPart1()
        {
            int prevMeasurement = InputAsInt[0];
            int increasedCounter = 0;

            foreach (var measurement in InputAsInt)
            {
                if (measurement > prevMeasurement)
                    increasedCounter++;

                prevMeasurement = measurement;
            }

            Console.WriteLine(String.Format("It increased {0} times", increasedCounter));
        }

        public new void GetResultPart2()
        {
            List<int> slideWindowSums = new List<int>();

            for (int i = 0; i < InputAsInt.Count; i++)
            {
                if (InputAsInt.Count < i + 3)
                    continue;

                var slideWindowSum = InputAsInt[i] + InputAsInt[i + 1] + InputAsInt[i + 2];
                slideWindowSums.Add(slideWindowSum);
            }

            InputAsInt = slideWindowSums;
            GetResultPart1();
        }
    }
}
