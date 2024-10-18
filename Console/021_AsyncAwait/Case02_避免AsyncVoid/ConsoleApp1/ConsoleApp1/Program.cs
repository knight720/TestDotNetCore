try
{
    DoAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"EX: {ex.Message}");
}

Thread.Sleep(1000);
Console.WriteLine("2");

async void DoAsync()
{
    Console.WriteLine("1");
    throw new ApplicationException("Some Error!");
}