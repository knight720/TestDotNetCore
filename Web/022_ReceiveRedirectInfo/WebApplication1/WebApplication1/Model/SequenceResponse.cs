namespace WebApplication1.Model
{
    public class SequenceResponse : IResponseService
    {
        private static long _seed = 0;

        public async Task<string> Response()
        {
            await Task.Delay(1000);

            return Interlocked.Increment(ref _seed).ToString();
        }
    }
}