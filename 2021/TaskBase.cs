using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class TaskBase
    {
        public List<string> InputAsString { get; set; } = new List<string>();
        public List<long> InputAsLong { get; set; } = new List<long>();
        public List<int> InputAsInt { get; set; } = new List<int>();

        public void ReadInput(int day)
        {
            var path = day.ToString() + "/input";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            InputAsString = File.ReadAllLines(path).ToList();
        }

        public void InputToInt()
        {
            InputAsInt = new List<int>();

            foreach (var item in InputAsString)
            {
                InputAsInt.Add(int.Parse(item));
            }
        }


        public void InputToLong()
        {
            InputAsLong = new List<long>();

            foreach (var item in InputAsString)
            {
                InputAsLong.Add(long.Parse(item));
            }
        }

        public void GetResultPart1()
        {

        }

        public void GetResultPart2()
        {

        }
    }
}
