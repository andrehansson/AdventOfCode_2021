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
	class Day_05 : BaseDay
	{
		struct Line
		{
			public Line(int[] p)
			{
				StartX = p[0];
				StartY = p[1];
				EndX = p[2];
				EndY = p[3];
			}
			public readonly int StartX { get; }
			public readonly int StartY { get; }
			public readonly int EndX { get; }
			public readonly int EndY { get; }

			public override string ToString()
			{
				return $"{StartX},{StartY} -> {EndX},{EndY}";
			}
		}

		private readonly List<Line> lines;
		private int[,] map;
		private int maxX;
		private int maxY;

		public Day_05()
		{
			lines = new();
			ParseInput();
		}

		private void ParseInput()
		{
			string[] input = File.ReadAllLines(InputFilePath);
			//string[] input = { "0,9 -> 5,9", "8,0 -> 0,8", "9,4 -> 3,4", "2,2 -> 2,1", "7,0 -> 7,4", "6,4 -> 2,0", "0,9 -> 2,9", "3,4 -> 1,4", "0,0 -> 8,8", "5,5 -> 8,2" };

			string[] split = { ",", " -> " };

			maxX = 0;
			maxY = 0;

			foreach (string str in input)
			{
				Line l = new(str.Split(split, StringSplitOptions.None).Select(int.Parse).ToArray());
				lines.Add(l);

				maxX = Math.Max(maxX, Math.Max(l.StartX, l.EndX));
				maxY = Math.Max(maxY, Math.Max(l.StartY, l.EndY));
			}

			map = new int[++maxX, ++maxY];

			// Set all to 0
			for (int i = 0; i < maxX * maxY; i++) map[i % maxX, i / maxX] = 0;
		}

		public override ValueTask<string> Solve_1()
		{
			foreach (Line line in lines)
			{
				// For now, only consider horizontal and vertical lines: lines where either x1 = x2 or y1 = y2.
				if (line.StartX == line.EndX)
				{
					if (line.StartY > line.EndY)
					{
						for (int y = line.StartY; y >= line.EndY; y--) map[line.StartX, y] += 1;
					}
					else
					{
						for (int y = line.StartY; y <= line.EndY; y++) map[line.StartX, y] += 1;
					}
				}
				else if (line.StartY == line.EndY)
				{
					if (line.StartX > line.EndX)
					{
						for (int x = line.StartX; x >= line.EndX; x--) map[x, line.StartY] += 1;
					}
					else
					{
						for (int x = line.StartX; x <= line.EndX; x++) map[x, line.StartY] += 1;
					}
				}
			}

			int result = 0;

			for (int i = 0; i < maxX * maxY; i++)
			{
				if (map[i % maxX, i / maxX] > 1) result++;
			}

			return new(result.ToString());
		}

		public override ValueTask<string> Solve_2()
		{
			foreach (Line line in lines)
			{
				// Horizontal and vertical is done. Find diagonals.
				if (Math.Abs(line.StartX - line.EndX) == Math.Abs(line.StartY - line.EndY))
				{
					if (line.StartX > line.EndX)
					{
						if (line.StartY > line.EndY)
						{
							int y = line.StartY;
							for (int x = line.StartX; x >= line.EndX; x--) map[x, y--] += 1;
						}
						else
						{
							int y = line.StartY;
							for (int x = line.StartX; x >= line.EndX; x--) map[x, y++] += 1;
						}
					}
					else
					{

						if (line.StartY > line.EndY)
						{
							int y = line.StartY;
							for (int x = line.StartX; x <= line.EndX; x++) map[x, y--] += 1;
						}
						else
						{
							int y = line.StartY;
							for (int x = line.StartX; x <= line.EndX; x++) map[x, y++] += 1;
						}
					}
				}
			}

			int result = 0;

			for (int i = 0; i < maxX * maxY; i++)
			{
				if (map[i % maxX, i / maxX] > 1) result++;
			}

			return new(result.ToString());
		}

		private void PrintMap()
		{
			StringBuilder sb = new();

			for (int y = 0; y < maxY; y++)
			{
				for (int x = 0; x < maxX; x++)
				{
					sb.Append($"{map[x, y]} ");
				}
				sb.Append(Environment.NewLine);
			}

			Debug.WriteLine(sb.ToString());
		}
	}
}
