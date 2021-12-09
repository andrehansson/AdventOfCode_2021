using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode_2021
{
	class Day_09 : BaseDay
	{
		private readonly int[,] map;
		private readonly int maxX;
		private readonly int maxY;

		public Day_09()
		{
			// Read input and put a border of 9s around it
			string[] input = File.ReadAllLines(InputFilePath);

			maxX = input[0].Length + 2;
			maxY = input.Length + 2;

			map = new int[maxX, maxY];

			for (int y = 0; y < maxY; y++)
			{
				for (int x = 0; x < maxX; x++)
				{
					if (y == 0 || y == maxY - 1)
					{
						map[x, y] = 9;
					}
					else if (x == 0 || x == maxX - 1)
					{
						map[x, y] = 9;
					}
					else
					{
						map[x, y] = (int)char.GetNumericValue(input[y-1][x-1]);
					}				
				}
			}
		}

		public override ValueTask<string> Solve_1()
		{
			int sum = 0;

			// Because of border of 9s, start at 1 and go to < max - 1
			for (int y = 1; y < maxY - 1; y++)
			{
				for (int x = 1; x < maxX - 1; x++)
				{
					int point = map[x, y];
					int top = map[x, y - 1];
					int right = map[x + 1, y];
					int bottom = map[x, y + 1];
					int left = map[x - 1, y];

					if (point < top && point < right && point < bottom && point < left)
					{
						sum += (map[x, y] + 1);
					}
				}
			}

			return new(sum.ToString()); // 554
		}

		public override ValueTask<string> Solve_2()
		{
			List<int> basins = new();

			// Because of border of 9s, start at 1 and go to < max - 1
			for (int y = 1; y < maxY - 1; y++)
			{
				for (int x = 1; x < maxX - 1; x++)
				{
					int point = map[x, y];
					int top = map[x, y - 1];
					int right = map[x + 1, y];
					int bottom = map[x, y + 1];
					int left = map[x - 1, y];

					if (point < top && point < right && point < bottom && point < left)
					{
						int[,] visited = new int[maxX, maxY];
						basins.Add(CountBasin(x, y, visited));
					}
				}
			}

			int[] largest = basins.OrderByDescending(x => x).Take(3).ToArray();
			int result = largest[0] * largest[1] * largest[2];

			return new(result.ToString()); // 1017792
		}

		private int CountBasin(int x, int y, int[,] visited)
		{
			visited[x, y] = 1;
			int count = 1;

			int top = map[x, y - 1];
			int right = map[x + 1, y];
			int bottom = map[x, y + 1];
			int left = map[x - 1, y];

			if (top != 9 && visited[x, y - 1] != 1)
			{
				count += CountBasin(x, y - 1, visited);
			}

			if (right != 9 && visited[x + 1, y] != 1)
			{
				count += CountBasin(x + 1, y, visited);
			}

			if (bottom != 9 && visited[x, y + 1] != 1)
			{
				count += CountBasin(x, y + 1, visited);
			}

			if (left != 9 && visited[x - 1, y] != 1)
			{
				count += CountBasin(x - 1, y, visited);
			}

			return count;
		}

	}
}
