using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RegexPerformance
{
    public class RegexTimer
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public RegexTimerResult Standard(string pattern, int srtLen = 100, int iterations = 1000)
        {
            var strings = new List<string>();

            for (int i = 0; i < iterations; i++)
            {
                strings.Add(RandomStringGenerator.Generate(srtLen));
            }

            _stopwatch.Reset();
            _stopwatch.Start();

            foreach (string s in strings)
            {
                var regex = new Regex(pattern);
                bool _ = regex.IsMatch(s);
            }

            _stopwatch.Stop();

            return new RegexTimerResult(srtLen, iterations, _stopwatch.ElapsedTicks);
        }

        public RegexTimerResult StandardReused(string pattern, int srtLen = 100, int iterations = 1000)
        {
            var strings = new List<string>();

            for (int i = 0; i < iterations; i++)
            {
                strings.Add(RandomStringGenerator.Generate(srtLen));
            }

            _stopwatch.Reset();
            _stopwatch.Start();

            var regex = new Regex(pattern);

            foreach (string s in strings)
            {
                bool _ = regex.IsMatch(s);
            }

            _stopwatch.Stop();

            return new RegexTimerResult(srtLen, iterations, _stopwatch.ElapsedTicks);
        }

        public RegexTimerResult Compiled(string pattern, int srtLen = 100, int iterations = 1000)
        {
            var strings = new List<string>();

            for (int i = 0; i < iterations; i++)
            {
                strings.Add(RandomStringGenerator.Generate(srtLen));
            }

            _stopwatch.Reset();
            _stopwatch.Start();

            var regexCompiled = new Regex(pattern, RegexOptions.Compiled);

            foreach (string s in strings)
            {
                bool _ = regexCompiled.IsMatch(s);
            }

            _stopwatch.Stop();

            return new RegexTimerResult(srtLen, iterations, _stopwatch.ElapsedTicks);
        }

        public class RegexTimerResult
        {
            public RegexTimerResult(int stringLength, int iterationCount, long elapsedTicks)
            {
                StringLength = stringLength;
                IterationCount = iterationCount;
                ElapsedTicks = elapsedTicks;
            }

            public int StringLength { get; set; }
            public int IterationCount { get; set; }
            public long ElapsedTicks { get; set; }
        }
    }
}
