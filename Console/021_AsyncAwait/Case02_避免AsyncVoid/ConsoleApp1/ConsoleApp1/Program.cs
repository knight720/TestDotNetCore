DoAsync();
//await DoAsync()

Console.WriteLine("2");

async void DoAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("1");
}