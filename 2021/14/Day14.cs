using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day14 : TaskBase
    {

        Dictionary<string, char> PairInsertionRules { get; set; } = new Dictionary<string, char>();
        List<char> PolymerTemplate { get; set; }
        string PolymerTemplateStr { get; set; }


        public Day14(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(14, Demo);
            PolymerTemplate = InputAsString[0].ToCharArray().ToList();
            PolymerTemplateStr = InputAsString[0];
            FillPairInsertInRules();
        }

        private void FillPairInsertInRules()
        {
            for (int i = 2; i < InputAsString.Count(); i++)
            {
                var ins = InputAsString[i].Split("->");
                PairInsertionRules.Add(ins[0].Trim(), char.Parse(ins[1].Trim()));
            }
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            var stepps = 10;

            for (int i = 0; i < stepps; i++)
            {
                List<char> polymer = new List<char>();
                for (int a = 0; a < PolymerTemplate.Count; a++)
                {

                    if (a + 1 == PolymerTemplate.Count)
                    {
                        polymer.Add(PolymerTemplate[a]);
                        PolymerTemplate = polymer;
                        break;
                    }

                    string pair = PolymerTemplate[a].ToString() + PolymerTemplate[a + 1].ToString();
                    polymer.Add(PolymerTemplate[a]);
                    polymer.Add(PairInsertionRules[pair]);
                }
            }

            var most = PolymerTemplate.GroupBy(x => x).OrderByDescending(x => x.Count()).First();
            var last = PolymerTemplate.GroupBy(x => x).OrderByDescending(x => x.Count()).Last();

            var answer = most.Count() - last.Count();
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 1588 : 2947;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {
            var stepps = 10;

            for (int i = 0; i < stepps; i++)
            {
                string test = "";

                foreach (var item in PairInsertionRules)
                {
                    var replaceValue = item.Key.ToCharArray();
                    var replaceString = replaceValue[0].ToString() + item.Value.ToString() + replaceValue[1].ToString();

                    if (PolymerTemplateStr.Contains(item.Key))
                        test += replaceString;
                }

                PolymerTemplateStr = test;
            }

            // for (int i = 0; i < stepps; i++)
            // {
            //     string polymer = "";
            //     for (int a = 0; a < PolymerTemplate.Length; a++)
            //     {
            //         var chars = PolymerTemplate.ToCharArray();

            //         if (a + 1 == PolymerTemplate.Length)
            //         {
            //             polymer += chars[a];
            //             PolymerTemplate = polymer;
            //             break;
            //         }

            //         string pair = chars[a].ToString() + chars[a + 1].ToString();
            //         string element = PairInsertionRules[pair];

            //         polymer += chars[a].ToString() + element;
            //     }
            // }

            var most = PolymerTemplate.GroupBy(x => x).OrderByDescending(x => x.Count()).First();
            var last = PolymerTemplate.GroupBy(x => x).OrderByDescending(x => x.Count()).Last();
            // var mostA = PolymerTemplate.Where(x => x == most).Count();

            var answer = most.Count() - last.Count();
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 2188189693529 : 2947;
            Assert(answer, expectedResult);
        }
    }
}
