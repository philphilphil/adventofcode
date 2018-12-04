using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LogEntry> logs = GetLogEntries();
            GetGuardWithMostTimeAsleep(logs);

            Console.Read();
        }

        private static void GetGuardWithMostTimeAsleep(List<LogEntry> logs)
        {
            List<Guard> guardInfos = new List<Guard>();
            int activeGuardId = 0;
            int minuteFellAsleep = 0;

            foreach (LogEntry log in logs)
            {
                if (log.GuardNumber != 0)
                {
                    activeGuardId = log.GuardNumber;
                    minuteFellAsleep = 0;
                }

                //add guard to list if new
                if (!guardInfos.Any(x => x.Id == activeGuardId))
                {
                    Guard g = new Guard();
                    g.Id = activeGuardId;
                    g.DayAndTimeAsleep = new Dictionary<int, List<int>>();
                    guardInfos.Add(g);
                }

                if (log.Message == "falls asleep")
                {
                    minuteFellAsleep = log.Minute;
                }
                else if (log.Message == "wakes up")
                {
                    var g = guardInfos.Where(x => x.Id == activeGuardId).FirstOrDefault();

                    //for each minute asleep, write into the list
                    for (int i = minuteFellAsleep; minuteFellAsleep < log.Minute; minuteFellAsleep++)
                    {
                        if (!g.DayAndTimeAsleep.ContainsKey(log.Day))
                            g.DayAndTimeAsleep.Add(log.Day, new List<int>());

                        g.DayAndTimeAsleep[log.Day].Add(minuteFellAsleep);
                        g.TotalMinuesAsleep++;
                    }
                }
            }

            var mostAsleepGuard = guardInfos.OrderByDescending(x => x.TotalMinuesAsleep).First();

            //find most minute asleep
            Dictionary<int, int> minuteAsleepCount = new Dictionary<int, int>();

            foreach (var d in mostAsleepGuard.DayAndTimeAsleep)
            {
                foreach (var m in d.Value)
                {

                    if (!minuteAsleepCount.ContainsKey(m))
                        minuteAsleepCount.Add(m, 0);

                    minuteAsleepCount[m]++;
                }
            }

            int mostAsleepMinute = minuteAsleepCount.OrderByDescending(x => x.Value).Select(x => x.Key).First();

            Console.WriteLine(String.Format(" Guard #{0} was asleep {1} minutes and mostly during minute {2}", mostAsleepGuard.Id, mostAsleepGuard.TotalMinuesAsleep, mostAsleepMinute));
            Console.WriteLine("Answer: " + (mostAsleepGuard.Id * mostAsleepMinute).ToString());
        }

        private static List<LogEntry> GetLogEntries()
        {
            List<LogEntry> logs = new List<LogEntry>();
            var inputFile = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\input.txt");

            foreach (var line in inputFile)
            {
                Regex regex = new Regex(@"\[1518-(\d*)-(\d*) (\d*):(\d*)\] (.*)");
                Match match = regex.Match(line);

                LogEntry l = new LogEntry();
                l.Month = int.Parse(match.Groups[1].ToString());
                l.Day = int.Parse(match.Groups[2].ToString());
                l.Hour = int.Parse(match.Groups[3].ToString());
                l.Minute = int.Parse(match.Groups[4].ToString());
                l.Message = match.Groups[5].Value;

                if (l.Message.Contains("#"))
                {
                    Regex regex2 = new Regex(@"#(\d*)");
                    Match match2 = regex2.Match(line);

                    l.GuardNumber = int.Parse(match2.Groups[1].Value);
                }

                logs.Add(l);
            }

            //sort logs
            logs = logs.OrderBy(x => x.Month).ThenBy(x => x.Day).ThenBy(x => x.Minute).ToList();

            return logs;
        }
    }
    class LogEntry
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Minute { get; set; }
        public int Hour { get; set; }
        public int GuardNumber { get; set; }
        public string Message { get; set; }
    }

    class Guard
    {
        public int Id { get; set; }
        public Dictionary<int, List<int>> DayAndTimeAsleep { get; set; }
        public int TotalMinuesAsleep { get; set; }
    }
}
