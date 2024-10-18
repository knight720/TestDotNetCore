var task = Task.Run(() => Do());

Console.WriteLine("2");

await task;

void Do()
{
    Thread.Sleep(100);
    Console.WriteLine("1");
}