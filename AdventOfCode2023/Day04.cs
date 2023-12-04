using System.Diagnostics;

namespace AdventOfCode2023
{
    internal class Day04 : AoC2023
    {
        internal static void Start()
        {
            var lines = TestPrompt(4);
            var watch = Stopwatch.StartNew();

            var totalPoints = 0;
            var cardCount = new int[lines.Length];
            Array.Fill<int>(cardCount, 1);

            for (int l = 0; l < lines.Length; l++)
            {
                var line = lines[l];
                var points = 0;
                var cardsAwarded = 0;
                var card = line.Split(": ");
                card[1].Replace("  ", " ");
                var sides = card[1].Split(" | ");
                var winning = sides[0].Split(" ");
                var player = sides[1].Split(" ");

                for (int i = 0;  i < winning.Length; i++)
                {
                    if (winning[i] == "") continue;

                    for (int j = 0; j < player.Length; j++)
                    {
                        if (winning[i] == player[j])
                        {
                            if (points == 0) points++;
                            else points *= 2;

                            cardsAwarded++;
                        }
                    }
                }

                totalPoints += points;
                
                for (int i = l + 1; i <= l + cardsAwarded; i++)
                {
                    cardCount[i] += cardCount[l];
                }
            }

            Console.WriteLine("The total points of all the cards is " + totalPoints + ".");
            Console.WriteLine("The total number of cards is " + cardCount.Sum() + ".");
            Summary(watch);
        }
    }
}