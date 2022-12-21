using System.Text;

namespace aoc2022
{
    internal static class Day10
    {
        static Stack<int> cycleCheckpoints = GetCycleCheckpoints();
        public static string GetDay10Output()
        {
            var xRegister = 1;
            var cycleCount = 0;
            var signalStrengthSum =0;

            var nextCycleCheckpoint = cycleCheckpoints.Pop();
            var lines = FileUtils.GetInputFileLines(10);
            var pt2Builder = new StringBuilder("Pt 2 Output: \r\n");
            foreach (var line in lines)
            {
                var commands = line.Split(' ');
                if(commands[0] == "addx")
                {
                    AddPt2Output(cycleCount, xRegister, pt2Builder);
                    AddPt2Output(cycleCount + 1, xRegister, pt2Builder);
                    cycleCount +=2;
                    if(cycleCount >= nextCycleCheckpoint)
                    {
                        signalStrengthSum += xRegister * nextCycleCheckpoint;
                        if(cycleCheckpoints.Count > 0)
                        {
                            nextCycleCheckpoint = cycleCheckpoints.Pop();
                        }
                        else
                        {
                            nextCycleCheckpoint = int.MaxValue;
                        }
                    }
                    xRegister += int.Parse(commands[1]);
                }
                else
                {
                    AddPt2Output(cycleCount, xRegister, pt2Builder);
                    cycleCount += 1;
                }
            }
            pt2Builder.AppendLine();
            var builder = new StringBuilder();

            builder.AppendLine($"Part 1 signal strength sum = {signalStrengthSum}");
            builder.Append(pt2Builder);
            return builder.ToString();
        }

        static void AddPt2Output(int cycleCount, int xRegister, StringBuilder outputBuilder){
            int charIndex = cycleCount % 40;
            if(charIndex == 0 && cycleCount > 0){
                outputBuilder.AppendLine();
            }
            if(xRegister -1 <= charIndex && charIndex <= xRegister+1)
            {
                outputBuilder.Append("#");
            }
            else{
                outputBuilder.Append(".");
            }
        }

        static Stack<int> GetCycleCheckpoints(){
            var checkpointsStack = new Stack<int>();
            checkpointsStack.Push(220);             
            checkpointsStack.Push(180);             
            checkpointsStack.Push(140);             
            checkpointsStack.Push(100);             
            checkpointsStack.Push(60);             
            checkpointsStack.Push(20);
            return checkpointsStack;             
        }
    }
}