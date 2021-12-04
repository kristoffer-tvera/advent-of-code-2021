namespace AdventOfCode
{
    public class Day4 : Day, IDay
    {
        public Day4(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/4/input");

            var blocks = input.Split("\n\n");
            var drawNumbers = blocks[0].Split(",").Select(num => int.Parse(num));

            var part1Score = "";
            var part2Score = "";

            var firstBoard = GetBingoBoard(blocks[1]);

            List<Board> boards = new()
            {
                new Board(firstBoard)
            };

            for (int i = 2; i < blocks.Length; i++)
            {
                var board = new Board(GetBingoBoard(blocks[i]));
                boards.Add(board);
            }

            foreach (var number in drawNumbers)
            {
                foreach(var board in boards)
                {
                    CheckNumberForBoard(board, number);
                    if (CheckForBingo(board))
                    {
                        var sumOfAllUnmarked = SumOfAllUnmarked(board);
                        part1Score = (sumOfAllUnmarked * number).ToString();
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(part1Score))
                {
                    break;
                }
            }

            foreach(var number in drawNumbers)
            {
                var remainingBoards = new List< Board>();


                foreach(var board in boards)
                {
                    CheckNumberForBoard(board, number);
                    if (!CheckForBingo(board)){
                        remainingBoards.Add(board);
                    } else
                    {
                        if(boards.Count == 1)
                        {
                            var sumOfAllUnmarked = SumOfAllUnmarked(board);
                            part2Score = (sumOfAllUnmarked * number).ToString();
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(part2Score))
                {
                    break;
                }

                boards.Clear();
                boards.AddRange(remainingBoards);
            }

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private BingoNumber[][] GetBingoBoard(string lines)
        {
            var board = new List<BingoNumber[]>();

            foreach (var line in lines.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var row = new List<BingoNumber>();

                for (int i = 0; i < numbers.Length; i++)
                {
                    var bingoNumber = new BingoNumber(int.Parse(numbers[i].Trim()));

                    row.Add(bingoNumber);
                }

                board.Add(row.ToArray());
            }

            return board.ToArray();
        }

        private int SumOfAllUnmarked(Board board)
        {
            var sum = 0;

            foreach(var row in board.Numbers)
            {
                foreach(var column in row)
                {
                    if (!column.Marked)
                    {
                        sum += column.Value;
                    }
                }
            }

            return sum;
        }

        private void CheckNumberForBoard(Board board, int number)
        {
            foreach(var row in board.Numbers)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i].Value == number)
                    {
                        row[i] = row[i] with { Marked = true };
                        break;
                    }
                }
            }
        }

        private bool CheckForBingo(Board board)
        {
            for (int i = 0; i < board.Numbers.Length; i++)
            {
                var rowBingo = true;
                foreach(var column in board.Numbers[i])
                {
                    if (!column.Marked)
                    {
                        rowBingo = false;
                        break;
                    }
                }
                if (rowBingo)
                {
                    return true;
                }

                var columnBingo = true;
                for (int j = 0; j < board.Numbers[i].Length; j++)
                {
                    if (!board.Numbers[j][i].Marked)
                    {
                        columnBingo = false;
                        break;
                    }
                }

                if (columnBingo)
                {
                    return true;
                }

            }

            return false;
        }
    }

    public record BingoNumber(int Value, bool Marked = false);
    public record Board(BingoNumber[][] Numbers);
}
