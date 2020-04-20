using System;
using System.Text;

namespace RegexPerformance
{
    internal static class RandomStringGenerator
    {
        private static readonly Random Random = new Random();

        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=";
        private static readonly int CharCount = CHARS.Length;

        public static string Generate(int length)
        {
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(CHARS[Random.Next(CharCount)]);
            }

            return stringBuilder.ToString();
        }
    }
}
