using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Day4 : TaskBase
    {

        private List<int> DrawnNumbers { get; set; }
        private List<BingoGame> BingoGames { get; set; }

        public Day4()
        {
            base.ReadInput(4);

            string drawnNumbers = "15,62,2,39,49,25,65,28,84,59,75,24,20,76,60,55,17,7,93,69,32,23,44,81,8,67,41,56,43,89,95,97,61,77,64,37,29,10,79,26,51,48,5,86,71,58,78,90,57,82,45,70,11,14,13,50,68,94,99,22,47,12,1,74,18,46,4,6,88,54,83,96,63,66,35,27,36,72,42,98,0,52,40,91,33,21,34,85,3,38,31,92,9,87,19,73,30,16,53,80";
            //string drawnNumbers = "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1";
            DrawnNumbers = drawnNumbers.Split(',').Select(x => int.Parse(x)).ToList();

            BingoGames = LoadGames();
        }

        private List<BingoGame> LoadGames()
        {
            List<BingoGame> games = new List<BingoGame>();

            BingoGame bg = new BingoGame();
            foreach (string input in InputAsString)
            {
                if (String.IsNullOrEmpty(input))
                {
                    games.Add(bg);
                    bg = new BingoGame();
                    continue;
                }
                var splits = input.Split(' ').ToList();
                splits = splits.Where(x => !String.IsNullOrEmpty(x)).ToList(); //clean spaces
                bg.Numbers.AddRange(splits.Select(x => int.Parse(x)));

            }

            return games;
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            int answer = 0;
            foreach (int drawnNumber in DrawnNumbers)
            {
                MarkNumberAndCeckForWinners(drawnNumber);

                var wonGame = BingoGames.Where(x => x.Won).FirstOrDefault();
                if (wonGame != null)
                {
                    answer = drawnNumber * GetSumOfUnmarkedNumbers(wonGame);
                    break;
                }
            }

            Console.WriteLine("Part 1 answer: {0}", answer);
            Assert(answer, 35711);
        }

        private int GetSumOfUnmarkedNumbers(BingoGame winner)
        {
            int sum = 0;

            for (int i = 0; i < winner.FoundNumbers.Length; i++)
            {
                if (!winner.FoundNumbers[i])
                    sum += winner.Numbers[i];
            }

            return sum;
        }

        private void MarkNumberAndCeckForWinners(int drawnNumber)
        {
            var games = BingoGames.Where(x => !x.Won);

            foreach (BingoGame bg in games)
            {
                int numberIndex = bg.Numbers.IndexOf(drawnNumber);
                if (numberIndex >= 0)
                {
                    bg.FoundNumbers[numberIndex] = true;
                }

                if (CheckForWin(bg))
                {
                    bg.Place = BingoGames.Where(x => x.Won).Count() + 1;
                    bg.Won = true;
                }
            }
        }

        private bool CheckForWin(BingoGame bg)
        {
            int horAdjust = 0;
            for (int i = 0; i < 5; i++)
            {
                // rows
                if (bg.FoundNumbers[i + horAdjust] && bg.FoundNumbers[i + 1 + horAdjust] && bg.FoundNumbers[i + 2 + horAdjust] && bg.FoundNumbers[i + 3 + horAdjust] && bg.FoundNumbers[i + 4 + horAdjust])
                    return true;
                horAdjust += 4;

                // columns
                if (bg.FoundNumbers[i] && bg.FoundNumbers[i + 5] && bg.FoundNumbers[i + 10] && bg.FoundNumbers[i + 15] && bg.FoundNumbers[i + 20])
                    return true;
            }

            return false;
        }

        private new void GetResultPart2()
        {
            int lastWinnerNumber = 0;
            int answer = 0;
            foreach (int drawnNumber in DrawnNumbers)
            {
                MarkNumberAndCeckForWinners(drawnNumber);

                if (BingoGames.Where(x => !x.Won).Count() == 0)
                {
                    lastWinnerNumber = drawnNumber;
                    break;
                }
            }

            BingoGame lastWinnerGame = BingoGames.OrderByDescending(x => x.Place).FirstOrDefault();
            answer = lastWinnerNumber * GetSumOfUnmarkedNumbers(lastWinnerGame);


            Console.WriteLine("Part 2 answer: {0}", answer);
            Assert(answer, 5586);
        }
    }

    class BingoGame
    {
        public List<int> Numbers { get; set; } = new List<int>();

        //public int FoundNumbers { get; set; } try with bb later

        public BitArray FoundNumbers { get; set; } = new BitArray(25);

        public bool Won { get; set; } = false;

        public int Place { get; set; } = 0;

    }
}
