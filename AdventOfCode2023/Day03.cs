using System.Diagnostics;

namespace AdventOfCode2023
{
    internal class Day03 : AoC2023
    {
        internal static void Start()
        {
            var lines = TestPrompt(3);
            var watch = Stopwatch.StartNew();

            var numberList = new List<SchematicNumber>();
            var gearList = new List<Gear>();
            var partNumberSum = 0;
            var gearRatioSum = 0;

            for (int j = 0; j < lines.Length; j++)
            {
                for (int i = 0; i < lines[j].Length; i++)
                {
                    var numberString = "";
                    var numberStart = i;

                    while (i < lines[j].Length && Char.IsDigit(lines[j][i]))
                    {
                        numberString += lines[j][i];
                        i++;
                    }

                    if (numberString.Length > 0)
                    {
                        numberList.Add(new SchematicNumber(numberString, j, numberStart, ref lines, ref gearList));
                    }
                }
            }

            foreach (var number in numberList)
            {
                if (number.PartNumber) partNumberSum += number.Number;
            }

            foreach (var gear in gearList)
            {
                gearRatioSum += gear.GetGearRatio();
            }

            Console.WriteLine("The sum of all part numbers is " + partNumberSum + ".");
            Console.WriteLine("The sum of the gear ratios is " + gearRatioSum + ".");
            Summary(watch);
        }
    }

    internal class SchematicNumber
    {
        private int number;
        private int row;
        private int col;
        private int length;
        private Boolean partNumber;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public Boolean PartNumber
        {
            get { return partNumber; }
        }

        public SchematicNumber(string numberInput, int rowInput, int colInput, ref string[] lines, ref List<Gear> gearList)
        {
            number = Int32.Parse(numberInput);
            row = rowInput;
            col = colInput;
            length = numberInput.Length;
            partNumber = IsPartNumber(ref lines, ref gearList);
        }

        private Boolean IsPartNumber(ref string[] lines, ref List<Gear> gearList)
        {
            var left = Math.Max(0, col - 1);
            var right = Math.Min(lines[0].Length - 1, col + length);
            var top = Math.Max(0, row - 1);
            var bottom = Math.Min(lines.Length - 1, row + 1);

            for (var rows = top; rows <= bottom; rows++)
            {
                for (var cols =  left; cols <= right; cols++)
                {
                    if (lines[rows][cols] != '.' && !Char.IsDigit(lines[rows][cols]))
                    {
                        if (lines[rows][cols].Equals('*'))
                        {
                            var matchFound = false;

                            foreach (var gear in gearList)
                            {
                                if (gear.Match(rows, cols))
                                {
                                    gear.AddPartNumber(number);
                                    matchFound = true;
                                }

                                if (matchFound) break;
                            }

                            if (!matchFound)
                            {
                                var gear = new Gear(rows, cols);
                                gear.AddPartNumber(number);
                                gearList.Add(gear);
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }

    internal class Gear
    {
        private int x;
        private int y;
        private List<int> partNumbers = new List<int>();

        public Gear(int xInput, int yInput)
        {
            x = xInput;
            y = yInput;
        }

        public void AddPartNumber(int number)
        {
            partNumbers.Add(number);
        }

        public Boolean Match(int xInput, int yInput)
        {
            if (x == xInput && y == yInput) return true;
            else return false;
        }

        public int GetGearRatio()
        {
            if (partNumbers.Count != 2)
            {
                return 0;
            }

            return partNumbers[0] * partNumbers[1];
        }
    }
}