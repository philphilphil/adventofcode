using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day6 : TaskBase
    {
        public Day6()
        {
            base.ReadInput(6);
        }

        internal void GetResults()
        {
            GetResultPart1();
            Console.WriteLine();
            Console.WriteLine();
            GetResultPart2();
            Console.WriteLine();
        }

        private List<string> GetGroupAnswers()
        {
            var groupAnswers = new List<string>();
            var answersString = "";

            foreach (var item in Input)
            {
                if (item == String.Empty)
                {
                    groupAnswers.Add(answersString);
                    answersString = "";
                }
                else
                {
                    answersString += item.Replace(" ", "");
                }
            }
            return groupAnswers;
        }

        private List<List<string>> GetGroupAnswersByPerson()
        {
            var groupAnswers = new List<List<string>>();
            var personAnswers = new List<string>();

            foreach (var item in Input)
            {
                if (item == String.Empty)
                {
                    groupAnswers.Add(personAnswers);
                    personAnswers = new List<string>();
                }
                else
                {
                    personAnswers.Add(item);
                }
            }
            return groupAnswers;
        }

        public void GetResultPart1()
        {
            int yes = 0;
            var groupAnswers = GetGroupAnswers();

            foreach (var a in groupAnswers)
            {
                yes += a.Distinct().Count();
            }

            Console.Write(String.Format("Part 1 yes given: {0}", yes.ToString()));
        }

        public void GetResultPart2()
        {
            int yes = 0;
            var groupAnswers = GetGroupAnswersByPerson();

            foreach (var a in groupAnswers)
            {
                HashSet<char> hashSet = new HashSet<char>(a[0]);

                for (int i = 1; i < a.Count; i++)
                {
                    hashSet.IntersectWith(a[i]);
                }

                List<char> intersection = hashSet.ToList();
                yes += intersection.Count;
            }

            Console.Write(String.Format("Part 2 yes given: {0}", yes.ToString()));
        }
    }
}
