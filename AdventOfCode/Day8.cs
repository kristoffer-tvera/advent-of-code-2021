namespace AdventOfCode
{
    public class Day8 : Day, IDay
    {
        public Day8(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            //var input = await _http.GetStringAsync("/2021/day/8/input");
            var input = "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |fdgacbe cefdb cefbgd gcbe\nedbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |fcgedb cgb dgebacf gc\nfgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg\nfbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb\naecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea\nfgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb\ndbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe\nbdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef\negadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb\ngcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce";

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

            var part2Score = "";
            var alphabet = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'};
            foreach(var entry in data)
            {
                var a = "";
                var b = "";
                var c = "";
                var d = "";
                var e = "";
                var f = "";
                var g = "";

                var one = entry.signalPatterns.First(val => val.Count() == 2);
                var seven = entry.signalPatterns.First(val => val.Count() == 3);
                var four = entry.signalPatterns.First(val => val.Count() == 4);
                var eight = entry.signalPatterns.First(val => val.Count() == 7);
                //var zero = entry.signalPatterns.First(val => val.Count() == 6 && )

                var zeroSixNine = entry.signalPatterns.Where(val => val.Count() == 6).SelectMany(str => str.ToCharArray()).Distinct();
                var zeroSixNineWhiteSpaces = alphabet.Except(zeroSixNine);

                a = seven.Intersect(four).ToString();
                c = zeroSixNineWhiteSpaces.Union(one).First().ToString();
                f = one.Except(c).First().ToString();

                var six = entry.signalPatterns.First(val => val.Count() == 6 && !c.Contains(c));


            }


            return $"Part 1: {part1Score}, Part 2: {part2Score}";

        }
    }
}
