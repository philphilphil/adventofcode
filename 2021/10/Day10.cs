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

            // (char, char) bracket = ('(', ')');
            // (char, char) sBracket = ('[', ']');
            // (char, char) braces = ('{', '}');
            // (char, char) relOp = ('<', '>');
            List<char> falseClose = new List<char>();

            foreach (string input in InputAsString)
            {
                List<char> operators = input.ToCharArray().ToList();

                for (int i = 0; i < operators.Count(); i++)
                {
                    if (i == operators.Count - 1)
                    {
                        break;
                    }

                    var closeChar = closeCharacters.IndexOf(operators[i]);
                    if (closeChar >= 0)
                    {
                        if (operators[i - 1] != openCharacters[closeChar])
                        {
                            falseClose.Add(operators[i]);
                            break;
                        }
                        else
                        {
                            operators.RemoveAt(i - 1);
                            operators.RemoveAt(i - 1);
                            i = 0;
                        }
                    }

                }
            }

            int bracketPoints = falseClose.Where(x => x == ')').Count() * 3;
            int sBracketPoints = falseClose.Where(x => x == ']').Count() * 57;
            int bracePoints = falseClose.Where(x => x == '}').Count() * 1197;
            int relOp = falseClose.Where(x => x == '>').Count() * 25137;

            var answer = bracketPoints + sBracketPoints + bracePoints + relOp;
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 26397 : 243939;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {
            var answer = 0;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }
    }
}
