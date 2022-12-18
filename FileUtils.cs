namespace aoc2022
{
    internal static class FileUtils
    {
        public static string GetInputFileContents(int day)
        {
            return File.ReadAllText($"InputFiles\\Day{day}.txt");
        }

        public static string[] GetInputFileLines(int day)
        {
            return File.ReadAllLines($"InputFiles\\Day{day}.txt");
        }
    }   
}
