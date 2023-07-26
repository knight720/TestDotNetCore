using BenchmarkDotNet.Attributes;
using ConsoleApp1.Services;

namespace ConsoleApp1
{
    public class Runner
    {
        //// TODO 調整連線字串
        private readonly string _connection = "localhost";
        private readonly Random _random;
        private IEnumerable<string> _keys;
        private readonly CacheService _cacheService;

        public Runner()
        {
            this._random = new Random((int)DateTime.Now.Millisecond);
            //// 初始化 Key
            this.GetKeys();
            this._cacheService = new CacheService(this._connection);
        }

        private IEnumerable<string> GetKeys()
        {
            if (this._keys == null)
            {
                //// TODO 調整測試數量
                int length = 100;
                this._keys = Enumerable.Range(1, length)
                                       .Select(i => this._random.Next(1, 100).ToString())
                                       .ToArray();
            }
            return this._keys;
        }

        [Benchmark(Baseline = true)]
        public void GetListAsync()
        {
            this._cacheService.GetListAsync(this.GetKeys()).GetAwaiter().GetResult();
        }

        [Benchmark]
        public void GetListWithBatchAsync()
        {
            this._cacheService.GetListAsync(this.GetKeys()).GetAwaiter().GetResult();
        }
    }
}