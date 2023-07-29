namespace WebApplication1.Services.Engines
{
    public class Engine
    {
        private string _guid = Guid.NewGuid().ToString();

        public string Process()
        {
            return this._guid;
        }
    }
}