using System.Text;

namespace aoc2022
{
    internal static class Day5
    {
        private static Stack<char>[] _stackArray = null!; 
        private static Stack<char>[] _stackArray2 = null!; 
        public static string GetDay5Output()
        {
            var lines = FileUtils.GetInputFileLines(5);
            var builder = new StringBuilder();
            var sectionSeparatorIndex = 0;
            var columnsCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].StartsWith('['))
                {
                    sectionSeparatorIndex = i+1;
                    var numsArray = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var numChar = numsArray.Last();
                    columnsCount = int.Parse(numChar);
                    break;
                }
            }

            var columnsLines = lines.Take(sectionSeparatorIndex-1).Reverse();
            var instructionLines = lines.Skip(sectionSeparatorIndex + 1);

            ProcessColumnLines(columnsLines, columnsCount);
            //DumpColumns(builder);

            foreach (var instructionLine in instructionLines)
            {
                var commandNums = instructionLine.Split(new[]{"move ", " from ", " to "}, StringSplitOptions.RemoveEmptyEntries);
                var move = int.Parse(commandNums[0]);
                var from = int.Parse(commandNums[1]);
                var to = int.Parse(commandNums[2]);
                ProcessMoveCommand1(move, from, to);
                ProcessMoveCommand2(move, from, to);
            }

            //DumpColumns(builder);
            builder.AppendLine($"The list of top crates via strategy 1 is {GetFinalResultString(_stackArray)}");
            builder.AppendLine($"The list of top crates via strategy 2 is {GetFinalResultString(_stackArray2)}");
            //builder.AppendLine($"The list of top crates via strategy 2 is");
            return builder.ToString();
        }

        private static string GetFinalResultString(Stack<char>[] stackArray)
        {
            var builder = new StringBuilder();
            foreach (var stack in stackArray)
            {
                builder.Append(stack.Pop());
            }
            return builder.ToString();
        }

        private static void ProcessMoveCommand1(int move, int from, int to)
        {
            for (int i = 0; i < move; i++)
            {
                var itemToMove = _stackArray[from - 1].Pop();
                _stackArray[to - 1].Push(itemToMove);
            }
        }

        private static void ProcessMoveCommand2(int move, int from, int to)
        {
            var itemsToMove = new Stack<char>();

            for (int i = 0; i < move; i++)
            {
                itemsToMove.Push(_stackArray2[from - 1].Pop());
            }

            while (itemsToMove.Count > 0)
            {
                _stackArray2[to-1].Push(itemsToMove.Pop());
            }
        }

        private static void ProcessColumnLines(IEnumerable<string> lines, int numberOfColumns)
        {
            _stackArray = new Stack<char>[numberOfColumns];
            _stackArray2 = new Stack<char>[numberOfColumns];
            for (int i = 0; i < numberOfColumns; i++)
            {
                _stackArray[i] = new Stack<char>();
                _stackArray2[i] = new Stack<char>();
            }

            foreach (var line in lines)
            {
                for (int i = 1; i <= numberOfColumns; i++)
                {
                    var lineArray = line.ToCharArray();
                    var index = i * 4 - 3;
                    var itemChar = lineArray[index];
                    if (itemChar != ' ')
                    {
                        _stackArray[i-1].Push(itemChar);
                        _stackArray2[i-1].Push(itemChar);
                    }
                }
            }
        }

        private static void DumpColumns(StringBuilder builder)
        {
            foreach (var stack in _stackArray)
            {
                var list = stack.Reverse();
                foreach (var item in list)
                {
                    builder.Append(item);
                }
                builder.AppendLine();
            }
        }
    }
}