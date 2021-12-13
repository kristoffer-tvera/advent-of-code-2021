using System.Text;

namespace AdventOfCode
{
    public class Day13 : Day, IDay
    {
        public Day13(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/13/input");
            //var input = "6,10\n0,14\n9,10\n0,3\n10,4\n4,11\n6,0\n6,12\n4,1\n0,13\n10,12\n3,4\n3,0\n8,4\n1,10\n2,14\n8,10\n9,0\n\nfold along y=7\nfold along x=5";
            var dots = input.Split("\n\n")[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var folds = input.Split("\n\n")[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var part1Score = 0;
            var part2Score = "";


            var initialSizeX = dots.Max(dot => int.Parse(dot.Split(',', StringSplitOptions.RemoveEmptyEntries)[0])) + 1;
            var initialSizeY = dots.Max(dot => int.Parse(dot.Split(',', StringSplitOptions.RemoveEmptyEntries)[1])) + 1;
            bool[,] paper = new bool [initialSizeY, initialSizeX];

            foreach(var dot in dots)
            {
                var coordinates = GetCoordinatesFromDot(dot);
                paper[coordinates.y, coordinates.x] = true;
            }

            foreach(var fold in folds)
            {
                var axis = fold.Split('=')[0];
                var line = int.Parse(fold.Split('=')[1]);
                paper = FoldPaper(paper, axis[axis.Length - 1], line);

                if(part1Score == 0)
                {
                    part1Score = CountDots(paper);
                }
            }

            Print(paper);
            var sb = new StringBuilder("\n");
            for (int y = 0; y < paper.GetLength(0); y++)
            {
                for (int x = 0; x < paper.GetLength(1); x++)
                {
                    sb.Append(paper[y, x] ? '#' : '.');
                }
                sb.Append('\n');
            }
            part2Score = sb.ToString();

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private int CountDots(bool[,] paper)
        {
            var score = 0;
            for (int y = 0; y < paper.GetLength(0); y++)
            {
                for (int x = 0; x < paper.GetLength(1); x++)
                {
                    if (paper[y, x])
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        private bool[,] FoldPaper(bool[,] paper, char axis, int line)
        {
            if(axis == 'y')
            {
                var newPaper = new bool[line, paper.GetLength(1)];

                for (int y = 0; y < paper.GetLength(0); y++)
                {
                    for (int x = 0; x < paper.GetLength(1); x++)
                    {
                        var index = y >= line ? 2 * line - y : y;

                        if(paper[y, x])
                        {
                            newPaper[index, x] = true;
                        }
                    }
                }

                return newPaper;
            }
            else
            {
                var newPaper = new bool[paper.GetLength(0), line];

                for (int y = 0; y < paper.GetLength(0); y++)
                {
                    for (int x = 0; x < paper.GetLength(1); x++)
                    {
                        var index = x >= line ? 2 * line - x : x;

                        if (paper[y, x])
                        {
                            newPaper[y, index] = true;
                        }
                    }
                }

                return newPaper;
            }
        }

        private (int x, int y) GetCoordinatesFromDot(string dot)
        {
            var coordinates = dot.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return (int.Parse(coordinates[0]), int.Parse(coordinates[1]));
        }

        private void Print(bool[,] paper)
        {
            Console.WriteLine();
            for (int y = 0; y < paper.GetLength(0); y++)
            {
                for (int x = 0; x < paper.GetLength(1); x++)
                {
                    Console.Write(paper[y, x] ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        private record Fold(char Axis, int Line);
    }
}
