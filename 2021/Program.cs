using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var task = new Day6(true);
            task.GetResults();

            watch.Stop();
            Console.WriteLine("It took {0}ms", watch.ElapsedMilliseconds);
        }
    }
}
