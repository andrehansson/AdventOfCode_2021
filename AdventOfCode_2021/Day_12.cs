using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_12 : BaseDay
	{
		private readonly Dictionary<string, List<string>> nodes;
		private int paths;

		public Day_12()
		{
			string[] input = File.ReadAllLines(InputFilePath);
			nodes = new();

			foreach (string line in input)
			{
				string[] parts = line.Split('-');

				if (nodes.ContainsKey(parts[0]))
				{
					nodes[parts[0]].Add(parts[1]);
				}
				else
				{
					nodes.Add(parts[0], new());
					nodes[parts[0]].Add(parts[1]);
				}

				if (nodes.ContainsKey(parts[1]))
				{
					nodes[parts[1]].Add(parts[0]);
				}
				else
				{
					nodes.Add(parts[1], new());
					nodes[parts[1]].Add(parts[0]);
				}
			}
		}

		public override ValueTask<string> Solve_1()
		{
			paths = 0;
			string node = "start";
			List<string> path = new();
			DFS(node, path);

			return new(paths.ToString()); // 4775
		}

		private void DFS(string node, List<string> path)
		{
			path.Add(node);

			foreach (string n in nodes[node])
			{
				if (n == "end")
				{
					// end reached, path complete
					paths++;
					continue;
				}
				else if (char.IsLower(n[0]) && path.Contains(n))
				{
					// small cave, already visited, not allowed
					continue;
				}
				else
				{
					DFS(n, new List<string>(path));
				}
			}
		}


		public override ValueTask<string> Solve_2()
		{
			paths = 0;
			string node = "start";
			List<string> path = new();
			DFS2(node, path, false);

			return new(paths.ToString()); // 152480
		}

		private void DFS2(string node, List<string> path, bool haveVisitedTwice)
		{
			path.Add(node);

			foreach (string n in nodes[node])
			{
				if (n == "end")
				{
					// end reached, path complete
					paths++;
					continue;
				}
				else if (char.IsLower(n[0]) && path.Contains(n))
				{
					// small cave, already visited, not allowed

					// Can visit 1 small cave twice, excluding start and end.
					if (!haveVisitedTwice && n != "start" && !VisitedSmallCaveTwice(path))
					{
						DFS2(n, new List<string>(path), true);
					}

					continue;
				}
				else
				{
					DFS2(n, new List<string>(path), haveVisitedTwice);
				}
			}
		}

		private static bool VisitedSmallCaveTwice(List<string> path)
		{	
			// check if any small cave has been visited twice
			for (int i = 0; i < path.Count; i++)
			{
				if (char.IsLower(path[i][0]))
				{
					if (i + 1 < path.Count)
					{
						if (path.IndexOf(path[i], i + 1) != -1)
						{
							// found small cave that has been visited twice
							return true;
						}
					}
				}
			}

			return false;
		}


		private static string ListToString(List<string> list)
		{
			StringBuilder sb = new();
			for (int i = 0; i < list.Count; i++)
			{
				if (i == list.Count - 1)
				{
					sb.Append($"{list[i]}");
				}
				else
				{
					sb.Append($"{list[i]}, ");
				}
			}
			return sb.ToString();
		}

	}
}
