using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unique
{
    internal class Program
    {
        private Object _lock = new Object();

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var p = new Program();
            //p.TestUnique(p.TimeTicks);
            p.TestParallel();

            Console.WriteLine("Finish!");
        }

        private void TestUnique(Func<string> generator)
        {
            var set = new HashSet<string>();
            for (int i = 0; i < int.MaxValue; i++)
            {
                set.Add(generator());
            }

            Console.WriteLine($"Count: {set.Count}");
        }

        private void TestParallel()
        {
            var set = new HashSet<string>();

            Parallel.For(0, int.MaxValue, i =>
            {
                var key = TimeTicks();
                lock (_lock)
                {
                    set.Add(key);
                }

                if (i == 1000000)
                {
                    int a = 0;
                }
            });

            Console.WriteLine($"Count: {set.Count}");
        }

        private string TimeTicks()
        {
            long ticks = DateTime.Now.Ticks;
            byte[] bytes = BitConverter.GetBytes(ticks);
            string id = Convert.ToBase64String(bytes)
                                    .Replace('+', '_')
                                    .Replace('/', '-')
                                    .TrimEnd('=');

            return id;
        }
    }
}