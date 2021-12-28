using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_17 : BaseDay
	{
		private readonly int targetLeft;
		private readonly int targetRight;
		private readonly int targetBottom;
		private readonly int targetTop;

		public Day_17()
		{
			string input = File.ReadAllText(InputFilePath);

			Regex regex = new(@"-?\d+");
			MatchCollection matches = regex.Matches(input);
			targetLeft = int.Parse(matches[0].Value);
			targetRight = int.Parse(matches[1].Value);
			targetBottom = int.Parse(matches[2].Value);
			targetTop = int.Parse(matches[3].Value);
		}

		public override ValueTask<string> Solve_1()
		{
			return new(Solve().Max().ToString()); // 5886
		}

		public override ValueTask<string> Solve_2()
		{
			return new(Solve().Count.ToString()); // 1806
		}

		private List<int> Solve()
		{
			List<int> highestYs = new();

			// determine initial x,y velocity to hit target
			for (int initialYVelocity = targetBottom; initialYVelocity < -targetBottom; initialYVelocity++)
			{
				for (int initialXVelocity = 1; initialXVelocity <= targetRight; initialXVelocity++)
				{
					// starts at 0,0
					int x = 0;
					int y = 0;

					int dX = initialXVelocity;
					int dY = initialYVelocity;

					int highestY = int.MinValue;

					while (x <= targetRight && y >= targetBottom && (dX == 0 && x >= targetLeft || dX != 0))
					{
						// The probe's x position increases by its x velocity.
						x += dX;

						// The probe's y position increases by its y velocity.
						y += dY;

						highestY = Math.Max(highestY, y);

						// Due to drag, the probe's x velocity changes by 1 toward the value 0.
						if (dX > 0) dX--;

						// Due to gravity, the probe's y velocity decreases by 1.
						dY--;

						if (x >= targetLeft && x <= targetRight && y >= targetBottom && y <= targetTop)
						{
							// Hit target
							highestYs.Add(highestY);
							break;
						}
					}
				}
			}

			return highestYs;
		}
	}
}
