using AoCHelper;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    class Day_02 : BaseDay
    {
        private readonly string[] _input;

        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            int horizontalPosition = 0;
            int depth = 0;

            foreach (string line in _input)
            {
                if (line.StartsWith("f"))
                {
                    horizontalPosition += int.Parse(line[line.IndexOf(' ')..]);
                } 
                else if (line.StartsWith("d"))
                {
                    depth += int.Parse(line[line.IndexOf(' ')..]);
                }
                else
                {
                    depth -= int.Parse(line[line.IndexOf(' ')..]);
                }
            }

            return new((horizontalPosition * depth).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in _input)
            {
                if (line.StartsWith("f"))
                {
                    int value = int.Parse(line[line.IndexOf(' ')..]);
                    horizontalPosition += value;
                    depth += (aim * value);
                }
                else if (line.StartsWith("d"))
                {
                    aim += int.Parse(line[line.IndexOf(' ')..]);
                }
                else
                {
                    aim -= int.Parse(line[line.IndexOf(' ')..]);
                }
            }

            return new((horizontalPosition * depth).ToString());
        }
    }
}
