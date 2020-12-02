using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class TaskBase
    {
        public List<string> Input { get; set; }

        public void ReadInput(int day)
        {
            var path = "../../../" + day.ToString() + "/input";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            Input = File.ReadAllLines(path).ToList();
        }
    }
}
