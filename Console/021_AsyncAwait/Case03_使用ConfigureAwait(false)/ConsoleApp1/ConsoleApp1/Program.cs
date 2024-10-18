await DoAsync();
//await DoAsync().ConfigureAwait(false);

Console.WriteLine("2");

async Task DoAsync()
{
    await Task.Delay(100);
    Console.WriteLine("1");
}