namespace AdventOfCode
{
    public class Day
    {
        internal readonly HttpClient _http;

        public Day(IConfiguration configuration)
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri("https://adventofcode.com");
            _http.DefaultRequestHeaders.Add("cookie", configuration["Cookie"]);
        }
    }
}
