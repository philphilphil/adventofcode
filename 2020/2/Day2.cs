using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day2 : TaskBase
    {
        public Day2()
        {
            base.ReadInput(2);
        }

        internal void GetResults()
        {
            GetResultPart1();
            Console.WriteLine();
            Console.WriteLine();
            GetResultPart2();
            Console.WriteLine();
        }

        public void GetResultPart1()
        {
            string regexPattern = @"([0-9]+)-([0-9]+) ([a-z]): ([a-zA-Z]+)";
            int validPasswords = 0;

            foreach (var checkLine in Input)
            {
                var regexResult = Regex.Match(checkLine, regexPattern);
                int minAmount = int.Parse(regexResult.Groups[1].Value);
                int maxAmount = int.Parse(regexResult.Groups[2].Value);
                char letter = char.Parse(regexResult.Groups[3].Value);
                var pw = regexResult.Groups[4].Value;

                int amount = GetPasswordLetterAmount(pw, letter);

                if (amount >= minAmount && amount <= maxAmount)
                {
                    validPasswords++;
                }

            }

            Console.Write(String.Format("Part 1 valid passwords: {0}", validPasswords.ToString()));
        }

        private int GetPasswordLetterAmount(string pw, char letter)
        {
            return pw.Count(x => x == letter);
        }

        public void GetResultPart2()
        {
            string regexPattern = @"([0-9]+)-([0-9]+) ([a-z]): ([a-zA-Z]+)";
            int validPasswords = 0;

            foreach (var checkLine in Input)
            {
                var regexResult = Regex.Match(checkLine, regexPattern);
                int pos1 = int.Parse(regexResult.Groups[1].Value);
                int pos2 = int.Parse(regexResult.Groups[2].Value);
                char letter = char.Parse(regexResult.Groups[3].Value);
                var pw = regexResult.Groups[4].Value;

                char charAtPosOne = pw.ToCharArray()[pos1 - 1];
                char charAtPosTwo = pw.ToCharArray()[pos2 - 1];

                if(charAtPosOne != letter && charAtPosTwo == letter)
                {
                    validPasswords++;
                }
                else if (charAtPosOne == letter && charAtPosTwo != letter)
                {
                    validPasswords++;
                }
            }

            Console.Write(String.Format("Part 2 valid passwords: {0}", validPasswords.ToString()));
        }
    }
}
