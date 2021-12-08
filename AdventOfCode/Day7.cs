namespace AdventOfCode
{
    public class Day7 : Day, IDay
    {
        public Day7(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/7/input");
            var numbers = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n));

            var fuelAmount = new Dictionary<int, int>();
            for (var i = numbers.Min(); i <= numbers.Max(); i++)
            {
                var fuel = 0;
                foreach (var number in numbers)
                {
                    fuel += Math.Abs(i - number);
                }

                fuelAmount.Add(i, fuel);
            }
            var part1Score = $"{fuelAmount.Min(pair => pair.Value)}";

            fuelAmount = new Dictionary<int, int>();
            for (var i = numbers.Min(); i <= numbers.Max(); i++)
            {
                var fuel = 0;
                foreach (var number in numbers)
                {
                    fuel += NthTriangle(Math.Abs(i - number));
                }

                fuelAmount.Add(i, fuel);
            }


            var part2Score = $"{fuelAmount.Min(pair => pair.Value)}";
            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private int NthTriangle(int input)
        {
            var output = (int)(Math.Pow(input, 2) + input) / 2;
            return output;
        }
    }
}
