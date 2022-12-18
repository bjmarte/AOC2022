using System.Text;

namespace aoc2022
{
    internal static class Day6
    {
        public static string GetDay6Output()
        {
            var input = FileUtils.GetInputFileContents(6);
            var builder = new StringBuilder();
            var packetMarkerLocation = GetUniqueMarkerLocation(input, 4);
            var messageMarkerLocation = GetUniqueMarkerLocation(input, 14);

            builder.AppendLine($"First unique packet marker ends at position {packetMarkerLocation}");
            builder.AppendLine($"First unique message marker ends at position {messageMarkerLocation}");
            return builder.ToString();
        }

        private static long GetUniqueMarkerLocation(string input, int markerLength)
        {
            var lengthMinus = markerLength - 1;
            var markerEndPos = 0;
            var dirtyUntil = lengthMinus;
            for (int i = 1; i < input.Count(); i++)
            {
                var max = i > lengthMinus ? lengthMinus : i;
                var found = false;
                for (int j = 1; j <= max; j++)
                {
                    if (input[i - j] == input[i])
                    {
                        var newDirty = i + (markerLength - j);
                        if (newDirty > dirtyUntil)
                        {
                            dirtyUntil = newDirty;
                        }
                        found = true;
                        break;
                    }
                }

                if (!found && dirtyUntil < i)
                {
                    markerEndPos = i;
                    break;
                }
            }
            return markerEndPos;
        }
    }
}