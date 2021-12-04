namespace AdventOfCode
{
    public class Day2 : Day, IDay
    {
        public Day2(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var response = await _http.GetStringAsync("/2021/day/2/input");

            var list = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var horizontal = 0;
            var depth = 0;

            foreach ((string direction, int amount) in list.Select(line => { var split = line.Split(" "); return (split[0], int.Parse(split[1])); })) {

                switch (direction) {
                    case "forward":
                        horizontal += amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            var part1Score = depth * horizontal;

            var aim = 0;
            horizontal = 0;
            depth = 0;

            foreach ((string direction, int amount) in list.Select(line => { var split = line.Split(" "); return (split[0], int.Parse(split[1])); }))
            {

                switch (direction)
                {
                    case "forward":
                        horizontal += amount;
                        depth += aim * amount;

                        break;
                    case "down":
                        aim += amount;
                        break;
                    case "up":
                        aim -= amount;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            var part2Score = depth * horizontal;

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }
    }
}
