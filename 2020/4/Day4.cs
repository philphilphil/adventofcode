using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day4 : TaskBase
    {
        public Day4()
        {
            base.ReadInput(4);
        }

        public void GetResults()
        {
            GetResultPart1();
            Console.WriteLine();
            // GetResultPart2();
            Console.WriteLine();
            Console.WriteLine();
        }

        private List<string> GetPassportsFromInput()
        {
            var passports = new List<string>();
            var passportString = "";

            foreach (var item in Input)
            {
                if (item == String.Empty)
                {
                    passports.Add(passportString);
                    passportString = "";
                }
                else
                {
                    passportString += " " + item;
                }
            }
            return passports;
        }

        private void GetResultPart1()
        {
            int validPassports = 0;
            var passports = GetPassportsFromInput();
            var ppFields = GetPassportValidationFields();

            foreach (var pp in passports)
            {
                if (ppFields.All(pp.Contains))
                    validPassports++;
            }
            
            Console.Write(String.Format("Part 1 valid passports: {0}", validPassports.ToString()));
        }

        private List<string> GetPassportValidationFields()
        {
            var ppFields = new List<string>();
            ppFields.Add("byr");
            ppFields.Add("iyr");
            ppFields.Add("eyr");
            ppFields.Add("hgt");
            ppFields.Add("hcl");
            ppFields.Add("ecl");
            ppFields.Add("pid");
            //  ppFields.Add("cid");
            return ppFields;
        }

        private void GetResultPart2()
        {
            //Right 1, down 1.
            //Right 3, down 1. (This is the slope you already checked.)           
            //Right 5, down 1.
            //Right 7, down 1.
            //Right 1, down 2.
            List<long> slopeResults = new List<long>();
            List<Tuple<int, int>> slopes = new List<Tuple<int, int>>();
            slopes.Add(new Tuple<int, int>(1, 1));
            slopes.Add(new Tuple<int, int>(3, 1));
            slopes.Add(new Tuple<int, int>(5, 1));
            slopes.Add(new Tuple<int, int>(7, 1));
            slopes.Add(new Tuple<int, int>(1, 2));

            foreach (var slope in slopes)
            {
                int pos = 0;
                int trees = 0;
                int currentLine = 0;

                foreach (string checkLine in Input)
                {

                    if (currentLine % slope.Item2 != 0)
                    {
                        currentLine++;
                        continue;
                    }

                    currentLine++;
                    List<char> chars = checkLine.ToCharArray().ToList();

                    int originalPos = pos;
                    if (chars.Count <= pos) //reset the pos, strings are the same
                    {
                        pos = pos % chars.Count;
                    }

                    if (chars[pos] == '#')
                        trees++;

                    pos = originalPos + slope.Item1;
                }

                slopeResults.Add((uint)trees);
            }
            long calc = slopeResults[0] * slopeResults[1] * slopeResults[2] * slopeResults[3] * slopeResults[4];
            Console.Write(String.Format("Part 2 result: {0}", calc.ToString()));
        }
    }
}
