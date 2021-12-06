using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode_2021
{
	class Day_06 : BaseDay
	{
		private readonly int[] input;

		public Day_06()
		{
			//string str = "3,4,3,1,2";
			string str = File.ReadAllText(InputFilePath);
			input = str.Split(',').Select(int.Parse).ToArray();
		}

		public override ValueTask<string> Solve_1()
		{
			return new(FishAfterDays(80).ToString());
		}

		public override ValueTask<string> Solve_2()
		{
			return new(FishAfterDays(256).ToString());
		}

		private ulong FishAfterDays(int days)
		{
			ulong[] f = new ulong[9];

			foreach (int i in input)
			{
				f[i]++;
			}

			for (int day = 1; day <= days; day++)
			{
				ulong fishToAdd = f[0];
				f[0] = f[1];
				f[1] = f[2];
				f[2] = f[3];
				f[3] = f[4];
				f[4] = f[5];
				f[5] = f[6];
				f[6] = f[7] + fishToAdd;
				f[7] = f[8];
				f[8] = fishToAdd;
			}

			ulong sum = 0;
			foreach (ulong i in f)
			{
				sum += i;
			}

			return sum;
		}
	}
}
