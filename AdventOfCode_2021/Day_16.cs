using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
	class Day_16 : BaseDay
	{
		private readonly string _inputBinary;
		private int _index;

		public Day_16()
		{
			string _inputHex = File.ReadAllText(InputFilePath);

			StringBuilder sb = new();
			foreach (char c in _inputHex)
			{
				sb.Append(HexadecimalToBinary(c));
			}
			_inputBinary = sb.ToString();
		}

		public override ValueTask<string> Solve_1()
		{
			_index = 0;
			int versionSum = DecodePacket().versionSum;
			return new(versionSum.ToString()); // 943
		}

		public override ValueTask<string> Solve_2()
		{
			_index = 0;
			long value = DecodePacket().value;
			return new(value.ToString()); // 167737115857
		}

		private (int length, long value, int versionSum) DecodePacket()
		{
			int decodedLength = 0;

			string Next(int n)
			{
				decodedLength += n;

				string result = _inputBinary.Substring(_index, n);
				_index += n;
				return result;
			}

			long value = 0;
			int versionSum = 0;

			int version = Convert.ToInt32(Next(3), 2);
			int typeId = Convert.ToInt32(Next(3), 2);

			versionSum += version;

			if (typeId == 4)
			{
				// literal
				StringBuilder literalSB = new();
				bool last = false;
				while (!last)
				{
					if (Next(1)[0] == '0') last = true;
					literalSB.Append(Next(4));
				}

				value = Convert.ToInt64(literalSB.ToString(), 2);
			}
			else
			{
				// operator
				char lengthTypeId = Next(1)[0];

				List<long> values = new();
				int decoded = 0;

				void SubPacket()
				{
					(int l, long v, int sum) = DecodePacket();
					decoded += l;
					versionSum += sum;
					values.Add(v);
				}

				if (lengthTypeId == '0')
				{
					// If the length type ID is 0, then the next 15 bits are a number that represents the total length in bits of the sub-packets contained by this packet.
					int totalLength = Convert.ToInt32(Next(15), 2);

					while (decoded != totalLength)
					{
						SubPacket();
					}
					decodedLength += decoded;
				}
				else
				{
					// If the length type ID is 1, then the next 11 bits are a number that represents the number of sub-packets immediately contained by this packet.
					int subPackets = Convert.ToInt32(Next(11), 2);

					for (int i = 0; i < subPackets; i++)
					{
						SubPacket();
					}
					decodedLength += decoded;
				}

				switch (typeId)
				{
					case 0:
						foreach (long v in values)
						{
							value += v;
						}
						break;
					case 1:
						value = values[0];
						for (int i = 1; i < values.Count; i++)
						{
							value *= values[i];
						}
						break;
					case 2:
						value = long.MaxValue;
						foreach (long v in values)
						{
							value = Math.Min(value, v);
						}
						break;
					case 3:
						value = long.MinValue;
						foreach (long v in values)
						{
							value = Math.Max(value, v);
						}
						break;
					case 5:
						if (values[0] > values[1]) value = 1;
						break;
					case 6:
						if (values[0] < values[1]) value = 1;
						break;
					case 7:
						if (values[0] == values[1]) value = 1;
						break;
					default:
						break;
				}
			}

			return (decodedLength, value, versionSum);
		}

		private static string HexadecimalToBinary(char hex)
		{
			return hex switch
			{
				'0' => "0000",
				'1' => "0001",
				'2' => "0010",
				'3' => "0011",
				'4' => "0100",
				'5' => "0101",
				'6' => "0110",
				'7' => "0111",
				'8' => "1000",
				'9' => "1001",
				'A' => "1010",
				'B' => "1011",
				'C' => "1100",
				'D' => "1101",
				'E' => "1110",
				'F' => "1111",
				_ => "",
			};
		}

	}
}
