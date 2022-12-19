using System.Collections.ObjectModel;
using System.Text;

namespace aoc2022
{
    internal static class Day7
    {
        static Stack<Directory> _currentDirectoryStack = new();
        static Directory _currentDirectory;
        static Dictionary<string, Directory> _directories = new();
        static Dictionary<string, long> _totalSizes = new();

        public static string GetDay7Output()
        {
            var lines = FileUtils.GetInputFileLines(7);
            var builder = new StringBuilder();

            foreach (var line in lines)
            {
                if (IsChangeDirectoryCommand(line))
                {
                    ProcessChangeDirectoryCommand(line);
                }
                else if (IsListDirectoryCommand(line))
                {
                    ProcessListDirectoryCommand(line);
                }
                else if (IsDirectoryListing(line))
                {
                    ProcessDirectoryListing(line);
                }
                else
                {
                    ProcessFileListing(line);
                }
            }
            var part1Score = CalculateTotalSizeOfDirectoriesLessThanMax(100000);
            var sizeOfSmallestDirectoryToDelete = CalculateSizeOfFolderToDelete();
            
            builder.AppendLine($"Sum of sizes for directories <= 100000 = {part1Score}");
            builder.AppendLine($"Size of smallest directory to delete to free up required space = {sizeOfSmallestDirectoryToDelete}");
            return builder.ToString();
        }

        public static long CalculateSizeOfFolderToDelete()
        {

            var rootDir = _directories["_/"];
            var totalSize = GetTotalSize(rootDir);
            var freeSize = 70000000 - totalSize;
            var sizeNeeded = 30000000 - freeSize;
            var smallestMatchingSize = totalSize;

            var candidateQueue = new Queue<Directory>(_directories.Where(d => rootDir.ChildDirectories.Contains(d.Key)).Where(d => GetTotalSize(d.Value) >= sizeNeeded).Select(d => d.Value));
            while (candidateQueue.Count > 0)
            {
                var candidateDirectory = candidateQueue.Dequeue();
                var candidateSize = GetTotalSize(candidateDirectory);
                if (candidateSize < smallestMatchingSize)
                {
                    smallestMatchingSize = candidateSize;
                }

                var childCandidates = _directories.Where(d => candidateDirectory.ChildDirectories.Contains(d.Key)).Where(d => GetTotalSize(d.Value) >= sizeNeeded).Select(d => d.Value);
                foreach (var childCandidate in childCandidates)
                {
                    candidateQueue.Enqueue(childCandidate);
                }

            }

            return smallestMatchingSize;
        }

        public static long CalculateTotalSizeOfDirectoriesLessThanMax(int maxSize)
        {
            var matchingLeafNodes = _directories.Values.Where(d => d.FileSizeTotal <= maxSize && !d.ChildDirectories.Any()).ToList();
            var matchingDirectories = new Queue<Directory>(matchingLeafNodes);
            _totalSizes = matchingLeafNodes.ToDictionary(d => d.Name, d => d.FileSizeTotal);
            long score = 0;
            while (matchingDirectories.Count > 0)
            {
                var directory = matchingDirectories.Dequeue();
                score += _totalSizes[directory.Name];
                var parentDir = _directories[directory.Parent];
                if (GetTotalSize(parentDir, maxSize) <= maxSize)
                {
                    matchingDirectories.Enqueue(parentDir);
                }
            }
            return score;
        }

        static long GetTotalSize(Directory dir, long maxSize = long.MaxValue, long parentSize = 0)
        {
            if (_totalSizes.ContainsKey(dir.Name))
                return _totalSizes[dir.Name];

            if (dir.FileSizeTotal + parentSize > maxSize)
                return maxSize + 1;

            long totalSize = dir.FileSizeTotal;
            foreach (var childDirName in dir.ChildDirectories)
            {
                var childDir = _directories[childDirName];
                if (parentSize + totalSize + childDir.FileSizeTotal > maxSize)
                    return maxSize + 1;
                totalSize += GetTotalSize(childDir, maxSize, totalSize + parentSize);
                if (totalSize > maxSize)
                    return maxSize + 1;
            }
            _totalSizes.Add(dir.Name, totalSize);
            return totalSize;
        }

        public static void ProcessFileListing(string line)
        {
            var lineParts = line.Split(' ');
            _currentDirectory.AddFile(long.Parse(lineParts[0]));
        }

        public static void ProcessDirectoryListing(string line)
        {
            var directoryName = line.Substring(4);
            _currentDirectory.AddDirectory(directoryName);
        }

        public static void ProcessListDirectoryCommand(string line)
        {
            // nothing to do here
        }

        public static void ProcessChangeDirectoryCommand(string line)
        {
            string newDirName = line.Substring(5);
            if (newDirName == "..")
            {
                _currentDirectory = _currentDirectoryStack.Pop();
            }
            else
            {
                if (_currentDirectory != null)
                    _currentDirectoryStack.Push(_currentDirectory);
                _currentDirectory = new Directory(newDirName, _currentDirectory?.Name);
                _directories.Add(_currentDirectory.Name, _currentDirectory);
            }
        }


        public static bool IsDirectoryListing(string line)
        {
            return line.StartsWith("dir ");
        }

        public static bool IsListDirectoryCommand(string line)
        {
            return line == "$ ls";
        }

        public static bool IsChangeDirectoryCommand(string line)
        {
            return line.StartsWith("$ cd");
        }
    }

    class Directory
    {
        public Directory(string name, string parent)
        {
            Name = $"{parent}_{name}";
            Parent = parent;
        }

        readonly List<string> _childDirectories = new();
        public string Name { get; }
        public string Parent { get; }
        public ReadOnlyCollection<string> ChildDirectories => _childDirectories.AsReadOnly();
        public long FileSizeTotal { get; private set; }

        public void AddFile(long size)
        {
            FileSizeTotal += size;
        }

        public void AddDirectory(string childName)
        {
            _childDirectories.Add($"{Name}_{childName}");
        }
    }
}