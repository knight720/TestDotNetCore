// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ConsoleApp1;
using ConsoleApp1.Services;

Console.WriteLine("Hello, World!");

//await Sample();

var summary = BenchmarkRunner.Run<Runner>();

static async Task Sample()
{
    var cacheService = new CacheService("localhost");

    var keys = new List<string>
{
    "a",
    "b",
};

    var result = await cacheService.GetListAsync(keys);

    Console.WriteLine($"GetListAsync: {result.Count()}");
    result.Select((i, index) => $"{keys[index]}: {i}")
          .ToList()
          .ForEach(i => Console.WriteLine(i));

    var resultWithBatch = await cacheService.GetListWithBatchAsync(keys);

    Console.WriteLine($"GetListAsync: {resultWithBatch.Count()}");
    resultWithBatch.Select((i, index) => $"{keys[index]}: {i}")
                   .ToList()
                   .ForEach(i => Console.WriteLine(i));
}