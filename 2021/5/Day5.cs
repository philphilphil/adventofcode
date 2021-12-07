using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day5 : TaskBase
    {


        public int[,] Map { get; set; }
        public Day5(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(5, Demo);

            Map = new int[999, 999];

            if (demo)
                Map = new int[10, 10];

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

                MarkHydroOnMap(x1, y1, x2, y2);
            }

            if (Demo)
                Helpers.Print2DArray(Map);
        }

        private void MarkHydroOnMap(int x1, int y1, int x2, int y2)
        {
            while (true)
            {
                Map[x1, y1]++;

                if (x1 == x2 && y1 == y2)
                    break;

                if (x1 < x2)
                {
                    x1++;
                }
                else if (x1 > x2)
                {
                    x1--;
                }
                if (y1 < y2)
                {
                    y1++;
                }
                else if (y1 > y2)
                {
                    y1--;
                }
            }
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            // does not work anymore because now also vertial lines are considered which creates more points
            // var answer = Map.Cast<int>().Where(x => x > 1).Count();
            // Console.WriteLine("Part 1 answer: {0}", answer);
            // Assert(answer, 6007);
        }

        private new void GetResultPart2()
        {
            var answer = Map.Cast<int>().Where(x => x > 1).Count();
            Console.WriteLine("Part 2 answer: {0}", answer);
            Assert(answer, 19349);
        }
    }
}