using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day13 : TaskBase
    {
        public bool[,] Map { get; set; }

        List<(int, int)> Coordinates { get; set; } = new List<(int, int)>();

        List<(char, int)> FoldInstructions { get; set; } = new List<(char, int)>();


        List<bool[,]> Folds { get; set; } = new List<bool[,]>();

        public Day13(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(13, Demo);

            FillInputCords();
            FillFoldInstructions();
            int biggestY = Coordinates.Select(x => x.Item1).Max() + 1;
            int biggestX = Coordinates.Select(x => x.Item2).Max() + 1;

            Map = new bool[biggestX, biggestY];
            MarkPointsOnMap();

            if (Demo) Helpers.Print2DArray(Map);
        }

        private void FillFoldInstructions()
        {
            foreach (var item in InputAsString)
            {
                if (!item.Contains("fold along "))
                    continue;

                var ins = item.Split('=');
                var axis = ins[0].Replace("fold along ", "");

                FoldInstructions.Add((char.Parse(axis), int.Parse(ins[1])));
            }
        }

        private void FillInputCords()
        {
            foreach (var item in InputAsString)
            {
                if (String.IsNullOrEmpty(item))
                    break; //reached end of cords

                var cords = item.Split(',');
                Coordinates.Add((int.Parse(cords[0]), int.Parse(cords[1])));
            }
        }

        private void MarkPointsOnMap()
        {
            foreach (var item in Coordinates)
                Map[item.Item2, item.Item1] = true;
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        void FoldMap(char axis, int foldAt)
        {
            if (axis == 'y')
            {
                for (int i = foldAt + 1; i < Map.GetLength(0); i++)
                    for (int x = 0; x < Map.GetLength(1); x++)
                        if (Map[i, x])
                        {
                            Map[foldAt - (i - foldAt), x] = true;
                            Map[i, x] = false;
                        }
            }
            else
            {
                for (int i = foldAt + 1; i < Map.GetLength(1); i++)
                    for (int x = 0; x < Map.GetLength(0); x++)
                        if (Map[x, i])
                        {
                            Map[x, foldAt - (i - foldAt)] = true;
                            Map[x, i] = false;
                        }
            }
        }

        void PrintLetters()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    var output = ".";
                    if (Map[i, j] == true)
                        output = "#";

                    Console.Write("{0}", output);
                }
                Console.WriteLine();
            }
        }

        private new void GetResultPart1()
        {

            var fold = FoldInstructions[0];
            FoldMap(fold.Item1, fold.Item2);

            if (Demo) Helpers.Print2DArray(Map);

            var answer = Map.Cast<bool>().Where(x => x).Count();
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 17 : 618;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {

            foreach (var fold in FoldInstructions)
                FoldMap(fold.Item1, fold.Item2);


            var answer = Map.Cast<bool>().Where(x => x).Count();
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 16 : 98;
            Assert(answer, expectedResult);

            if (Demo) Helpers.Print2DArray(Map);
            if (!Demo) PrintLetters();
        }
    }
}
