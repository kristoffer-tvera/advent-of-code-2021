namespace AdventOfCode
{
    public class Day8 : Day, IDay
    {
        public Day8(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/8/input");

            var data = new List<(string[] signalPatterns, string[] outputValues)>();
            foreach(var line in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                var signalPattern = line.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var outputValues = line.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                data.Add((signalPattern, outputValues));
            }

            var part1Score = 0;
            foreach(var entry in data)
            {
                part1Score += entry.outputValues.Count(c => c.Count() == 2); // 1
                part1Score += entry.outputValues.Count(c => c.Count() == 4); // 4
                part1Score += entry.outputValues.Count(c => c.Count() == 3); // 7
                part1Score += entry.outputValues.Count(c => c.Count() == 7); // 8
            }

            var part2Score = 0;
            foreach(var entry in data)
            {
                var signalMap = SignalMapping(entry.signalPatterns);
                var finalNumber = "";

                foreach (var outputValue in entry.outputValues)
                {
                    var signal = string.Join("", outputValue.Select(c => signalMap[c]));

                    var currentNumber = OutputValue(signal);
                    finalNumber += currentNumber;
                }

                part2Score += int.Parse(finalNumber);
            }

            return $"Part 1: {part1Score}, Part 2: {part2Score}";

        }

        /// <summary>
        /// Key-value mapping where input-char becomes key, and actual char becomes value
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private Dictionary<char, char> SignalMapping(string[] entry)
        {
            var alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            var map = new Dictionary<char, char>();

            var one = entry.Single(val => val.Count() == 2);
            var seven = entry.Single(val => val.Count() == 3);
            var four = entry.Single(val => val.Count() == 4);
            var eight = entry.Single(val => val.Count() == 7);

            var zeroSixNine = entry.Where(val => val.Count() == 6);
            var zeroSixNineWhiteSpace = new List<char>();
            foreach(var num in zeroSixNine)
            {
                zeroSixNineWhiteSpace.Add(alphabet.Except(num).Single());
            }

            var a = seven.Except(four).Single().ToString();
            var c = zeroSixNineWhiteSpace.Intersect(one.ToCharArray()).Single().ToString();
            var f = one.Except(c).Single().ToString();

            var six = entry.Single(val => val.Count() == 6 && !val.Contains(c));
            var two = entry.Single(val => !val.Contains(f));
            var five = entry.Single(val => val.Count() == 5 && !val.Contains(c));
            var e = alphabet.Except(five.ToCharArray()).Except(c).Single().ToString();

            var nine = entry.Single(val => val.Count() == 6 && !val.Contains(e));
            var three = entry.Single(val => val.Count() == 5 && val.Contains(c) && !val.Contains(e));
            var zero = entry.Single(val => val.Count() == 6 && val != nine && val != six);

            var d = alphabet.Except(zero.ToCharArray()).Single().ToString();
            var b = alphabet.Except(two.ToCharArray()).Except(f).Single().ToString();
            var g = alphabet.Except(a).Except(b).Except(c).Except(d).Except(e).Except(f).Single().ToString();
            
            map.Add(a[0], 'a');
            map.Add(b[0], 'b');
            map.Add(c[0], 'c');
            map.Add(d[0], 'd');
            map.Add(e[0], 'e');
            map.Add(f[0], 'f');
            map.Add(g[0], 'g');
            return map;
        }
        
        /// <summary>
        /// Sort the input string, and output the corresponding digit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string OutputValue(string input)
        {
            switch(string.Join("", input.OrderBy(c => c)))
            {
                case "abcefg":
                    return "0";
                case "cf":
                    return "1";
                case "acdeg":
                    return "2";
                case "acdfg":
                    return "3";
                case "bcdf":
                    return "4";
                case "abdfg":
                    return "5";
                case "abdefg":
                    return "6";
                case "acf":
                    return "7";
                case "abcdefg":
                    return "8";
                case "abcdfg":
                    return "9";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
