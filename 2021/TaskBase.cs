using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class TaskBase
    {
        protected List<string> InputAsString { get; set; } = new List<string>();
        protected List<long> InputAsLong { get; set; } = new List<long>();
        protected List<int> InputAsInt { get; set; } = new List<int>();

        protected void ReadInput(int day)
        {
            //var path = "../../../" + day.ToString() + "/input";
            var path = day.ToString() + "/input";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            InputAsString = File.ReadAllLines(path).ToList();
        }

        protected void InputToInt()
        {
            InputAsInt = new List<int>();

            foreach (var item in InputAsString)
            {
                InputAsInt.Add(int.Parse(item));
            }
        }


        protected void InputToLong()
        {
            InputAsLong = new List<long>();

            foreach (var item in InputAsString)
            {
                InputAsLong.Add(long.Parse(item));
            }
        }

        protected void GetResultPart1()
        {

        }

        protected void GetResultPart2()
        {

        }

        protected void Assert(int result, int expected)
        {
            if (result != expected)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("TEST ERROR: Expected {0} but got {1}", expected, result);
                Console.ResetColor();
            }
        }
    }
}
