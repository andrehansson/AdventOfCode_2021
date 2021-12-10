using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_10 : BaseDay
	{
		private readonly string[] input;
		private readonly List<string> incomplete;

		public Day_10()
		{
			input = File.ReadAllLines(InputFilePath);
			incomplete = new();
		}

		public override ValueTask<string> Solve_1()
		{
			int score = 0;

			for (int i = 0; i < input.Length; i++)
			{
				string line = input[i];

				Stack<char> stack = new();

				for (int j = 0; j < line.Length; j++)
				{
					char c = line[j];

					if (c == '(' || c == '[' || c == '{' || c == '<')
					{
						stack.Push(c);
					}
					else
					{
						char b = stack.Pop();
						if ((b == '(' && c != ')') || (b == '[' && c != ']') || (b == '{' && c != '}') || (b == '<' && c != '>'))
						{
							score += c switch
							{
								')' => 3,
								']' => 57,
								'}' => 1197,
								'>' => 25137,
								_ => 0,
							};

							break;
						}
					}

					if (j == line.Length - 1)
					{
						// last char and did not break
						incomplete.Add(line);
					}
				}
			}

			return new(score.ToString()); // 294195
		}

		public override ValueTask<string> Solve_2()
		{
			ulong[] scores = new ulong[incomplete.Count];

			for (int i = 0; i < incomplete.Count; i++)
			{
				string line = incomplete[i];

				Stack<char> stack = new();

				for (int j = 0; j < line.Length; j++)
				{
					char c = line[j];

					if (c == '(' || c == '[' || c == '{' || c == '<')
					{
						stack.Push(c);
					}
					else
					{
						stack.Pop();
					}
				}

				int count = stack.Count;

				// Only the open ones are left
				for (int j = 0; j < count; j++)
				{
					char c = stack.Pop();			

					scores[i] *= 5;

					scores[i] += c switch
					{
						'(' => 1,
						'[' => 2,
						'{' => 3,
						'<' => 4,
						_ => 0,
					};
				}

			}

			Array.Sort(scores);
			ulong score = scores[scores.Length / 2];

			return new(score.ToString()); // 3490802734
		}
	}
}
