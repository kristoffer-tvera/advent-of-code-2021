namespace AdventOfCode
{
    public class Day5 : Day, IDay
    {
        public Day5(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/5/input");
            var coordinateGroups = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var lines = new List<(Coordinate start, Coordinate end)>();
            var maxX = 0;
            var maxY = 0;
            foreach (var coordinateGroup in coordinateGroups)
            {
                var groups = coordinateGroup.Split(" -> ");

                var start = new Coordinate(int.Parse(groups[0].Split(',')[0]), int.Parse(groups[0].Split(',')[1]));
                var end = new Coordinate(int.Parse(groups[1].Split(',')[0]), int.Parse(groups[1].Split(',')[1]));

                if (start.X > maxX) maxX = start.X;
                if (end.X > maxX) maxX = end.X;

                if (start.Y > maxY) maxY = start.Y;
                if (end.Y > maxY) maxY = end.Y;

                lines.Add((start, end));
            }


            int[,] map = new int[maxX + 1, maxY + 1];

            foreach (var line in lines)
            {

                if (VerticalOrHorizontalLine(line.start, line.end))
                {
                    foreach (var point in GetVerticalOrHorizontalLineBetweenTwoPoints(line.start, line.end))
                    {
                        map[point.X, point.Y]++;
                    }
                    continue;
                }
            }

            var part1Score = $"{Count(map, 2)}";

            foreach (var line in lines)
            {

                if (!VerticalOrHorizontalLine(line.start, line.end))
                {
                    foreach (var point in GetDiagonalLineBetweenTwoPoints(line.start, line.end))
                    {
                        map[point.X, point.Y]++;
                    }
                    continue;
                }
            }

            var part2Score = $"{Count(map, 2)}";

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private bool VerticalOrHorizontalLine(Coordinate start, Coordinate end)
        {
            if (start.X == end.X) return true;
            if (start.Y == end.Y) return true;
            return false;
        }

        private int Count(int[,] map, int minValue)
        {
            var amount = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] >= minValue)
                    {
                        amount++;
                    }
                }
            }

            return amount;
        }

        private void Print(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        private IEnumerable<Coordinate> GetVerticalOrHorizontalLineBetweenTwoPoints(Coordinate start, Coordinate end)
        {
            var coordinates = new List<Coordinate>();

            if (start.X == end.X)
            {
                var x = start.X;
                var startY = Math.Min(start.Y, end.Y);
                var distance = Math.Abs(start.Y - end.Y);
                foreach (var point in Enumerable.Range(startY, distance + 1))
                {
                    coordinates.Add(new Coordinate(x, point));
                }
            }

            if (start.Y == end.Y)
            {
                var y = start.Y;
                var startX = Math.Min(start.X, end.X);
                var distance = Math.Abs(start.X - end.X);
                foreach (var point in Enumerable.Range(startX, distance + 1))
                {
                    coordinates.Add(new Coordinate(point, y));
                }
            }

            return coordinates;
        }

        private IEnumerable<Coordinate> GetDiagonalLineBetweenTwoPoints(Coordinate start, Coordinate end)
        {
            var coordinates = new List<Coordinate>();

            var distance = Math.Abs(start.X - end.X);
            for (var i = 0; i <= distance; i++)
            {
                var x = (start.X > end.X ? -1 : 1) * i;
                var y = (start.Y > end.Y ? -1 : 1) * i;
                var newCoordinate = new Coordinate(start.X + x, start.Y + y);
                coordinates.Add(newCoordinate);
            }

            return coordinates;
        }
    }

    public record Coordinate(int X, int Y);
}
