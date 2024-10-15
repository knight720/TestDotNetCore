Do();
//await DoAsync();
Console.WriteLine("2");

async Task Do()
//async Task DoAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("1");
}