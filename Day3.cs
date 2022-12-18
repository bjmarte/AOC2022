using System.Text;

namespace aoc2022
{
    internal static class Day3
    {
        public static string GetDay3Output()
        {
            var lines = FileUtils.GetInputFileLines(3);
            var score = 0;
            foreach (var line in lines)
            {
                var compartment1 = line.Substring(0, line.Length / 2).ToCharArray();
                var compartment2 = line.Substring(line.Length / 2).ToCharArray();
                var duplicateItem = GetDuplicateItem(compartment1, compartment2);
                score += GetPriority(duplicateItem);
            }

            var badgeScore = 0;
            for (int i = 0; i < lines.Length; i += 3)
            {
                var rucksack1 = lines[i].ToCharArray();
                var rucksack2 = lines[i + 1].ToCharArray();
                var rucksack3 = lines[i + 2].ToCharArray();

                var badge = GetGroupBadge(rucksack1, rucksack2, rucksack3);
                badgeScore += GetPriority(badge);
            }

            var builder = new StringBuilder();
            builder.AppendLine($"The sum of duplicate item priorities is {score}");
            builder.AppendLine($"The sum of group badge priorities is {badgeScore}");

            return builder.ToString();
        }

        public static char GetGroupBadge(char[] rucksack1, char[] rucksack2, char[] rucksack3)
        {
            foreach (var item in rucksack1)
            {
                if(rucksack2.Contains(item) && rucksack3.Contains(item))
                    return item;
            }

            throw new ArgumentException("Group badge not found");
        }

        public static char GetDuplicateItem(char[] compartment1, char[] compartment2)
        {
            foreach (var item in compartment1)
            {
                if (compartment2.Contains(item))
                {
                    return item;
                }
            }

            throw new ArgumentException("Duplicate item not found!");
        }

        public static int GetPriority(char item)
        {
            var val = (int)item;
            
            return val < 97 ? val-38 : val - 96;
        }
    }
}
