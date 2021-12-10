namespace AdventOfCode
{
    public class Day10 : Day, IDay
    {
        public Day10(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var input = await _http.GetStringAsync("/2021/day/10/input");

            var part1Score = 0;
            foreach (var line in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                var characters = new Stack<char>();

                foreach (var character in line)
                {
                    if (IsOpeningCharacter(character))
                    {
                        characters.Push(character);
                    }

                    if (IsClosingCharacter(character))
                    {
                        var lastOpeningCharacter = characters.Pop();
                        if (InvalidCharacterScore(character) == InvalidCharacterScore(lastOpeningCharacter)) continue;

                        var pepe = InvalidCharacterScore(character);
                        part1Score += pepe;
                        break;
                    }
                }
            }

            var scores = new List<long>();
            foreach (var line in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                var characters = new Stack<char>();

                foreach (var character in line)
                {
                    if (IsOpeningCharacter(character))
                    {
                        characters.Push(character);
                    }

                    if (IsClosingCharacter(character))
                    {
                        var lastOpeningCharacter = characters.Pop();
                        if (InvalidCharacterScore(character) != InvalidCharacterScore(lastOpeningCharacter))
                        {
                            characters.Clear();
                            break;
                        };
                    }
                }

                long score = 0;
                foreach(var character in characters)
                {                    
                    var closingCharacter = GetClosingCharacter(character);
                    score *= 5;
                    score += MissingCharacterScore(closingCharacter);
                }

                if(score != 0)
                {
                    scores.Add(score);
                }
            }

            var part2Score = scores.OrderBy(num => num).ElementAt(scores.Count() / 2);

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private bool IsOpeningCharacter(char input)
        {
            switch (input)
            {
                case '(':
                    return true;
                case '[':
                    return true;
                case '{':
                    return true;
                case '<':
                    return true;
                default:
                    return false;
            }
        }

        private bool IsClosingCharacter(char input)
        {
            switch (input)
            {
                case ')':
                    return true;
                case ']':
                    return true;
                case '}':
                    return true;
                case '>':
                    return true;
                default:
                    return false;
            }
        }

        private char GetClosingCharacter(char input)
        {
            switch (input)
            {
                case '(':
                    return ')';
                case '[':
                    return ']';
                case '{':
                    return '}';
                case '<':
                    return '>';
                default:
                    throw new NotImplementedException();
            }
        }

        private int InvalidCharacterScore(char input)
        {
            switch (input)
            {
                case '(':
                case ')':
                    return 3;
                case '[':
                case ']':
                    return 57;
                case '{':
                case '}':
                    return 1197;
                case '<':
                case '>':
                    return 25137;
                default:
                    return 0;
            }
        }

        private int MissingCharacterScore(char input)
        {
            switch (input)
            {
                case '(':
                case ')':
                    return 1;
                case '[':
                case ']':
                    return 2;
                case '{':
                case '}':
                    return 3;
                case '<':
                case '>':
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
