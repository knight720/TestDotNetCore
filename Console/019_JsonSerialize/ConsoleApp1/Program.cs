// See https://aka.ms/new-console-template for more information
using System.Text.Json;

Console.WriteLine("Hello, World!");

var a = new AClass();
Console.WriteLine("WriteIndented = defulat");
Console.WriteLine(JsonSerializer.Serialize(a));
Console.WriteLine("WriteIndented = true");
Console.WriteLine(JsonSerializer.Serialize(a, new JsonSerializerOptions { WriteIndented = true }));

Console.ReadLine();

internal class AClass
{
    public int MyProperty { get; set; }
    public int Second { get; set; }
}