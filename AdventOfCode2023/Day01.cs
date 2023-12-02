using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AdventOfCode2023
{
    internal class Day01 : AoC2023
    {
        internal static void Start()
        {
            var lines = TestPrompt(1);
            var watch = Stopwatch.StartNew();

            var calibrationSumNumerical = 0;
            string[] stringDigits = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

            foreach (var line in lines)
            {
                var firstDigit = "";
                var secondDigit = "";
                var first = true;

                for (int i = 0; i < line.Length; i++)
                {
                    var substringLength = Math.Min(5, line.Length - i);
                    var lineSegment = line.Substring(i, substringLength);

                    if (stringDigits.Any(lineSegment.Contains))
                    {
                        var temp = 0;
                        if (lineSegment.StartsWith("one")) temp = 1;
                        if (lineSegment.StartsWith("two")) temp = 2;
                        if (lineSegment.StartsWith("three")) temp = 3;
                        if (lineSegment.StartsWith("four")) temp = 4;
                        if (lineSegment.StartsWith("five")) temp = 5;
                        if (lineSegment.StartsWith("six")) temp = 6;
                        if (lineSegment.StartsWith("seven")) temp = 7;
                        if (lineSegment.StartsWith("eight")) temp = 8;
                        if (lineSegment.StartsWith("nine")) temp = 9;

                        if (temp != 0 && first)
                        {
                            firstDigit = temp.ToString();
                            first = false;
                        }
                        secondDigit = temp.ToString();
                    }

                    if (Char.IsDigit(line[i]))
                    {
                        if (first)
                        {
                            firstDigit = line[i].ToString();
                            first = false;
                        }
                        secondDigit = line[i].ToString();
                    }
                }

                calibrationSumNumerical += Int32.Parse(firstDigit + secondDigit);
            }

            Console.WriteLine("The sum of the calibration values is " + calibrationSumNumerical + ".");
            Summary(watch);
        }
    }
}