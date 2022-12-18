using System.Text;

namespace aoc2022
{
    internal static class Day4
    {
        public static string GetDay4Output()
        {
            var lines = FileUtils.GetInputFileLines(4);
            var fullOverlapCount = 0;
            var partialOverlapCount = 0;
            var builder = new StringBuilder();

            foreach (var line in lines)
            {
                var assignments = line.Split(',', '-').Select(int.Parse).ToArray();
                var assignment1 = assignments[0];
                var assignment2 = assignments[1];
                var assignment3 = assignments[2];
                var assignment4 = assignments[3];

                if (IsPartialOverlap(assignment1, assignment2, assignment3, assignment4))
                    partialOverlapCount++;

                if (IsFullOverlap(assignment1, assignment2, assignment3, assignment4))
                    fullOverlapCount++;
            }

            builder.AppendLine($"The number of fully overlapping assignments is {fullOverlapCount}");
            builder.AppendLine($"The number of partial overlapping assignments is {partialOverlapCount}");

            return builder.ToString();
        }

        public static bool IsPartialOverlap(int assignment1Start, int assignment1End, int assignment2Start, int assignment2End)
        {
            return assignment1Start <= assignment2End && assignment1End >= assignment2Start;
        }

        public static bool IsFullOverlap(int assignment1Start, int assignment1End, int assignment2Start, int assignment2End)
        {
            if (assignment1Start >= assignment2Start && assignment1End <= assignment2End)
            {
                return true;
            }

            if (assignment1Start <= assignment2Start)
            {
                return assignment1End >= assignment2End;
            }

            return false;
        }
    }
}
