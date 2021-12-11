using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day8 : TaskBase
    {
        List<List<string>> UniqueSignalPatterns = new List<List<string>>();
        List<List<string>> OutputValues = new List<List<string>>();

        public Day8(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(8, Demo);

            BuildData();
        }

        private void BuildData()
        {
            foreach (var item in InputAsString)
            {
                var referenceData = item.Split("|")[0].Split(" ").ToList();
                var outputString = item.Split("|")[1].Split(" ").ToList();
                outputString.Remove("");
                referenceData.Remove("");
                UniqueSignalPatterns.Add(referenceData);
                OutputValues.Add(outputString);
            }
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            int foundCharacters = 0;
            foreach (var item in OutputValues)
            {
                foundCharacters += item.Where(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7).Count();
            }

            Log.Information("Part 1 answer: {0}", foundCharacters);
            var expectedResult = Demo ? 26 : 261;
            Assert(foundCharacters, expectedResult);
        }

        private new void GetResultPart2()
        {

            List<int> Outputnumbers = new List<int>();
            for (int i = 0; i < OutputValues.Count(); i++)
            {
                var outputValues = OutputValues[i];
                var referenceData = UniqueSignalPatterns[i];

                var one = referenceData.Where(x => x.Length == 2).First();
                var four = referenceData.Where(x => x.Length == 4).First();
                var seven = referenceData.Where(x => x.Length == 3).First();
                var eight = referenceData.Where(x => x.Length == 7).First();

                var sixLengthSegments = referenceData.Where(x => x.Length == 6);
                var nine = sixLengthSegments.Where(x => x.Intersect(four).Count() == four.Length).First();
                var zero = sixLengthSegments.Where(x => x != nine && x.Intersect(one).Count() == one.Length).First();
                var six = sixLengthSegments.Where(x => x != nine && x != zero).First();

                var fiveLengthSegments = referenceData.Where(x => x.Length == 5);
                var three = fiveLengthSegments.Where(x => x.Intersect(seven).Count() == seven.Length).First();
                var five = fiveLengthSegments.Where(x => x != three && x.Intersect(six).Count() == six.Length - 1).First();
                var two = fiveLengthSegments.Where(x => x != five && x != three).First();

                Dictionary<string, string> mapping = new Dictionary<string, string>();
                mapping.Add(zero, "0");
                mapping.Add(one, "1");
                mapping.Add(two, "2");
                mapping.Add(three, "3");
                mapping.Add(four, "4");
                mapping.Add(five, "5");
                mapping.Add(six, "6");
                mapping.Add(seven, "7");
                mapping.Add(eight, "8");
                mapping.Add(nine, "9");

                var outputNumber = "";
                foreach (var item in outputValues)
                {
                    var a = mapping.Where(x => item.Length == x.Key.Length && item.Except(x.Key).Count() == 0).FirstOrDefault();
                    outputNumber += a.Value;
                }

                Outputnumbers.Add(int.Parse(outputNumber));

            }

            Log.Information("Part 2 answer: {0}", Outputnumbers.Sum());
            var expectedResult = Demo ? 61229 : 987553;
            Assert(Outputnumbers.Sum(), expectedResult);
        }
    }
}
