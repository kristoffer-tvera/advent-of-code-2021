namespace AdventOfCode
{
    public class Day1: Day, IDay
    {

        public Day1(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var response = await _http.GetStringAsync("/2021/day/1/input");

            var list = response.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();

            var score = 0;

            for (int i = 1; i < list.Count(); i++)
            {
                if (list[i] > list[i - 1])
                {
                    score++;
                }
            }

            var part1Score = score.ToString();

            score = 0;

            for (int i = 3; i < list.Count(); i++)
            {
                var first = list[i - 3] + list[i - 2] + list[i - 1];
                var second = list[i - 2] + list[i - 1] + list[i];
                if (first < second)
                {
                    score++;
                }
            }

            var part2Score = score.ToString();

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }
    }
}
