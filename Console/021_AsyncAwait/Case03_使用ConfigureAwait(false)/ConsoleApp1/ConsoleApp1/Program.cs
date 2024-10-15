await DoAsync();
//await DoAsync().ConfigureAwait(false);

Console.WriteLine("2");

async Task DoAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("1");
}