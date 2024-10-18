DoAsync();

Thread.Sleep(1000);
Console.WriteLine("2");

async void DoAsync()
{
    await Task.Run(() => throw new Exception("Some Error!"));
}