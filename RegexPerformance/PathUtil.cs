using System;
using System.IO;

namespace RegexPerformance
{
    public static class PathUtil
    {
        public static string GetSolutionBasePath()
        {
            string slnPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

            return slnPath;
        }
    }
}
