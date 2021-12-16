using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_14 : BaseDay
	{
		private readonly string _template;
		private readonly Dictionary<string, string> _rules;

		public Day_14()
		{
			string[] input = File.ReadAllLines(InputFilePath);

			_template = input[0];

			_rules = new();

			for (int i = 2; i < input.Length; i++)
			{
				string[] parts = input[i].Split(" -> ");
				_rules.Add(parts[0], parts[1]);
			}
		}

		public override ValueTask<string> Solve_1()
		{
			return new(PairInsertion(10).ToString()); // 4244
		}

		public override ValueTask<string> Solve_2()
		{
			return new(PairInsertion(40).ToString()); // 4807056953866
		}

		private ulong PairInsertion(int steps)
		{
			Dictionary<string, ulong> pairs = new();
			Dictionary<char, ulong> charCount = new();

			for (int i = 0; i < _template.Length; i++)
			{
				charCount.TryGetValue(_template[i], out ulong currentCount);
				charCount[_template[i]] = currentCount + 1;

				if (i + 1 < _template.Length)
				{
					string pair = _template.Substring(i, 2);
					pairs.TryGetValue(pair, out ulong currentCount2);
					pairs[pair] = currentCount2 + 1;
				}
			}

			for (int step = 0; step < steps; step++)
			{
				foreach (KeyValuePair<string, ulong> pair in pairs.ToList())
				{
					if (pair.Value > 0)
					{
						pairs.TryGetValue(pair.Key[0] + _rules[pair.Key], out ulong currentCount);
						pairs[pair.Key[0] + _rules[pair.Key]] = currentCount + pair.Value;

						pairs.TryGetValue(_rules[pair.Key] + pair.Key[1], out ulong currentCount2);
						pairs[_rules[pair.Key] + pair.Key[1]] = currentCount2 + pair.Value;

						pairs[pair.Key] -= pair.Value;

						charCount.TryGetValue(_rules[pair.Key][0], out ulong currentCount3);
						charCount[_rules[pair.Key][0]] = currentCount3 + pair.Value;
					}
				}
			}

			ulong max = 0;
			ulong min = ulong.MaxValue;

			foreach (ulong i in charCount.Values)
			{
				max = Math.Max(i, max);
				min = Math.Min(i, min);
			}

			return max - min;
		}

	}
}
