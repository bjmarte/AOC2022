using System.Text;

namespace aoc2022
{
    internal static class Day9
    {
        public static string GetDay9Output()
        {
            Dictionary<Tuple<int, int>, bool> visitedByShortTail = new Dictionary<Tuple<int, int>, bool>();
            Dictionary<Tuple<int, int>, bool> visitedByLongTail = new Dictionary<Tuple<int, int>, bool>();

            var shortRope = new Rope(2);
            var longRope = new Rope(10);

            TrackCurrentTailLocation(shortRope, visitedByShortTail);

            var lines = FileUtils.GetInputFileLines(9);

            foreach (var line in lines)
            {
                var lineChars = line.Split(' ');
                var directionToMove = lineChars[0];
                var numToMove = int.Parse(lineChars[1]);
                ExecuteRopeMoveCommand(shortRope, directionToMove, numToMove, visitedByShortTail);
                ExecuteRopeMoveCommand(longRope, directionToMove, numToMove, visitedByLongTail);
            }

            var builder = new StringBuilder();

            builder.AppendLine($"Number of unique locations visited by the tail of the short rope = {visitedByShortTail.Count()}");
            builder.AppendLine($"Number of unique locations visited by the tail of the long rope = {visitedByLongTail.Count()}");

            return builder.ToString();
        }

        static void TrackCurrentTailLocation(Rope ropeToTrack, Dictionary<Tuple<int, int>, bool> tailLocationTracker)
        {
            tailLocationTracker[ropeToTrack.GetKnotLocation(ropeToTrack.Length-1)] = true;
        }
        static void ExecuteRopeMoveCommand(Rope ropeToMove, string direction, int number, Dictionary<Tuple<int, int>, bool> tailLocationTracker)
        {
            for (int i = 0; i < number; i++)
            {
                ropeToMove.Move(direction);
                TrackCurrentTailLocation(ropeToMove, tailLocationTracker);
            }
        }
    }

    class Rope
    {
        private Tuple<int, int>[] _knotLocations;
        public Rope(int length){
            _knotLocations = GetInitialLocations(length);
            Length = length;
        }

        public int Length{get;}

        public void Move(string direction)
        {
            MoveHead(direction);
            MoveTail();
        }

        public Tuple<int, int> GetKnotLocation(int knotIndex)
        {
            return _knotLocations[knotIndex];
        }

        private void MoveHead(string direction)
        {
            var head = _knotLocations[0];
            
            switch (direction)
            {
                case "R":
                    _knotLocations[0] = new Tuple<int, int>(head.Item1 + 1, head.Item2);
                    break;
                case "L":
                    _knotLocations[0] = new Tuple<int, int>(head.Item1 -1, head.Item2);
                    break;
                case "U":
                    _knotLocations[0] = new Tuple<int, int>(head.Item1, head.Item2 + 1);
                    break;
                case "D":
                    _knotLocations[0] = new Tuple<int, int>(head.Item1, head.Item2 - 1);
                    break;
                default:
                    throw new ArgumentException($"Bad direction command {direction}");
            }
        }

        private void MoveTail()
        {
            for (int i = 1; i < _knotLocations.Length; i++)
            {
                MoveTailKnot(i);
            }
        }

        private void MoveTailKnot(int locationIndex)
        {
            var tailKnotToMove = _knotLocations[locationIndex];
            var previousKnot = _knotLocations[locationIndex-1];
            var hDiff = Math.Abs(previousKnot.Item1 - tailKnotToMove.Item1);
            var vDiff = Math.Abs(previousKnot.Item2 - tailKnotToMove.Item2);
            var newX = tailKnotToMove.Item1;
            var newY = tailKnotToMove.Item2;

            if((hDiff > 1 && vDiff > 0) || (vDiff > 1 && hDiff > 0))
            {
                newX = previousKnot.Item1 > tailKnotToMove.Item1 ? tailKnotToMove.Item1 +1 : tailKnotToMove.Item1 -1;
                newY = previousKnot.Item2 > tailKnotToMove.Item2 ? tailKnotToMove.Item2 +1 : tailKnotToMove.Item2 -1;
            }
            else if(hDiff > 1)
            {
                newX = previousKnot.Item1 > tailKnotToMove.Item1 ? tailKnotToMove.Item1 +1 : tailKnotToMove.Item1 -1;
            }
            else if(vDiff > 1)
            {
                newY = previousKnot.Item2 > tailKnotToMove.Item2 ? tailKnotToMove.Item2 +1 : tailKnotToMove.Item2 -1;
            }

            _knotLocations[locationIndex] = new Tuple<int, int>(newX, newY);
        }



        private Tuple<int, int>[] GetInitialLocations(int length)
        {
            var startLocation = new Tuple<int, int>(0,0);
            var locationArray = new Tuple<int, int>[length];
            for (int i = 0; i < length; i++)
            {
                locationArray[i] = startLocation;
            }
            return locationArray;
        }
    }
}