namespace AdventOfCode
{
    public class Day14 : Day, IDay
    {
        public Day14(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/14/input");

            object part1Score = null;
            object part2Score = null;

            return $"Part 1: {part1Score}, Part 2: {part2Score}";

        }
    }
}
