namespace AdventOfCode
{
    public class Day6 : Day, IDay
    {
        private readonly int NewFishDuration = 8;
        private readonly int OldFishDuration = 6;
        private readonly int InitialIterations = 80;
        private readonly int MaxIterations = 256;
        public Day6(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/6/input");
            var fish = GetFish(input);

            for (int i = 0; i < InitialIterations; i++)
            {
                var recycle = fish[0];
                for (int j = 1; j < fish.Length; j++)
                {
                    fish[j - 1] = fish[j];
                }

                fish[OldFishDuration] += recycle;
                fish[NewFishDuration] = recycle;
            }

            var part1Score = $"{CountFish(fish)}";

            fish = GetFish(input);

            for (int i = 0; i < MaxIterations; i++)
            {
                var recycle = fish[0];
                for (int j = 1; j < fish.Length; j++)
                {
                    fish[j - 1] = fish[j];
                }

                fish[OldFishDuration] += recycle;
                fish[NewFishDuration] = recycle;
            }

            var part2Score = $"{CountFish(fish)}";

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private long CountFish(long[] fishes)
        {
            long count = 0;

            foreach(var fish in fishes)
            {
                count += fish;
            }

            return count;
        }

        private long[] GetFish(string input)
        {
            var initialFishes = input.Split(',').Select(f => int.Parse(f));
            var fish = new long[NewFishDuration + 1];
            foreach (var initialFish in initialFishes)
            {
                fish[initialFish]++;
            }
            return fish;
        }
    }
}
