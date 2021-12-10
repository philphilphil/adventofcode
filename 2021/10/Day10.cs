using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class Day10 : TaskBase
    {

        List<string> IncompleteLines = new List<string>();
        public Day10(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(10, Demo);
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            List<char> openCharacters = new List<char> { '(', '[', '{', '<' };
            List<char> closeCharacters = new List<char> { ')', ']', '}', '>' };
            List<char> corruptedLineEnds = new List<char>();
            IncompleteLines.AddRange(InputAsString);

            foreach (string input in InputAsString)
            {
                List<char> operators = input.ToCharArray().ToList();

                for (int i = 0; i < operators.Count(); i++)
                {
                    var closeChar = closeCharacters.IndexOf(operators[i]);
                    if (closeChar != -1)
                    {
                        if (operators[i - 1] != openCharacters[closeChar])
                        {
                            corruptedLineEnds.Add(operators[i]);
                            IncompleteLines.Remove(input); //remove corrupted line for p2
                            break;
                        }
                        else
                        {
                            operators.RemoveAt(i - 1);
                            operators.RemoveAt(i - 1);
                            i = 0; //start from the beginning
                        }
                    }
                }
            }

            int bracketPoints = corruptedLineEnds.Where(x => x == ')').Count() * 3;
            int sBracketPoints = corruptedLineEnds.Where(x => x == ']').Count() * 57;
            int bracePoints = corruptedLineEnds.Where(x => x == '}').Count() * 1197;
            int relOpPoints = corruptedLineEnds.Where(x => x == '>').Count() * 25137;

            var answer = bracketPoints + sBracketPoints + bracePoints + relOpPoints;
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 26397 : 243939;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {
            List<char> openCharacters = new List<char> { '(', '[', '{', '<' };
            List<char> closeCharacters = new List<char> { ')', ']', '}', '>' };
            List<char[]> lineOpenings = new List<char[]>();

            foreach (string input in IncompleteLines)
            {
                List<char> operators = input.ToCharArray().ToList();

                for (int i = 0; i < operators.Count(); i++)
                {
                    var closeChar = closeCharacters.IndexOf(operators[i]);
                    if (closeChar != -1)
                    {
                        if (operators[i - 1] != openCharacters[closeChar])
                        {
                            break;
                        }
                        else
                        {
                            operators.RemoveAt(i - 1);
                            operators.RemoveAt(i - 1);
                            i = 0; //start from the beginning
                        }
                    }
                }

                lineOpenings.Add(operators.ToArray());
            }

            Dictionary<char, int> points = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
            List<long> answerValues = new List<long>();

            foreach (char[] line in lineOpenings)
            {
                long linePoints = 0;

                // reverse to get first close on first position (but with opposite char)
                Array.Reverse(line);

                for (int i = 0; i < line.Length; i++)
                {
                    linePoints *= 5;
                    linePoints += points[line[i]];
                }

                answerValues.Add(linePoints);
            }

            long answer = answerValues.OrderByDescending(x => x).ToList()[answerValues.Count / 2];

            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 288957 : 2421222841;
            Assert(answer, expectedResult);
        }
    }
}
