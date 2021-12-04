using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;
using System.IO;

namespace AdventOfCode_2021
{
	class Day_04 : BaseDay
	{
		private int[] _drawOrder;
		private readonly List<int[][]> _boards;

		public Day_04()
		{
			_boards = new();
			ParseInput();
		}

		private void ParseInput()
		{
			string[] input = File.ReadAllLines(InputFilePath);

			_drawOrder = input[0].Split(',').Select(int.Parse).ToArray();

			int[][] boardLines = new int[10][];
			int boardRow = 0;

			for (int i = 1; i < input.Length; i++)
			{
				if (string.IsNullOrWhiteSpace(input[i]))
				{
					if (boardLines[4] != null)
					{

						int column = 0;
						for (int j = 5; j < boardLines.Length; j++)
						{
							boardLines[j] = new int[5];

							for (int k = 0; k < 5; k++)
							{
								boardLines[j][k] = boardLines[k][column];
							}

							column++;
						}

						_boards.Add(boardLines);
					}

					boardLines = new int[10][];
					boardRow = 0;
				}
				else
				{
					boardLines[boardRow++] = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				}

			}
		}

		public override ValueTask<string> Solve_1()
		{
			int winningDraw = 0;
			int[][] winner = null;

			for (int d = 0; d < _drawOrder.Length; d++)
			{
				if (d < 4) continue;

				int correct;

				int[][] FindWinner()
				{
					foreach (int[][] board in _boards)
					{
						for (int i = 0; i < board.Length; i++)
						{
							correct = 0;

							for (int j = 0; j < board[i].Length; j++)
							{
								if (board[i][j] == d || board[i][j] == 0)
								{
									board[i][j] = 0;
									correct++;
								}
								if (correct == 5)
								{
									winningDraw = d;
									return board;
								}
							}
						}
					}
					return null;
				}
				winner = FindWinner();

				if (winner != null)
				{
					break;
				}
			}

			int sum = 0;

			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					sum += winner[i][j];
				}
			}

			int result = sum * winningDraw;

			return new(result.ToString());
		}

		public override ValueTask<string> Solve_2()
		{
			List<int[][]> winners = new();
			List<int> winnersDrawn = new();

			for (int d = 0; d < _drawOrder.Length; d++)
			{
				if (winners.Count == _boards.Count)
				{
					break;
				}

				if (d < 4) continue;

				int correct;

				foreach (int[][] board in _boards)
				{
					if (winners.Contains(board)) continue;

					void CheckBoard()
					{
						for (int i = 0; i < board.Length; i++)
						{
							correct = 0;

							for (int j = 0; j < board[i].Length; j++)
							{
								if (board[i][j] == d || board[i][j] == 0)
								{
									board[i][j] = 0;
									correct++;
								}
								if (correct == 5)
								{
									winners.Add(board);
									winnersDrawn.Add(d);
									return;
								}
							}
						}
					}
					CheckBoard();
				}
			}

			int[][] winner = winners.Last();

			int sum = 0;

			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					sum += winner[i][j];
				}
			}

			int result = sum * winnersDrawn.Last();

			return new(result.ToString());
		}

	}
}
