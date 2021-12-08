using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode_2021
{
	class Day_05 : BaseDay
	{
		private const int START_X = 0;
		private const int START_Y = 1;
		private const int END_X = 2;
		private const int END_Y = 3;

		private readonly int[][] lines;
		private readonly int[,] map;
		private readonly int maxX;
		private readonly int maxY;

		public Day_05()
		{
			string[] input = File.ReadAllLines(InputFilePath);
			//string[] input = { "0,9 -> 5,9", "8,0 -> 0,8", "9,4 -> 3,4", "2,2 -> 2,1", "7,0 -> 7,4", "6,4 -> 2,0", "0,9 -> 2,9", "3,4 -> 1,4", "0,0 -> 8,8", "5,5 -> 8,2" };

			lines = new int[input.Length][];

			string[] split = { ",", " -> " };

			for (int i = 0; i < input.Length; i++)
			{
				lines[i] = input[i].Split(split, StringSplitOptions.None).Select(int.Parse).ToArray();

				maxX = Math.Max(maxX, Math.Max(lines[i][START_X], lines[i][END_X]));
				maxY = Math.Max(maxY, Math.Max(lines[i][START_Y], lines[i][END_Y]));
			}

			map = new int[++maxX, ++maxY];
		}

		public override ValueTask<string> Solve_1()
		{
			for (int i = 0; i < lines.Length; i++)
			{
				int startX = lines[i][START_X];
				int startY = lines[i][START_Y];
				int endX = lines[i][END_X];
				int endY = lines[i][END_Y];

				// For now, only consider horizontal and vertical lines: lines where either x1 = x2 or y1 = y2.
				if (startX == endX)
				{
					if (startY > endY)
					{
						for (int y = startY; y >= endY; y--) map[startX, y]++;
					}
					else
					{
						for (int y = startY; y <= endY; y++) map[startX, y]++;
					}
				}
				else if (startY == endY)
				{
					if (startX > endX)
					{
						for (int x = startX; x >= endX; x--) map[x, startY]++;
					}
					else
					{
						for (int x = startX; x <= endX; x++) map[x, startY]++;
					}
				}
			}

			int result = 0;

			for (int y = 0; y < maxY; y++)
			{
				for (int x = 0; x < maxX; x++)
				{
					if (map[x, y] > 1) result++;
				}
			}

			return new(result.ToString()); // 7318
		}

		public override ValueTask<string> Solve_2()
		{
			for (int i = 0; i < lines.Length; i++)
			{
				int startX = lines[i][START_X];
				int startY = lines[i][START_Y];
				int endX = lines[i][END_X];
				int endY = lines[i][END_Y];

				// Horizontal and vertical is done. Find diagonals.
				if (Math.Abs(startX - endX) == Math.Abs(startY - endY))
				{
					if (startX > endX)
					{
						if (startY > endY)
						{
							int y = startY;
							for (int x = startX; x >= endX; x--) map[x, y--]++;
						}
						else
						{
							int y = startY;
							for (int x = startX; x >= endX; x--) map[x, y++]++;
						}
					}
					else
					{
						if (startY > endY)
						{
							int y = startY;
							for (int x = startX; x <= endX; x++) map[x, y--]++;
						}
						else
						{
							int y = startY;
							for (int x = startX; x <= endX; x++) map[x, y++]++;
						}
					}
				}
			}

			int result = 0;

			for (int y = 0; y < maxY; y++)
			{
				for (int x = 0; x < maxX; x++)
				{
					if (map[x, y] > 1) result++;
				}
			}

			return new(result.ToString()); // 19939
		}

	}
}
