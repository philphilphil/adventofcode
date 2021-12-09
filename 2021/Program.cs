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

            bool runWithDemoInput = false;
            if (args.Length == 1 && args[0] == "demo")
                runWithDemoInput = true;

            var task = new Day9(runWithDemoInput);
            task.GetResults();

            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            Log.Information("It took {0}h {1}m {2}s {3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
