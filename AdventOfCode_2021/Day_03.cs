using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_03 : BaseDay
	{
		private readonly string[] _input;

		public Day_03()
		{
			_input = File.ReadAllLines(InputFilePath);
		}

		public override ValueTask<string> Solve_1()
		{
			int[] count = new int[_input[0].Length];

			for (int i = 0; i < _input.Length; i++)
			{
				for (int j = 0; j < _input[i].Length; j++)
				{
					if (_input[i][j] == '1') count[j]++;
				}
			}

			string gammaString = "";
			int half = _input.Length / 2;

			foreach (int c in count)
			{
				if (c > half)
				{
					gammaString += '1';
				}
				else
				{
					gammaString += '0';
				}
			}

			int gamma = Convert.ToInt32(gammaString, 2);
			int epsilon = ~gamma & ((1 << gammaString.Length) - 1);

			int result = gamma * epsilon;

			return new(result.ToString());
		}

		public override ValueTask<string> Solve_2()
		{
			List<string> oxygenList = _input.ToList();
			List<string> co2List = _input.ToList();
			int strLength = _input[0].Length;

			// oxygen
			for (int i = 0; i < strLength; i++)
			{
				if (oxygenList.Count == 1) break;

				int c = CountMostCommon(oxygenList.ToArray(), i);

				if (c == 1 || c == 0)
				{
					oxygenList.RemoveAll(str => str[i] == '0');
				}
				else
				{
					oxygenList.RemoveAll(str => str[i] == '1');
				}
			}

			// co2
			for (int i = 0; i < strLength; i++)
			{
				if (co2List.Count == 1) break;

				int c = CountMostCommon(co2List.ToArray(), i);

				if (c == 1 || c == 0)
				{
					co2List.RemoveAll(str => str[i] == '1');
				}
				else
				{
					co2List.RemoveAll(str => str[i] == '0');
				}
			}

			int oxygen = Convert.ToInt32(oxygenList[0], 2);
			int co2 = Convert.ToInt32(co2List[0], 2);

			int result = oxygen * co2;

			return new(result.ToString());
		}

		private static int CountMostCommon(string[] list, int pos)
		{
			int ones = 0;
			int zeros = 0;
			foreach (string str in list)
			{
				if (str[pos] == '1')
				{
					ones++;
				}
				else
				{
					zeros++;
				}
			}

			if (ones == zeros) // same number
			{
				return 0;
			}
			else if (ones > zeros) // one most common
			{
				return 1;
			}
			else // zero most common
			{
				return -1;
			}
		}

	}
}
