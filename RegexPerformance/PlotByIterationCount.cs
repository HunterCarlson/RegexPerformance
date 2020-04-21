using System.Collections.Generic;
using System.IO;
using System.Linq;
using XPlot.Plotly;

namespace RegexPerformance
{
    internal class PlotByIterationCount
    {
        public static void Plot(string pattern, bool generateHtml = false)
        {
            var iterationCounts = new List<int>
            {
                1,
                10,
                25,
                50,
                100,
                250,
                500,
                1000,
                2000,
                3000,
                4000,
                5000
            };

            var regexTimer = new RegexTimer();

            List<RegexTimer.RegexTimerResult> timesStandard = iterationCounts
                .Select(iterationCount => regexTimer.Standard(pattern, iterations: iterationCount))
                .ToList();

            List<RegexTimer.RegexTimerResult> timesStandardReused = iterationCounts
                .Select(iterationCount => regexTimer.StandardReused(pattern, iterations: iterationCount))
                .ToList();

            List<RegexTimer.RegexTimerResult> timesCompiled = iterationCounts
                .Select(iterationCount => regexTimer.Compiled(pattern, iterations: iterationCount))
                .ToList();

            var graph1 = new Graph.Scatter
            {
                x = timesStandard.Select(x => x.IterationCount),
                y = timesStandard.Select(x => x.ElapsedTicks),
                name = "Standard Regex"
            };

            var graph2 = new Graph.Scatter
            {
                x = timesStandardReused.Select(x => x.IterationCount),
                y = timesStandardReused.Select(x => x.ElapsedTicks),
                name = "Reused Regex"
            };

            var graph3 = new Graph.Scatter
            {
                x = timesCompiled.Select(x => x.IterationCount),
                y = timesCompiled.Select(x => x.ElapsedTicks),
                name = "Compiled Regex"
            };

            PlotlyChart chart = Chart.Plot(
                new[]
                {
                    graph1,
                    graph2,
                    graph3
                }
            );

            chart.WithXTitle("Iteration Count");
            chart.WithYTitle("Ticks");
            chart.WithTitle("Regex on 1000 characters");

            chart.Show();

            if (generateHtml)
            {
                string html = chart.GetHtml();

                using var outFile = new StreamWriter(
                    Path.Combine(PathUtil.GetSolutionBasePath(), "Plots", "PlotByIterationCount.html")
                );

                outFile.Write(html);
            }
        }
    }
}
