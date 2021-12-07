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
	class Day_07 : BaseDay
	{
		private readonly int[] input;

		public Day_07()
		{
			//string str = "16,1,2,0,4,2,7,1,2,14";
			string str = File.ReadAllText(InputFilePath);
			input = str.Split(',').Select(int.Parse).ToArray();			
		}

		public override ValueTask<string> Solve_1()
		{
			Array.Sort(input);
			int median = input[input.Length / 2];

			int fuelCost = 0;

			foreach (int i in input)
			{
				fuelCost += Math.Abs(i - median);
			}

			return new(fuelCost.ToString());
		}

		public override ValueTask<string> Solve_2()
		{
			double average = input.Average();
			int averageCeiling = (int)Math.Ceiling(average);
			int averageFloor = (int)Math.Floor(average);

			int FuelCost(int value)
			{
				int fuelCost = 0;

				foreach (int i in input)
				{
					int distance = Math.Abs(i - value);
					fuelCost += distance * (distance + 1) / 2;
				}

				return fuelCost;
			}

			int fuelCost = Math.Min(FuelCost(averageCeiling), FuelCost(averageFloor));

			return new(fuelCost.ToString());
		}
	}
}
