Do();
Console.WriteLine("2");

async Task Do()
{
    await Task.Delay(1000);
    Console.WriteLine("1");
}