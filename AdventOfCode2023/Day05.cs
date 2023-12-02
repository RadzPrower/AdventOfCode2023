using System.Diagnostics;

namespace AdventOfCode2023
{
    internal class Day05 : AoC2023
    {
        internal static void Start()
        {
            var lines = TestPrompt(1);
            var watch = Stopwatch.StartNew();

            Console.WriteLine("");
            Console.WriteLine("");
            Summary(watch);
        }
    }
}