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
	class Day_18 : BaseDay
	{
		private interface IToken
		{
			public Type Type { get; }
			public string ToString();
		}

		private struct Token<T> : IToken
		{
			public Token(T v)
			{
				Value = v;
			}

			public T Value { get; set; }

			Type IToken.Type => typeof(T);

			string IToken.ToString()
			{
				return Value.ToString();
			}
		}

		private readonly List<IToken>[] _tokens;

		public Day_18()
		{
			string[] lines = File.ReadAllLines(InputFilePath);
			_tokens = new List<IToken>[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				List<IToken> list = new();
				_tokens[i] = list;

				foreach (char c in line)
				{
					if (char.IsDigit(c))
					{
						list.Add(new Token<int>((int)char.GetNumericValue(c)));
					}
					else
					{
						list.Add(new Token<char>(c));
					}
				}
			}
		}

		public override ValueTask<string> Solve_1()
		{
			List<IToken> sum = _tokens[0];

			for (int i = 1; i < _tokens.Length; i++)
			{
				Add(sum, _tokens[i]);
				Reduce(sum);
			}

			return new(Eval(sum).ToString()); // 4235
		}

		public override ValueTask<string> Solve_2()
		{
			List<IToken> sum;
			long largestMagnitude = 0;

			for (int i = 0; i < _tokens.Length; i++)
			{
				for (int j = 0; j < _tokens.Length; j++)
				{
					if (i == j) continue;

					sum = new(_tokens[i]);
					Add(sum, _tokens[j]);
					Reduce(sum);

					largestMagnitude = Math.Max(largestMagnitude, Eval(sum));
				}
			}

			return new(largestMagnitude.ToString()); // 4659
		}

		private static void Add(List<IToken> sum, List<IToken> term)
		{
			sum.Insert(0, new Token<char>('['));
			sum.Add(new Token<char>(','));
			sum.AddRange(term);
			sum.Add(new Token<char>(']'));
		}

		private static void Reduce(List<IToken> sum)
		{
			while (true)
			{
				if (Explode(sum)) continue;

				if (Split(sum)) continue;

				break;
			}
		}

		private static bool Explode(List<IToken> sum)
		{
			// If any pair is nested inside four pairs, the leftmost such pair explodes.
			int nested = 0;

			for (int i = 0; i < sum.Count; i++)
			{
				if (sum[i].Type == typeof(char))
				{
					char c = ((Token<char>)sum[i]).Value;

					if (c == '[') nested++;
					else if (c == ']') nested--;
				}

				if (nested == 5)
				{
					int x = ((Token<int>)sum[i + 1]).Value;
					int y = ((Token<int>)sum[i + 3]).Value;

					// Right number
					for (int j = i + 4; j < sum.Count; j++)
					{
						if (sum[j].Type == typeof(int))
						{
							int newRight = ((Token<int>)sum[j]).Value + y;
							sum[j] = new Token<int>(newRight);
							break;
						}
					}

					// Left number
					for (int j = i - 1; j >= 0; j--)
					{
						if (sum[j].Type == typeof(int))
						{
							int newLeft = ((Token<int>)sum[j]).Value + x;
							sum[j] = new Token<int>(newLeft);
							break;
						}
					}

					// replace pair with 0
					sum.RemoveRange(i, 5);
					sum.Insert(i, new Token<int>(0));

					return true;
				}
			}

			return false;
		}

		private static bool Split(List<IToken> sum)
		{
			// If any regular number is 10 or greater, the leftmost such regular number splits.
			for (int i = 0; i < sum.Count; i++)
			{
				if (sum[i].Type == typeof(int))
				{
					int value = ((Token<int>)sum[i]).Value;

					if (value >= 10)
					{
						int x = (int)Math.Floor((double)value / 2);
						int y = (int)Math.Ceiling((double)value / 2);

						sum.RemoveAt(i);
						IToken[] insert = { new Token<char>('['), new Token<int>(x), new Token<char>(','), new Token<int>(y), new Token<char>(']') };
						sum.InsertRange(i, insert);

						return true;
					}
				}
			}

			return false;
		}

		private static long Eval(List<IToken> list)
		{
			string expression = TokenListToString(list);
			expression = expression.Replace("[", "(3*").Replace(",", "+2*").Replace("]", ")");

			System.Data.DataTable table = new();
			return Convert.ToInt64(table.Compute(expression, String.Empty));
		}

		private static string TokenListToString(List<IToken> list)
		{
			StringBuilder sb = new();
			foreach (IToken token in list) sb.Append(token.ToString());
			return sb.ToString();
		}

	}
}
