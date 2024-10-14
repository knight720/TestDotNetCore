DoAsync();
//await DoAsync(); // CS4008 無法等候 'void'

Console.WriteLine("2");

async void DoAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("1");
}