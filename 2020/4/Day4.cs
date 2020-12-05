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
            GetResultPart2();
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
            int validPassports = 0;
            var passports = GetPassportsFromInput();
            var ppValidationFields = GetPassportValidationFields();

            foreach (var pp in passports)
            {

                if (ppValidationFields.All(pp.Contains) && ValidatePassportFields(pp))
                    validPassports++;
            }

            Console.Write(String.Format("Part 2 valid passports: {0}", validPassports.ToString()));
        }

        private bool ValidatePassportFields(string pp)
        {
            var fields = pp.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fields)
            {
                var keyValue = field.Split(':');
                var valid = true;

                switch (keyValue[0])
                {
                    case "byr":
                        valid = CheckNumberRange(keyValue[1], 1920, 2002);
                        break;
                    case "iyr":
                        valid = CheckNumberRange(keyValue[1], 2010, 2020);
                        break;
                    case "eyr":
                        valid = CheckNumberRange(keyValue[1], 2020, 2030);
                        break;
                    case "hgt":
                        valid = CheckSpecial_hgt(keyValue[1]);
                        break;
                    case "hcl":
                        valid = CheckWithRegex(keyValue[1], @"^#[0-9a-f]{6}$");
                        break;
                    case "ecl":
                        valid = CheckWithRegex(keyValue[1], @"^(amb|blu|brn|gry|grn|hzl|oth)$");
                        break;
                    case "pid":
                        valid = CheckWithRegex(keyValue[1], @"^\d{9}$");
                        break;
                    default:
                        break;
                }
                if (valid)
                    Console.WriteLine(String.Format("Checking: {0} key: {1} value {2}", valid.ToString(), keyValue[0], keyValue[1]));

                if (!valid)
                    return false;
            }

            return true;
        }

        private bool CheckSpecial_hgt(string value)
        {
            var regexResult = Regex.Match(value, @"^(\d+)(cm|in)$");

            if (!regexResult.Success)
                return false;

            var number = int.Parse(regexResult.Groups[1].Value);
            var unit = regexResult.Groups[2].Value;

            return (unit == "cm" && number >= 150 && number <= 193) || (unit == "in" && number >= 59 && number <= 76);
        }

        private bool CheckWithRegex(string value, string regex)
        {
            return Regex.Match(value, regex, RegexOptions.IgnoreCase).Success;
        }

        private bool CheckNumberRange(string value, int min, int max)
        {
            int valueAsInt;
            if (!int.TryParse(value, out valueAsInt))
                return false;

            return valueAsInt >= min && valueAsInt <= max;
        }
    }
}
