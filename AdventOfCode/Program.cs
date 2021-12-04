using AdventOfCode;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var day1 = new Day1(builder.Configuration);
var day2 = new Day2(builder.Configuration);
var day3 = new Day3(builder.Configuration);
var day4 = new Day4(builder.Configuration);

var results = new Dictionary<string, string>();
results.Add("Day1", await day1.Solution());
results.Add("Day2", await day2.Solution());
results.Add("Day3", await day3.Solution());
results.Add("Day4", await day4.Solution());

app.MapGet("/", () => "Hello World!\n" + string.Join("\n", results.Select(dict => $"{dict.Key}: {dict.Value}")));


app.Run();
