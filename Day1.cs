using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2022
{
    internal static class Day1
    {
        public static string GetDay1Output()
        {
            var lines = FileUtils.GetInputFileLines(1);
            var totals = new List<int>();
            int total = 0;
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    total += int.Parse(line);
                }
                else
                {
                    totals.Add(total);
                    total = 0;
                }
            }
            totals.Add(total);

            var biggestTotal = totals.Max();
            var totalOf3Biggest = totals.OrderByDescending(t => t).Take(3).Sum();
            var builder = new StringBuilder();
            builder.AppendLine($"Elf with the most calories has {biggestTotal} calories");
            builder.AppendLine($"The top 3 elves are carrying {totalOf3Biggest} calories");

            return builder.ToString();
        }
    }
}
