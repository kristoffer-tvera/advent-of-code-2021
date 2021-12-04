namespace AdventOfCode
{
    public class Day3 : Day, IDay
    {
        public Day3(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> Solution()
        {
            var response = await _http.GetStringAsync("/2021/day/3/input");

            var list = response.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var bits = GetByteDistribution(list);

            var gamma = 0;
            var epsilon = 0;

            for (int i = 0; i < bits.Length; i++)
            {
                if(bits[i] > 0)
                {
                    gamma += (int)Math.Pow(2, (bits.Length - 1) -i);
                } else
                {
                    epsilon += (int)Math.Pow(2, (bits.Length - 1) -i);
                }
            }

            var gamma2 = BinaryArrayToDecimal(bits);
            var epsilon2 = BinaryArrayToDecimal(bits, true);

            var part1Score = $"{gamma * epsilon}";

            var oxygen = list.Select(x => x).ToList();
            for (int i = 0; i < bits.Length; i++)
            {
                bits = GetByteDistribution(oxygen);

                if(oxygen.Count == 1) { break; }

                if(bits[i] == 0) {
                    oxygen = oxygen.Where(line => line[i] == '1').ToList();
                }
                else if (bits[i] > 0)
                {
                    oxygen = oxygen.Where(line => line[i] == '1').ToList();
                }
                else
                {
                    oxygen = oxygen.Where(line => line[i] == '0').ToList();
                }
            }

            var carbon = list.Select(x => x).ToList();
            for (int i = 0; i < bits.Length; i++)
            {
                bits = GetByteDistribution(carbon);

                if (carbon.Count == 1) { break; }

                if (bits[i] == 0)
                {
                    carbon = carbon.Where(line => line[i] == '0').ToList();
                }
                else if (bits[i] > 0)
                {
                    carbon = carbon.Where(line => line[i] == '0').ToList();
                }
                else
                {
                    carbon = carbon.Where(line => line[i] == '1').ToList();
                }
            }

            var part2Score = BinaryArrayToDecimal(GetByteDistribution(carbon)) * BinaryArrayToDecimal(GetByteDistribution(oxygen));

            return $"Part 1: {part1Score}, Part 2: {part2Score}";
        }

        private int BinaryArrayToDecimal(int[] array, bool reverse = false)
        {
            var dec = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0 && reverse)
                {
                    dec += (int)Math.Pow(2, (array.Length - 1) - i);
                }

                if (array[i] > 0 && !reverse)
                {
                    dec += (int)Math.Pow(2, (array.Length - 1) - i);
                }
            }

            return dec;
        }

        private int[] GetByteDistribution(IEnumerable<string> list)
        {
            var size = list.First().Length;

            var bits = new int[size];

            foreach (var line in list)
            {
                foreach (var i in Enumerable.Range(0, size))
                {
                    if (line[i] == '1')
                    {
                        bits[i]++;
                    }
                    else
                    {
                        bits[i]--;
                    }
                }
            }

            return bits;
        }
    }
}
