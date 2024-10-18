try
{
    DoAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Catch: {ex.Message}");
}

Thread.Sleep(1000);
Console.WriteLine("2");

async void DoAsync()
{
    await Task.Delay(1);
    throw new Exception("Some Exception");
}