using System;
using Serilog;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Log.Information("Running..");

            var task = new Day6(true);
            task.GetResults();

            watch.Stop();
            Log.Information("It took {0} ms", watch.ElapsedMilliseconds);
        }
    }
}
