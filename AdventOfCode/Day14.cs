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

            var template = input.Split("\n\n")[0];
            var rules = ParseRules(input.Split("\n\n")[1].Split('\n', StringSplitOptions.RemoveEmptyEntries));

            var part1 = PolymerSimulation(template, rules, 10);
            var part1Score = PolymerCount(part1);

            var part2 = PolymerSimulation(template, rules, 40);
            var part2Score = PolymerCount(part2);

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private long PolymerCount(Dictionary<Polymer, long> polymerBindings)
        {
            var letterCount = new Dictionary<char, long>();

            foreach (var binding in polymerBindings)
            {
                if (letterCount.ContainsKey(binding.Key.First))
                {
                    letterCount[binding.Key.First] += binding.Value;
                } else
                {
                    letterCount.Add(binding.Key.First, binding.Value);
                }

                if (letterCount.ContainsKey(binding.Key.Second))
                {
                    letterCount[binding.Key.Second] += binding.Value;
                }
                else
                {
                    letterCount.Add(binding.Key.Second, binding.Value);
                }
            }

            var ordered = letterCount.OrderBy(x => x.Value);

            long sum = ordered.Last().Value - ordered.First().Value;

            sum = (long)Math.Ceiling((double)sum / 2); //....

            return sum;
        }

        private Dictionary<Polymer, long> PolymerSimulation(string template, Dictionary<Polymer, string> rules, int iterations)
        {
            var polymers = new Dictionary<Polymer, long>();

            for (int i = 1; i < template.Length; i++)
            {
                var polymer = new Polymer(template[i - 1], template[i]);

                if (!polymers.ContainsKey(polymer))
                {
                    polymers.Add(polymer, 1);
                    continue;
                }

                polymers[polymer]++;
            }

            for (int iteration = 0; iteration < iterations; iteration++)
            {

                var newPolymers = new Dictionary<Polymer, long>();

                foreach(var polymer in polymers.Keys)
                {
                    var count = polymers[polymer];

                    if (rules.TryGetValue(polymer, out var rule))
                    {
                        var firstPolymer = new Polymer(polymer.First, rule[0]);
                        var secondPolymer = new Polymer(rule[0], polymer.Second);

                        if (!newPolymers.ContainsKey(firstPolymer))
                        {
                            newPolymers.Add(firstPolymer, count);
                        }
                        else
                        {
                            newPolymers[firstPolymer] += count;
                        }

                        if (!newPolymers.ContainsKey(secondPolymer))
                        {
                            newPolymers.Add(secondPolymer, count);
                        }
                        else
                        {
                            newPolymers[secondPolymer] += count;
                        }

                    } else
                    {
                        if (!newPolymers.ContainsKey(polymer))
                        {
                            newPolymers.Add(polymer, count);
                        }
                        else
                        {
                            newPolymers[polymer] += count;
                        }
                    }
                }

                polymers = new Dictionary<Polymer, long>(newPolymers);
            }

            return polymers;
        }

        /// <summary>
        /// Exponential Growth smoked us.
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        //private int PolymerCount(string template, Dictionary<string, string> rules, int iterations)
        //{
        //    for (int iteration = 0; iteration < iterations; iteration++)
        //    {
        //        var sb = new System.Text.StringBuilder();
        //        sb.Append(template[0]);
        //        for (int i = 1; i < template.Length; i++)
        //        {
        //            var key = $"{template[i - 1]}{template[i]}";

        //            if (rules.ContainsKey(key))
        //            {
        //                sb.Append($"{rules[key]}{template[i]}");
        //            }
        //            else
        //            {
        //                sb.Append(key[1]);
        //            }
        //        }
        //        template = sb.ToString();
        //    }

        //    var groupedByLetterOrderedByCount = template.GroupBy(c => c).OrderBy(group => group.Count());
        //    var smallestGroup = groupedByLetterOrderedByCount.First().Count();
        //    var biggestGroup = groupedByLetterOrderedByCount.Last().Count();
        //    return biggestGroup - smallestGroup;
        //}

        private Dictionary<Polymer, string> ParseRules(IEnumerable<string> rules)
        {
            var dictionary = new Dictionary<Polymer, string>();
            foreach(var rule in rules)
            {
                var split = rule.Split(" -> ");
                var key = split[0];
                var value = split[1];
                dictionary.Add(new Polymer(key[0], key[1]), value);
            }
            return dictionary;
        }

        private record Polymer(char First, char Second);
    }
}
