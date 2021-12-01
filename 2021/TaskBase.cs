using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class TaskBase
    {
        public List<string> Input { get; set; }

        public List<long> InputAsLong { get; set; }

        public List<int> InputAsInt { get; set; }

        public void ReadInput(int day)
        {
            var path = day.ToString() + "/input";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            Input = File.ReadAllLines(path).ToList();
        }

        public void InputToInt()
        {
            InputAsInt = new List<int>();

            foreach (var item in Input)
            {
                InputAsInt.Add(int.Parse(item));
            }
        }
    }
}
