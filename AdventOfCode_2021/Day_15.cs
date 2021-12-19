using AoCHelper;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_15 : BaseDay
	{
		private readonly int[,] _map;
		private readonly int[,] _fullmap;

		public Day_15()
		{
			string[] lines = File.ReadAllLines(InputFilePath);

			int X = lines[0].Length;
			int Y = lines.Length;

			_map = new int[X, Y];
			_fullmap = new int[X * 5, Y * 5];

			for (int y = 0; y < Y; y++)
			{
				for (int x = 0; x < X; x++)
				{
					_map[x, y] = (int)char.GetNumericValue(lines[y][x]);
					_fullmap[x, y] = _map[x, y];
				}
			}

			for (int y = 0; y < Y * 5; y++)
			{
				for (int x = 0; x < X * 5; x++)
				{
					if (x - X >= 0)
					{
						int value = _fullmap[x - X, y] + 1;
						_fullmap[x, y] = value > 9 ? 1 : value;
					}
					else if (y - Y >= 0)
					{
						int value = _fullmap[x, y - Y] + 1;
						_fullmap[x, y] = value > 9 ? 1 : value;
					}

				}
			}
		}

		public override ValueTask<string> Solve_1()
		{
			return new(Dijkstra(_map).ToString()); // 656
		}

		public override ValueTask<string> Solve_2()
		{
			return new(Dijkstra(_fullmap).ToString()); // 2979
		}

		private static int Dijkstra(int[,] map)
		{
			int X = map.GetLength(0);
			int Y = map.GetLength(1);

			// Use list of Points because SortedList can't have duplicate keys
			SortedList<int, List<Point>> priorityQueue = new();

			void add(int risk, Point point)
			{
				if (!priorityQueue.TryGetValue(risk, out List<Point> points))
				{
					points = new List<Point>();
					priorityQueue[risk] = points;
				}
				points.Add(point);
			}

			void remove(int risk, Point point)
			{
				priorityQueue[risk].Remove(point);
				if (priorityQueue[risk].Count == 0) priorityQueue.Remove(risk);
			}

			int[,] distTo = new int[X, Y];

			for (int y = 0; y < Y; y++)
			{
				for (int x = 0; x < X; x++)
				{
					distTo[x, y] = int.MaxValue;
				}
			}
			distTo[0, 0] = 0;

			add(0, new Point(0, 0));

			while (priorityQueue.Count != 0)
			{
				KeyValuePair<int, List<Point>> riskPoints = priorityQueue.First();

				foreach (Point point in riskPoints.Value.ToArray())
				{
					remove(riskPoints.Key, point);

					int x = point.X;
					int y = point.Y;

					if (x == X - 1 && y == Y - 1)
					{
						break;
					}

					List<Point> adjacents = Adjacents(X, Y, x, y);

					foreach (Point adj in adjacents)
					{
						if (distTo[adj.X, adj.Y] > distTo[x, y] + map[adj.X, adj.Y])
						{
							distTo[adj.X, adj.Y] = distTo[x, y] + map[adj.X, adj.Y];

							add(distTo[adj.X, adj.Y], adj);
						}
					}
				}
			}

			return distTo[X - 1, Y - 1];
		}

		private static List<Point> Adjacents(int X, int Y, int x, int y)
		{
			List<Point> adjacents = new();

			// Top
			if (y - 1 >= 0)
			{
				adjacents.Add(new Point(x, y - 1));
			}

			// Right
			if (x + 1 < X)
			{
				adjacents.Add(new Point(x + 1, y));
			}

			// Bottom
			if (y + 1 < Y)
			{
				adjacents.Add(new Point(x, y + 1));
			}

			// Left
			if (x - 1 >= 0)
			{
				adjacents.Add(new Point(x - 1, y));
			}

			return adjacents;
		}

	}
}
