using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode_2021
{
	class Day_08 : BaseDay
	{
		private readonly string[] input;
		private readonly string[] output;

		public Day_08()
		{
			string[] lines = File.ReadAllLines(InputFilePath);
			input = new string[lines.Length];
			output = new string[lines.Length];
			for (int i = 0; i < lines.Length; i++)
			{
				string[] split = lines[i].Split(" | ");
				input[i] = split[0];
				output[i] = split[1];
			}
		}

		public override ValueTask<string> Solve_1()
		{
			int result = 0;

			foreach (string str in output)
			{
				string[] values = str.Split(' ');
				foreach (string v in values)
				{
					if (v.Length == 2 || v.Length == 3 || v.Length == 4 || v.Length == 7)
					{
						result++;
					}
				}
			}

			return new(result.ToString()); // 237
		}

		public override ValueTask<string> Solve_2()
		{
			HashSet<char>[] digits;
			int result = 0;

			for (int i = 0; i < input.Length; i++)
			{
				digits = new HashSet<char>[10];

				string[] values = input[i].Split(' ');

				for (int v = 0; v < values.Length; v++)
				{
					if (values[v].Length == 2) // 1
					{
						digits[1] = new(values[v]);
					}
					else if (values[v].Length == 3) // 7
					{
						digits[7] = new(values[v]);
					}
					else if (values[v].Length == 4) // 4
					{
						digits[4] = new(values[v]);
					}
					else if (values[v].Length == 7) // 8
					{
						digits[8] = new(values[v]);
					}
				}

				for (int v = 0; v < values.Length; v++)
				{
					if (values[v].Length == 5) // 2, 3 or 5
					{
						HashSet<char> vhs = new(values[v]);

						if (vhs.Intersect(digits[1]).Count() == 2)
						{
							// 3 contains all of 1
							digits[3] = vhs;
						}
						else if (vhs.Intersect(digits[4]).Count() == 2)
						{
							// 2 has 2 of 4
							digits[2] = vhs;
						}
						else 
						{
							// 5 is only one left
							digits[5] = vhs;
						}
					}
					else if (values[v].Length == 6) // 6, 9 or 0
					{
						HashSet<char> vhs = new(values[v]);

						if (vhs.Intersect(digits[4]).Count() == 4)
						{
							// 9 contains all of 4
							digits[9] = vhs;
						}
						else if (vhs.Intersect(digits[1]).Count() == 1)
						{
							// 6 contains 1 of 1
							digits[6] = vhs;
						}
						else
						{
							// 0 is only one left
							digits[0] = vhs;
						}
					}
				}			

				string[] outValues = output[i].Split(' ');
				StringBuilder stringBuilder = new();

				for (int o = 0; o < outValues.Length; o++)
				{
					HashSet<char> ohs = new(outValues[o]);

					for (int d = 0; d < digits.Length; d++)
					{
						if (digits[d].SetEquals(ohs))
						{
							stringBuilder.Append(d);
							break;
						}
					}
				}

				result += int.Parse(stringBuilder.ToString());
			}

			return new(result.ToString()); // 1009098
		}

	}
}
