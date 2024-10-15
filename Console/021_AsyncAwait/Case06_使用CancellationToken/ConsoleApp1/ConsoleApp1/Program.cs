CancellationTokenSource source = new CancellationTokenSource();
CancellationToken token = source.Token;

var task = DoAsync(token);

await Task.Delay(1000);

Console.WriteLine("2");

//source.Cancel();
task.Wait();

async Task DoAsync(CancellationToken cancellationToken)
{
    try
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"DoAsync: {i}");
            await Task.Delay(300);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }

    Console.WriteLine("1");
}