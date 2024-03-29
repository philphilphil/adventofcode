﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    class DayX : TaskBase
    {

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

            var answer = 0;
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }

        private new void GetResultPart2()
        {
            var answer = 0;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }
    }
}
