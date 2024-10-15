try
{
    DoAsync();
    //await DoAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Catch: {ex.Message}");
}

Console.WriteLine("2");

async Task DoAsync()
{
    await Task.Delay(1000);
    throw new Exception("Some Exception");
}