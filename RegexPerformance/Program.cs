using System.Text.RegularExpressions;

namespace RegexPerformance
{
    internal class Program
    {
        private static void Main()
        {
            var regex = new Regex(@"/^(?=.*[\d])(?=.*[a-z])(?=.*[A-Z])\S{8,4096}$/");

            PlotByStringLength.Plot(regex.ToString(), true);
            PlotByIterationCount.Plot(regex.ToString(), true);
        }
    }
}
