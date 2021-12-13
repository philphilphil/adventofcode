using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day12 : TaskBase
    {
        List<PathWay> Paths = new List<PathWay>();

        Dictionary<string, string[]> Connections = new Dictionary<string, string[]>();

        public Day12(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(12, Demo);
            BuildAllConnections();
        }

        private void BuildAllConnections()
        {

            List<string[]> cons = new List<string[]>();
            foreach (string input in InputAsString)
            {
                var caves = input.Split("-");
                var caveA = caves[0];
                var caveB = caves[1];

                cons.Add(new[] { caveA, caveB });
                cons.Add(new[] { caveB, caveA });

            }

            // grouped by "from":
            // return (
            //     from p in connections
            //     group p by p.From into g
            //     select g
            // ).ToDictionary(g => g.Key, g => g.Select(connnection => connnection.To).ToArray());
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {

            var startPaths = Paths.Where(x => x.CaveA == "start" || x.CaveB == "start").ToList();

            List<PathWay> nextPaths = new List<PathWay>();
            foreach (PathWay start in startPaths)
            {
                if (start.CaveA == "start")
                {
                    nextPaths.AddRange(GetNextPath(start.CaveA, start.CaveB));
                }
                else
                {
                    nextPaths.AddRange(GetNextPath(start.CaveB, start.CaveA));
                }
            }

            var answer = 0;
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }

        private List<PathWay> GetNextPath(string from, string to)
        {
            var p = Paths.Where(x => (x.CaveA == to && !x.CaveAIsSmallAndAlreadyVisited) || (x.CaveB == to && !x.CaveBIsSmallAndAlreadyVisited)).ToList();

            return p.Where(x => x.CaveA != from && x.CaveB != to).ToList();
        }

        private new void GetResultPart2()
        {
            var answer = 0;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }
    }

    class PathWay
    {
        public string CaveA { get; set; }
        public string CaveB { get; set; }

        public bool CaveAIsSmallAndAlreadyVisited
        {
            get { return CaveA.All(x => char.IsLower(x)) && this.CaveAVisited; }
        }

        public bool CaveBIsSmallAndAlreadyVisited
        {
            get { return CaveB.All(x => char.IsLower(x)) && this.CaveBVisited; }
        }

        public bool CaveAVisited { get; set; }

        public bool CaveBVisited { get; set; }

        public PathWay(string input)
        {
            var caves = input.Split('-');
            CaveA = caves[0];
            CaveB = caves[1];
        }
    }
}
