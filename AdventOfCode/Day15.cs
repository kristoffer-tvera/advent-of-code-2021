namespace AdventOfCode
{
    public class Day15 : Day, IDay
    {
        public Day15(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/15/input");
            //var input = "1163751742\n1381373672\n2136511328\n3694931569\n7463417111\n1319128137\n1359912421\n3125421639\n1293138521\n2311944581";
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var map = new Tile[lines.Length, lines.Max(l => l.Count())];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    var risk = lines[y][x].ToString();
                    map[y, x] = new Tile(int.Parse(risk));
                }
            }

            map[0, 0] = map[0, 0] with { LowestTotalRiskToThisTile = 0 };

            GetRiskForPath(map, 0, 0);

            var part1Score = map[map.GetLength(0)-1, map.GetLength(1)-1].LowestTotalRiskToThisTile;

            Print(map);

            object part2Score = null;
            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private void GetRiskForPath(Tile[,] map, int x, int y)
        {
            var adjacentTiles = GetAdjacentTiles(map, x, y).OrderBy(tuple => tuple.tile.Risk);

            if(!adjacentTiles.Any())
            {
                return;
            }

            //var score = map[y, x].Risk;
            foreach (var adjacentTile in adjacentTiles)
            {
                map[adjacentTile.y, adjacentTile.x] = map[adjacentTile.y, adjacentTile.x] with 
                { 
                    LowestTotalRiskToThisTile = map[adjacentTile.y, adjacentTile.x].Risk + map[y,x].LowestTotalRiskToThisTile
                };
                GetRiskForPath(map, adjacentTile.x, adjacentTile.y);
            }
        }

        private IEnumerable<(Tile tile, int x, int y)> GetAdjacentTiles(Tile[,] map, int x, int y)
        {
            var adjacentTiles = new List<(Tile tile, int x, int y)>();
            if(y + 1 < map.GetLength(0))
            {
                var newCost = map[y, x].LowestTotalRiskToThisTile + map[y + 1, x].Risk;
                if(newCost < map[y + 1, x].LowestTotalRiskToThisTile)
                {
                    adjacentTiles.Add((map[y + 1, x], x, y + 1));
                }
            }

            if (y - 1 >= 0)
            {
                var newCost = map[y, x].LowestTotalRiskToThisTile + map[y - 1, x].Risk;
                if (newCost < map[y - 1, x].LowestTotalRiskToThisTile)
                {
                    adjacentTiles.Add((map[y - 1, x], x, y - 1));
                }
            }

            if (x + 1 < map.GetLength(1))
            {
                var newCost = map[y, x].LowestTotalRiskToThisTile + map[y, x + 1].Risk;
                if (newCost < map[y, x + 1].LowestTotalRiskToThisTile)
                {
                    adjacentTiles.Add((map[y, x + 1], x + 1, y));
                }
            }

            if (x - 1 >= 0)
            {
                var newCost = map[y, x].LowestTotalRiskToThisTile + map[y, x - 1].Risk;
                if (newCost < map[y, x - 1].LowestTotalRiskToThisTile)
                {
                    adjacentTiles.Add((map[y, x - 1], x - 1, y));
                }
            }

            return adjacentTiles;
        }

        private void Print(Tile[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if(map[y, x].LowestTotalRiskToThisTile == int.MaxValue)
                    {
                        Console.Write(" -- ");
                    }
                    else
                    {
                        Console.Write(map[y, x].LowestTotalRiskToThisTile.ToString());
                    }
                }
                Console.WriteLine();
            }
        }

        private record Tile(int Risk, int LowestTotalRiskToThisTile = int.MaxValue);
    }
}
