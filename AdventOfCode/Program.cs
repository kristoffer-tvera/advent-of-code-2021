using AdventOfCode;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var day1 = new Day1(builder.Configuration);
var day2 = new Day2(builder.Configuration);
var day3 = new Day3(builder.Configuration);
var day4 = new Day4(builder.Configuration);
var day5 = new Day5(builder.Configuration);
var day6 = new Day6(builder.Configuration);
var day7 = new Day7(builder.Configuration);
var day8 = new Day8(builder.Configuration);
var day9 = new Day9(builder.Configuration);
var day10 = new Day10(builder.Configuration);

var results = new Dictionary<string, string>();
//results.Add("Day1", await day1.Solution());
//results.Add("Day2", await day2.Solution());
//results.Add("Day3", await day3.Solution());
//results.Add("Day4", await day4.Solution());
//results.Add("Day5", await day5.Solution());
//results.Add("Day6", await day6.Solution());
//results.Add("Day7", await day7.Solution());
//results.Add("Day8", await day8.Solution());
//results.Add("Day9", await day9.Solution());
results.Add("Day10", await day10.Solution());

app.MapGet("/", () => "Hello World!\n" + string.Join("\n", results.Select(dict => $"{dict.Key}: {dict.Value}")));


app.Run();
