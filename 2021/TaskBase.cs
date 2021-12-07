using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class TaskBase
    {
        public bool Demo { get; set; }
        protected List<string> InputAsString { get; set; } = new List<string>();
        protected List<long> InputAsLong { get; set; } = new List<long>();
        protected List<int> InputAsInt { get; set; } = new List<int>();

        protected void ReadInput(int day, bool demo = false)
        {
            //var path = "../../../" + day.ToString() + "/input";
            var path = day.ToString() + "/input";

            if (demo)
                path += "Demo";

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
                Log.Error("Expected {0} but got {1}", expected, result);
            }
        }

        protected void Assert(string result, string expected)
        {
            if (result != expected)
            {
                Log.Error("Expected {0} but got {1}", expected, result);
            }
        }


        protected void Assert(long result, long expected)
        {
            if (result != expected)
            {
                Log.Error("Expected {0} but got {1}", expected, result);
            }
        }
    }
}
