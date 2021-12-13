namespace AdventOfCode
{
    public class Day11 : Day, IDay
    {
        public Day11(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/11/input");

            var part1Score = Part1(GetOctopusesFromInput(input));

            var part2Score = Part2(GetOctopusesFromInput(input));
            
            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private Octopus[,] GetOctopusesFromInput(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var maxLineLength = lines.Max(line => line.Count());

            var octopuses = new Octopus[lines.Length, maxLineLength];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (int j = 0; j < maxLineLength; j++)
                {
                    var character = line[j].ToString();
                    octopuses[i, j] = new Octopus(int.Parse(character));
                }
            }
            return octopuses;
        }

        private int Part1(Octopus[,] octopuses)
        {
            var iterations = 100;
            for (int interation = 0; interation < iterations; interation++)
            {
                for (int i = 0; i < octopuses.GetLength(0); i++)
                {
                    for (int j = 0; j < octopuses.GetLength(1); j++)
                    {
                        if (octopuses[i, j].Energy >= 9)
                        {
                            octopuses = FlashAdjacent(i, j, octopuses);
                        }

                        if (!octopuses[i, j].HasFlashedThisIteration)
                        {
                            octopuses[i, j] = octopuses[i, j] with { Energy = octopuses[i, j].Energy + 1 };
                        }
                    }
                }

                for (int i = 0; i < octopuses.GetLength(0); i++)
                {
                    for (int j = 0; j < octopuses.GetLength(1); j++)
                    {
                        octopuses[i, j] = octopuses[i, j] with { HasFlashedThisIteration = false };
                    }
                }
            }

            return Flashes;
        }

        private int Part2(Octopus[,] octopuses)
        {
            var iteration = 0;
            while (true)
            {
                CurrentIterationFlashes = 0;
                iteration++;

                for (int i = 0; i < octopuses.GetLength(0); i++)
                {
                    for (int j = 0; j < octopuses.GetLength(1); j++)
                    {
                        if (octopuses[i, j].Energy >= 9)
                        {
                            octopuses = FlashAdjacent(i, j, octopuses);
                        }

                        if (!octopuses[i, j].HasFlashedThisIteration)
                        {
                            octopuses[i, j] = octopuses[i, j] with { Energy = octopuses[i, j].Energy + 1 };
                        }
                    }
                }

                for (int i = 0; i < octopuses.GetLength(0); i++)
                {
                    for (int j = 0; j < octopuses.GetLength(1); j++)
                    {
                        octopuses[i, j] = octopuses[i, j] with { HasFlashedThisIteration = false };
                    }
                }

                if (CurrentIterationFlashes == octopuses.GetLength(0) * octopuses.GetLength(1))
                {
                    break;
                }
            }

            return iteration;
        }

        private int Flashes { get; set; } = 0;
        private int CurrentIterationFlashes { get; set; }

        private Octopus[,] FlashAdjacent(int x, int y, Octopus[,] octopuses)
        {
            if (octopuses[x, y].HasFlashedThisIteration) return octopuses;

            octopuses[x, y] = octopuses[x, y] with { Energy = octopuses[x, y].Energy + 1 };

            if (octopuses[x, y].Energy > 9 && !octopuses[x, y].HasFlashedThisIteration)
            {
                octopuses[x, y] = octopuses[x, y] with { HasFlashedThisIteration = true, Energy = 0 };
                Flashes++;
                CurrentIterationFlashes++;
            } else
            {
                return octopuses;
            }

            var maxX = octopuses.GetLength(0);
            var maxY = octopuses.GetLength(1);

            if (x + 1 < maxX) // top
            {
                octopuses = FlashAdjacent(x + 1, y, octopuses);
            }

            if (x + 1 < maxX && y + 1 < maxY) // top right
            {
                octopuses = FlashAdjacent(x + 1, y + 1, octopuses);
            }

            if (y + 1 < maxY) // right
            {
                octopuses = FlashAdjacent(x, y + 1, octopuses);
            }

            if (x - 1 >= 0 && y + 1 < maxY) // bottom right
            {
                octopuses = FlashAdjacent(x - 1, y + 1, octopuses);
            }

            if (x - 1 >= 0) // bottom
            {
                octopuses = FlashAdjacent(x - 1, y, octopuses);
            }

            if (x - 1 >= 0 && y - 1 >= 0) // bottom left
            {
                octopuses = FlashAdjacent(x - 1, y - 1, octopuses);
            }

            if (y - 1 >= 0) //left
            {
                octopuses = FlashAdjacent(x, y - 1, octopuses);
            }

            if (x + 1 < maxX && y - 1 >= 0) //top left
            {
                octopuses = FlashAdjacent(x + 1, y - 1, octopuses);
            }

            return octopuses;
        }
        
        private record Octopus(int Energy, bool HasFlashedThisIteration = false);
    }
}
