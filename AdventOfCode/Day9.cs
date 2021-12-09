namespace AdventOfCode
{
    public class Day9 : Day, IDay
    {
        public Day9(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/9/input");
            var mapWidth = input.IndexOf('\n');
            var mapHeight = input.Count(c => c == '\n');
            var map = new int[mapHeight, mapWidth];

            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var digit = lines[i][j];
                    map[i,j] = int.Parse(digit.ToString());
                }
            }

            var part1Score = 0;
            var lowPoints = new List<(int x, int y)>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var currentDigit = map[i, j];
                    if(IsSmallestAdjacent(map, i, j))
                    {
                        part1Score += (1 + map[i, j]);
                        lowPoints.Add((i, j));
                    }
                }
            }

            var visitedPoints = new(int height, bool visited)[mapHeight, mapWidth];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    visitedPoints[i, j] = (map[i, j], false);
                }
            }

            var basinSizes = new List<int>();
            foreach(var lowpoint in lowPoints)
            {
                var basinSize = BasinSize(visitedPoints, lowpoint.x, lowpoint.y);
                basinSizes.Add(basinSize);
            }

            var topThreeBasins = basinSizes.OrderByDescending(basinSize => basinSize).Take(3);

            var part2Score = topThreeBasins.Aggregate((num, sum) => num * sum);

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private bool IsSmallestAdjacent(int[,] map, int x, int y)
        {
            var digit = map[x, y];

            if(x - 1 >= 0)
            {
                if (digit >= map[x - 1, y]) return false;
            }

            if (x + 1 < map.GetLength(0))
            {
                if (digit >= map[x + 1, y]) return false;
            }

            if (y - 1 >= 0)
            {
                if (digit >= map[x, y - 1]) return false;
            }

            if (y + 1 < map.GetLength(1))
            {
                if (digit >= map[x, y + 1]) return false;
            }

            return true;
        }

        private int BasinSize((int height, bool visited)[,] map, int x, int y)
        {
            if (map[x, y].visited) return 0;
            if (map[x, y].height == 9) return 0;
            map[x, y].visited = true;

            var sum = 1;
            if (x - 1 >= 0)
            {
                sum += BasinSize(map, x - 1, y);
            }

            if (x + 1 < map.GetLength(0))
            {
                sum += BasinSize(map, x + 1, y);
            }

            if (y - 1 >= 0)
            {
                sum += BasinSize(map, x, y - 1);
            }

            if (y + 1 < map.GetLength(1))
            {
                sum += BasinSize(map, x, y + 1);
            }

            return sum;
        }

        private void PrintMap(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {

                    if (IsSmallestAdjacent(map, i, j))
                    {
                        var oldColor = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(map[i, j]);
                        Console.BackgroundColor = oldColor;
                    } else
                    {
                        Console.Write(map[i, j]);
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
