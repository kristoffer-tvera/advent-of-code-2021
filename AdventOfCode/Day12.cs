namespace AdventOfCode
{
    public class Day12 : Day, IDay
    {
        public Day12(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/12/input");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var caves = new List<Cave>();

            foreach (var line in lines)
            {
                var currentCaves = line.Split('-');

                var cave1 = GetCave(currentCaves[0], ref caves);
                var cave2 = GetCave(currentCaves[1], ref caves);

                cave1.ConnectingCaves.Add(cave2);
                cave2.ConnectingCaves.Add(cave1);

                caves.Add(cave1);
                caves.Add(cave2);
            }

            var start = caves.Single(c => c.Name == "start");
            var part1Score = NumberOfRoutesToTheEnd(start, new List<Cave>());
            var part2Score = AlternativeNumberOfRoutesToTheEnd(start, new List<Cave>());

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }


        private int NumberOfRoutesToTheEnd(Cave cave, List<Cave> cavesVisited)
        {
            cavesVisited.Add(cave);
            if (cave.Name == "end")
            {
                return 1;
            };

            var score = 0;
            for (int i = 0; i < cave.ConnectingCaves.Count; i++)
            {
                if (cave.ConnectingCaves[i].StartCave) continue;
                if (cave.ConnectingCaves[i].SmallCave && cavesVisited.Any(c => c.Name == cave.ConnectingCaves[i].Name)) continue;

                var newCavesVisited = new List<Cave>();
                newCavesVisited.AddRange(cavesVisited);
                score += NumberOfRoutesToTheEnd(cave.ConnectingCaves[i], newCavesVisited);
            }

            return score;
        }

        private int AlternativeNumberOfRoutesToTheEnd(Cave cave, List<Cave> cavesVisited)
        {
            cavesVisited.Add(cave);
            if (cave.Name == "end")
            {
                return 1;
            };

            var score = 0;
            for (int i = 0; i < cave.ConnectingCaves.Count; i++)
            {
                if (cave.ConnectingCaves[i].StartCave) continue;
                if (CaveShouldBeSkipped(cavesVisited, cave.ConnectingCaves[i])) continue;

                var newCavesVisited = new List<Cave>();
                newCavesVisited.AddRange(cavesVisited);
                score += AlternativeNumberOfRoutesToTheEnd(cave.ConnectingCaves[i], newCavesVisited);
            }

            return score;
        }

        private bool CaveShouldBeSkipped(List<Cave> cavesVisited, Cave cave)
        {
            if (!cave.SmallCave) return false;
            if (!cavesVisited.Any(c => c.Name == cave.Name)) return false;

            var currentCaveCount = cavesVisited.Count(c => c.Name == cave.Name);
            if (currentCaveCount == 2) return true;

            var otherSmallCaves = cavesVisited
                .Where(c => c.Name != cave.Name)
                .Where(c => c.SmallCave);

            if (otherSmallCaves.Any())
            {
                var maxCount = otherSmallCaves
                .GroupBy(c => c.Name)
                .Max(group => group.Count());

                if (currentCaveCount == 2 && maxCount == 1) return false;
                if (currentCaveCount == 1 && maxCount == 2) return true;
            }

            if (currentCaveCount == 1) return false;

            return true;
        }

        private Cave GetCave(string caveName, ref List<Cave> caves)
        {
            var cave = caves.FirstOrDefault(c => c.Name == caveName);
            if (cave != null)
            {
                caves.Remove(cave);
            }
            else
            {
                cave = new Cave(caveName, new List<Cave>());
            }
            return cave;
        }

        private record Cave(string Name, List<Cave> ConnectingCaves)
        {
            public bool SmallCave => Name != "start" && Name != "end" && Name.All(c => char.IsLower(c));
            public bool StartCave => Name == "start";
        };
    }
}
