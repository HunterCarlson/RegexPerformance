using System.Collections.Generic;
using System.IO;
using System.Linq;
using XPlot.Plotly;

namespace RegexPerformance
{
    public static class PlotByStringLength
    {
        public static void Plot(string pattern)
        {
            var stringLengths = new List<int>
            {
                1,
                10,
                25,
                50,
                100,
                250,
                500,
                1000,
                5000
            };

            var regexTimer = new RegexTimer();

            List<RegexTimer.RegexTimerResult> timesStandard = stringLengths
                .Select(stringLength => regexTimer.Standard(pattern, stringLength)).ToList();

            List<RegexTimer.RegexTimerResult> timesStandardReused = stringLengths
                .Select(stringLength => regexTimer.StandardReused(pattern, stringLength)).ToList();

            List<RegexTimer.RegexTimerResult> timesCompiled = stringLengths
                .Select(stringLength => regexTimer.Compiled(pattern, stringLength)).ToList();

            var graph1 = new Graph.Scatter
            {
                x = timesStandard.Select(x => x.StringLength),
                y = timesStandard.Select(x => x.ElapsedTicks),
                name = "Standard Regex"
            };

            var graph2 = new Graph.Scatter
            {
                x = timesStandardReused.Select(x => x.StringLength),
                y = timesStandardReused.Select(x => x.ElapsedTicks),
                name = "Reused Regex"
            };

            var graph3 = new Graph.Scatter
            {
                x = timesCompiled.Select(x => x.StringLength),
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

            chart.WithXTitle("String Length");
            chart.WithYTitle("Ticks");
            chart.WithTitle("Regex on 1000 iterations");

            chart.Show();

            string html = chart.GetHtml();
            using (var outFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "PlotByStringLength.html")))
            {
                outFile.WriteAsync(html);
            }
        }
    }
}
