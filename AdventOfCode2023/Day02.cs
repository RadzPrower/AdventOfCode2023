using System.Diagnostics;
using System.Net.Quic;

namespace AdventOfCode2023
{
    internal class Day02 : AoC2023
    {
        internal static void Start()
        {
            var lines = TestPrompt(2);
            var watch = Stopwatch.StartNew();

            var maxRed = 12;
            var maxGreen = 13;
            var maxBlue = 14;

            var gameIDSum = 0;
            var powerSum = 0;

            foreach (var line in lines)
            {
                var game = line.Split(": ");
                var impossible = false;

                var minRed = int.MinValue;
                var minGreen = int.MinValue;
                var minBlue = int.MinValue;

                var rounds = game[1].Split("; ");
                foreach (var round in rounds)
                {
                    var cubes = round.Split(", ");
                    foreach (var value in cubes)
                    {
                        var color = value.Split(" ");

                        if (color[1].Contains("red"))
                        {
                            var count = Int32.Parse(color[0]);

                            if (count > maxRed) impossible = true;
                            if (count > minRed) minRed = count;
                        }
                        if (color[1].Contains("green"))
                        {
                            var count = Int32.Parse(color[0]);

                            if (count > maxGreen) impossible = true;
                            if (count > minGreen) minGreen = count;
                        }
                        if (color[1].Contains("blue"))
                        {
                            var count = Int32.Parse(color[0]);

                            if (count > maxBlue) impossible = true;
                            if (count > minBlue) minBlue = count;
                        }
                    }
                }

                powerSum += minRed * minGreen * minBlue;

                if (!impossible) gameIDSum += Int32.Parse(game[0].Substring(5));
            }

            Console.WriteLine("The sum of valid game IDs is " + gameIDSum + ".");
            Console.WriteLine("The sum of each set's power is " + powerSum + ".");
            Summary(watch);
        }
    }
}