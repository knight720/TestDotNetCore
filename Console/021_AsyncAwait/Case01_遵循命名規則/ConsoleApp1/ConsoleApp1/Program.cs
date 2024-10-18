Do();

Console.WriteLine("2");

async Task Do()
{
    await Task.Delay(100);
    Console.WriteLine("1");
}