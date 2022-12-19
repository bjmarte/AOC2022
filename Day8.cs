using System.Text;

namespace aoc2022
{
    internal static class Day8
    {
        private static int _gridWidth = 0;
        private static int _gridHeight = 0;
        private static Dictionary<Tuple<int, int>, bool> _visibleTreesDict = new Dictionary<Tuple<int, int>, bool>();
        
        public static string GetDay8Output()
        {
            var input = FileUtils.GetInputFileLines(8).Select(l => l.ToCharArray().Select(t => t - '0').ToArray()).ToArray();
            _gridWidth = input[0].Length;
            _gridHeight = input.Length;
            var builder = new StringBuilder();
            
            LookForTreesDown(input);
            LookForTreesUp(input);
            LookForTreesRight(input);
            LookForTreesLeft(input);

            var visibleTrees = _visibleTreesDict.Values.Count;

            var bestVisibility = 0;
            
            for (int i = 0; i < _gridHeight; i++)
            {
                for (int j = 0; j < _gridWidth; j++)
                {
                    var visibility = CalculateTreeVisibility(i, j, input);
                    if (visibility > bestVisibility)
                    {
                        bestVisibility = visibility;
                    }
                }
            }

            builder.AppendLine($"Number of trees visible from outside the grid = {visibleTrees}");
            builder.AppendLine($"Highest visibility score is {bestVisibility}");
            return builder.ToString();
        }

        static int CalculateTreeVisibility(int i, int j, int[][] input)
        {
            var treeHeight = input[i][j];
            int upScore = 0, downScore = 0, rightScore = 0, leftScore = 0;

            for (int x = i - 1; x >= 0; x--)
            {
                upScore++;
                var nextTreeHeight = input[x][j];
                if (nextTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            for (int x = i + 1; x < _gridHeight; x++)
            {
                downScore++;
                var nextTreeHeight = input[x][j];
                if (nextTreeHeight >= treeHeight)
                {
                    break;
                } 
            }

            for (int x = j - 1; x >= 0; x--)
            {
                leftScore++;
                var nextTreeHeight = input[i][x];
                if (nextTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            for (int x = j + 1; x < _gridWidth; x++)
            {
                rightScore++;
                var nextTreeHeight = input[i][x];
                if (nextTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            return upScore * downScore * leftScore * rightScore;
        }

        static void LookForTreesDown(int[][] input)
        {
            for (int i = 0; i < _gridWidth; i++)
            {
                var maxHeight = -1;
                for (int j = 0; j < _gridHeight; j++)
                {
                    var currentHeight = input[j][i];
                    if (currentHeight > maxHeight)
                    {
                        _visibleTreesDict[new Tuple<int, int>(j, i)] = true;
                        maxHeight = currentHeight;
                    }
                }
            }
        }

        static void LookForTreesUp(int[][] input)
        {
            for (int i = 0; i < _gridWidth; i++)
            {
                var maxHeight = -1;
                for (int j = _gridHeight -1; j >= 0; j--)
                {
                    var currentHeight = input[j][i];
                    if (currentHeight > maxHeight)
                    {
                        _visibleTreesDict[new Tuple<int, int>(j, i)] = true;
                        maxHeight = currentHeight;
                    }
                }
            }
        }

        static void LookForTreesRight(int[][] input)
        {
            for (int i = 0; i < _gridHeight; i++)
            {
                var maxHeight = -1;
                for (int j = 0; j < _gridWidth; j++)
                {
                    var currentHeight = input[i][j];
                    if (currentHeight > maxHeight)
                    {
                        _visibleTreesDict[new Tuple<int, int>(i, j)] = true;
                        maxHeight = currentHeight;
                    }
                }
            }
        }

        static void LookForTreesLeft(int[][] input)
        {
            for (int i = 0; i < _gridHeight; i++)
            {
                var maxHeight = -1;
                for (int j = _gridWidth -1; j >= 0; j--)
                {
                    var currentHeight = input[i][j];
                    if (currentHeight > maxHeight)
                    {
                        _visibleTreesDict[new Tuple<int, int>(i, j)] = true;
                        maxHeight = currentHeight;
                    }
                }
            }
        }
    }
}