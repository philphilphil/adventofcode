using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day5 : TaskBase
    {

        public List<HydrothermalVent> HydroVentPoints = new List<HydrothermalVent>();

        public int[,] Map = new int[999, 999];

        public Day5()
        {
            base.ReadInput(5);
            BuildMap();
        }

        private void BuildMap()
        {
            foreach (var item in InputAsString)
            {

                var pair = item.Split(" -> ");
                var from = pair[0].Split(',');
                var to = pair[1].Split(',');

                int x1 = int.Parse(from[0]);
                int y1 = int.Parse(from[1]);
                int x2 = int.Parse(to[0]);
                int y2 = int.Parse(to[1]);

                if (x1 == x2)
                {
                    var range = GetHydroVentRange(y1, y2);
                    MarkHydroOnMap(true, x1, range);

                }
                else if (y1 == y2)
                {
                    var range = GetHydroVentRange(x1, x2);

                    MarkHydroOnMap(false, y1, range);
                }
                else
                {
                    //For now only lines where either x1 = x2 or y1 = y2.
                    continue;
                }
                // var hydro = new HydrothermalVent { X1 = int.Parse(from[0]), Y1 = int.Parse(from[1]), X2 = int.Parse(to[0]), Y2 = int.Parse(to[1]) };
                // HydroVentPoints.Add(hydro);
            }

            Helpers.Print2DArray(Map);
        }

        private void MarkHydroOnMap(bool vertical, int pos, List<int> range)
        {
            foreach (var point in range)
            {
                if (vertical)
                {
                    Map[point, pos]++;
                }
                else
                {
                    Map[pos, point]++;
                }
            }
        }

        private List<int> GetHydroVentRange(int a, int b)
        {
            List<int> range = new List<int>();
            int from = Math.Min(a, b);
            int to = Math.Max(a, b);

            for (; from <= to; from++)
            {
                range.Add(from);
            }
            return range;
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            var answer = Map.Cast<int>().Where( x => x > 1).Count();
            Console.WriteLine("Part 1 answer: {0}", "answer");
            Assert(answer, 1);
        }

        private new void GetResultPart2()
        {
            // var answer = "answer";
            // Console.WriteLine("Part 2 answer: {0}", "answer");
            // Assert(answer, "4138664");
        }
    }

    class HydrothermalVent
    {
        public int? X1 { get; set; }
        public int? Y1 { get; set; }
        public int? X2 { get; set; }
        public int? Y2 { get; set; }

        // List<HydrothermalVent> AllCoords { get; set; } = new List<HydrothermalVent>();
    }
}
