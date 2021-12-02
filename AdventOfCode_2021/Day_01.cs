using AoCHelper;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    class Day_01 : BaseDay
    {
        private readonly int[] _input;
        public Day_01()
        {
            _input = ParseInput();
        }

        public override ValueTask<string> Solve_1()
        {
            int result = 0;

            for (int i = 1; i < _input.Length; i++)
            {
                if (_input[i] > _input[i - 1]) result++;
            }

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = 0;

            for (int i = 3; i < _input.Length; i++)
            {
                if ((_input[i] + _input[i-1] + _input[i-2]) > (_input[i - 1] + _input[i-2] + _input[i-3])) result++;
            }

            return new(result.ToString());
        }

        private int[] ParseInput()
        {
            return File.ReadAllLines(InputFilePath).Select(int.Parse).ToArray();
        }
    }
}
