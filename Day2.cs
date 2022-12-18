using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022
{
    internal static class Day2
    {
        private static Dictionary<string, Dictionary<string, int>> _scoresLookup = GetScoresLookup();
        private static Dictionary<string, Dictionary<string, string>> _actionLookup = GetActionLookup();

        public static string GetDay2Output()
        {
            var lines = FileUtils.GetInputFileLines(2);
            var part1Score = 0;
            var part2Score = 0;
            foreach (var line in lines)
            {
                var moves = line.Split(' ');
                var opponentChoice = moves[0];
                var desiredResult = moves[1];
                var myAction = GetDesiredAction(opponentChoice, desiredResult);
                part1Score += GetScore(opponentChoice, desiredResult);
                part2Score += GetScore(opponentChoice, myAction);
            }

            var builder = new StringBuilder();
            builder.AppendLine($"Strategy guide score part 1 is {part1Score}");
            builder.AppendLine($"Strategy guide score part 2 is {part2Score}");

            return builder.ToString();
        }

        private static int GetScore(string opponentChoice, string playerChoice)
        {
            return _scoresLookup[opponentChoice][playerChoice];
        }

        private static string GetDesiredAction(string opponentChoice, string desiredResult)
        {
            return _actionLookup[opponentChoice][desiredResult];
        }

        private static Dictionary<string, Dictionary<string, string>> GetActionLookup()
        {
            return new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "A", new Dictionary<string, string>()
                    {
                        { "X", "Z" },
                        { "Y", "X" },
                        { "Z", "Y" },
                    }
                },
                {
                    "B", new Dictionary<string, string>()
                    {
                        {"X", "X"},
                        {"Y", "Y"},
                        {"Z", "Z"}
                    }
                },
                {
                    "C", new Dictionary<string, string>()
                    {
                        { "X", "Y" },
                        { "Y", "Z" },
                        { "Z", "X"}
                    }
                }
            };
        }

        private static Dictionary<string, Dictionary<string, int>> GetScoresLookup()
        {
            return new Dictionary<string, Dictionary<string, int>>()
            {
                {
                    "A", new Dictionary<string, int>()
                    {
                        {"X", 4 },
                        {"Y", 8 },
                        {"Z", 3 },
                    }
                },
                {
                    "B", new Dictionary<string, int>()
                    {
                        {"X", 1},
                        {"Y", 5},
                        {"Z", 9},
                    }
                },
                {
                    "C", new Dictionary<string, int>()
                    {
                        {"X", 7},
                        {"Y", 2},
                        {"Z", 6},
                    }
                }
            };
        }
    }
}
