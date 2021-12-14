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
	class Day_13 : BaseDay
	{
		private readonly int _X;
		private readonly int _Y;
		private readonly bool[,] _map;
		private readonly List<string> _instructions;

		public Day_13()
		{
			string[] input = File.ReadAllLines(InputFilePath);
			//string[] input = {"6,10", "0,14", "9,10", "0,3", "10,4", "4,11", "6,0", "6,12", "4,1", "0,13", "10,12", "3,4", "3,0", "8,4", "1,10", "2,14", "8,10", "9,0", "\n", "fold along y=7", "fold along x=5" };

			_instructions = new();

			List<int> xList = new();
			List<int> yList = new();

			foreach (string line in input)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					if (line[0] == 'f')
					{
						_instructions.Add(line.Substring(11));
					}
					else
					{
						int[] c = line.Split(',').Select(int.Parse).ToArray();
						xList.Add(c[0]);
						yList.Add(c[1]);
						_X = Math.Max(_X, c[0]);
						_Y = Math.Max(_Y, c[1]);
					}
				}
			}

			_map = new bool[++_X, ++_Y];

			for (int i = 0; i < xList.Count; i++)
			{
				_map[xList[i], yList[i]] = true;
			}
		}

		public override ValueTask<string> Solve_1()
		{
			bool[,] map = (bool[,])_map.Clone();

			Fold(ref map, true);

			int dots = 0;
			for (int y = 0; y < map.GetLength(1); y++)
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					if (map[x, y]) dots++;
				}
			}

			return new(dots.ToString()); // 671
		}

		public override ValueTask<string> Solve_2()
		{
			bool[,] map = (bool[,])_map.Clone();

			Fold(ref map, false);

			StringBuilder sb = new();

			for (int y = 0; y < map.GetLength(1); y++)
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					if (map[x, y])
					{
						sb.Append("# ");
					}
					else
					{
						sb.Append(". ");
					}
				}
				sb.AppendLine();
			}

			Debug.WriteLine("Day 13 Part 2 answer:");
			Debug.WriteLine(sb.ToString());
			// PCPHARKL
			return new("Check debug console");
		}

		private void Fold(ref bool[,] map, bool part1)
		{
			foreach (string instruction in _instructions)
			{
				int value = int.Parse(instruction.Substring(2));
				int xLenght = map.GetLength(0);
				int yLength = map.GetLength(1);

				if (instruction.StartsWith('x'))
				{
					int width = Math.Max(value - 1, xLenght - value - 1);
					bool[,] folded = new bool[width, yLength];

					int leftX = value - 1;
					int rightX = value + 1;

					for (int y = 0; y < yLength; y++)
					{
						for (int x = width - 1; x >= 0; x--)
						{
							bool dot = false;
							if (leftX >= 0)
							{
								dot = map[leftX, y] || dot;
							}
							if (rightX < xLenght)
							{
								dot = map[rightX, y] || dot;
							}
							folded[x, y] = dot;

							leftX--;
							rightX++;
						}
						leftX = value - 1;
						rightX = value + 1;
					}
					map = folded;
				}
				else // y
				{
					int length = Math.Max(value - 1, yLength - value - 1);
					bool[,] folded = new bool[xLenght, length];

					int topY = value - 1;
					int bottomY = value + 1;

					for (int y = length - 1; y >= 0; y--)
					{
						for (int x = 0; x < xLenght; x++)
						{
							bool dot = false;
							if (topY >= 0)
							{
								dot = map[x, topY] || dot;
							}
							if (bottomY < yLength)
							{
								dot = map[x, bottomY] || dot;
							}
							folded[x, y] = dot;
						}
						topY--;
						bottomY++;
					}
					map = folded;
				}

				if (part1) break; // First fold only
			}
		}

	}
}
