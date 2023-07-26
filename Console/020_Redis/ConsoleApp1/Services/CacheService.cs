using StackExchange.Redis;

namespace ConsoleApp1.Services
{
    public class CacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        private IDatabase GetDatabase
        {
            get
            {
                return this._connectionMultiplexer.GetDatabase();
            }
        }

        public CacheService(string connection)
        {
            this._connectionMultiplexer = ConnectionMultiplexer.Connect(connection);
        }

        public async Task<IEnumerable<string>> GetListAsync(IEnumerable<string> keys)
        {
            var db = this.GetDatabase;

            var result = await db.StringGetAsync(keys.Select(i => new RedisKey(i)).ToArray());

            return result.Select(i => i.ToString()).ToArray();
        }

        public async Task<IEnumerable<string>> GetListWithBatchAsync(IEnumerable<string> keys)
        {
            var db = this.GetDatabase;
            var batch = db.CreateBatch();

            var taskList = keys.Select(i => batch.StringGetAsync(new RedisKey(i))).ToArray();
            batch.Execute();

            Task.WaitAll(taskList);

            return taskList.Select(i => i.Result.ToString()).ToArray();
        }
    }
}