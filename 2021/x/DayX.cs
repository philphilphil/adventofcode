﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class DayX : TaskBase
    {

        private bool Demo { get; set; }

        public DayX(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(0, Demo);
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            foreach (string input in InputAsString)
            {
            }

            var answer = "answer";
            Console.WriteLine("Part 1 answer: {0}", "answer");
            Assert(answer, "4138664");
        }

        private new void GetResultPart2()
        {
            var answer = "answer";
            Console.WriteLine("Part 2 answer: {0}", "answer");
            Assert(answer, "4138664");
        }
    }
}
