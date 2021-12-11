using AoCHelper;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_11 : BaseDay
	{
		private const int _X = 10;
		private const int _Y = 10;
		private readonly int[,] _map;

		public Day_11()
		{
			string[] input = File.ReadAllLines(InputFilePath);

			_map = new int[_X, _Y];

			for (int i = 0; i < input.Length; i++)
			{
				string line = input[i];
				for (int j = 0; j < line.Length; j++)
				{
					_map[j, i] = (int)char.GetNumericValue(line[j]);
				}
			}
		}

		public override ValueTask<string> Solve_1()
		{
			int[,] map = (int[,])_map.Clone();
			int flashes = 0;

			for (int step = 0; step < 100; step++)
			{
				for (int y = 0; y < _Y; y++)
				{
					for (int x = 0; x < _X; x++)
					{
						map[x, y]++;
					}
				}

				bool hasFlashed = true;
				while (hasFlashed)
				{
					hasFlashed = false;
					for (int y = 0; y < _Y; y++)
					{
						for (int x = 0; x < _X; x++)
						{
							if (map[x, y] > 9)
							{
								map[x, y] = 0;
								flashes++;
								hasFlashed = true;

								// top
								if (y - 1 >= 0 && map[x, y - 1] != 0)
								{
									map[x, y - 1]++;
								}
								// top right
								if (y - 1 >= 0 && x + 1 < _X && map[x + 1, y - 1] != 0)
								{
									map[x + 1, y - 1]++;
								}
								// right
								if (x + 1 < _X && map[x + 1, y] != 0)
								{
									map[x + 1, y]++;
								}
								// bottom right
								if (y + 1 < _Y && x + 1 < _X && map[x + 1, y + 1] != 0)
								{
									map[x + 1, y + 1]++;
								}
								// bottom
								if (y + 1 < _Y && map[x, y + 1] != 0)
								{
									map[x, y + 1]++;
								}
								// bottom left
								if (y + 1 < _Y && x - 1 >= 0 && map[x - 1, y + 1] != 0)
								{
									map[x - 1, y + 1]++;
								}
								// left
								if (x - 1 >= 0 && map[x - 1, y] != 0)
								{
									map[x - 1, y]++;
								}
								// top left
								if (y - 1 >= 0 && x - 1 >= 0 && map[x - 1, y - 1] != 0)
								{
									map[x - 1, y - 1]++;
								}
							}
						}
					}
				}

			}

			return new(flashes.ToString()); // 1647
		}

		public override ValueTask<string> Solve_2()
		{
			int[,] map = (int[,])_map.Clone();
			int step;

			for (step = 1; step < int.MaxValue; step++)
			{
				int flashes = 0;

				for (int y = 0; y < _Y; y++)
				{
					for (int x = 0; x < _X; x++)
					{
						map[x, y]++;
					}
				}

				bool hasFlashed = true;
				while (hasFlashed)
				{
					hasFlashed = false;
					for (int y = 0; y < _Y; y++)
					{
						for (int x = 0; x < _X; x++)
						{
							if (map[x, y] > 9)
							{
								map[x, y] = 0;
								flashes++;
								hasFlashed = true;

								// top
								if (y - 1 >= 0 && map[x, y - 1] != 0)
								{
									map[x, y - 1]++;
								}
								// top right
								if (y - 1 >= 0 && x + 1 < _X && map[x + 1, y - 1] != 0)
								{
									map[x + 1, y - 1]++;
								}
								// right
								if (x + 1 < _X && map[x + 1, y] != 0)
								{
									map[x + 1, y]++;
								}
								// bottom right
								if (y + 1 < _Y && x + 1 < _X && map[x + 1, y + 1] != 0)
								{
									map[x + 1, y + 1]++;
								}
								// bottom
								if (y + 1 < _Y && map[x, y + 1] != 0)
								{
									map[x, y + 1]++;
								}
								// bottom left
								if (y + 1 < _Y && x - 1 >= 0 && map[x - 1, y + 1] != 0)
								{
									map[x - 1, y + 1]++;
								}
								// left
								if (x - 1 >= 0 && map[x - 1, y] != 0)
								{
									map[x - 1, y]++;
								}
								// top left
								if (y - 1 >= 0 && x - 1 >= 0 && map[x - 1, y - 1] != 0)
								{
									map[x - 1, y - 1]++;
								}
							}
						}
					}
				}

				if (flashes == _X * _Y) break;
			}

			return new(step.ToString()); // 348
		}

	}
}
