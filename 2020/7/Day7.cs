using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day7 : TaskBase
    {
        private string _myBag = "shiny gold";
        private int _myBagCount = 0;
        private List<Bag> _bags = new List<Bag>();
        public Day7()
        {
            base.ReadInput(7);
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
            string pattern = @"(.*) bags contain(.*)";
            foreach (var line in base.Input)
            {
                var m = Regex.Match(line, pattern);
                var bag = GetBag(m.Groups[1].Value.Trim());

                if (m.Groups[2].Value.Contains("no other"))
                    continue;

                var innerBags = m.Groups[2].Value.Split(",");
                bag.InnerBags = new Dictionary<Bag, int>();

                foreach (var innerBag in innerBags)
                {
                    string pattern2 = @"(\d) (.*) bags*";
                    Match m2 = Regex.Match(innerBag, pattern2);

                    var innerBagOb = GetBag(m2.Groups[2].Value.Trim());
                    bag.InnerBags.Add(innerBagOb, int.Parse(m2.Groups[1].Value));
                }
            }

            foreach (var bag in _bags)
            {
                if (bag.InnerBags == null)
                    continue;

                if (CheckForBag(bag.InnerBags))
                    _myBagCount++;
            }

            Console.WriteLine("My {0} can be inside {1} other bags", _myBag, _myBagCount);
        }

        private Bag GetBag(string v)
        {
            var bag = _bags.Where(x => x.Name == v).FirstOrDefault();

            if (bag == null)
            {
                bag = new Bag { Name = v };
                _bags.Add(bag);
            }

            return bag;
        }

        private bool CheckForBag(Dictionary<Bag, int> innerBags)
        {
            foreach (var bag in innerBags)
            {
                if (bag.Key.Name == _myBag)
                    return true;

                if (bag.Key.InnerBags != null)
                    if (CheckForBag(bag.Key.InnerBags))
                        return true;
            }

            return false;
        }

        public void GetResultPart2()
        {

        }
    }

    class Bag
    {
        public string Name { get; set; }

        public Dictionary<Bag, int> InnerBags { get; set; }
    }
}
