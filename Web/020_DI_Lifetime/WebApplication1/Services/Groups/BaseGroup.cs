using WebApplication1.Services.Engines;

namespace WebApplication1.Services.Groups
{
    public class BaseGroup
    {
        private readonly ILogger<BaseGroup> _logger;
        private readonly Engine _engine;

        public BaseGroup(ILogger<BaseGroup> logger,
                         Engine engine)
        {
            this._logger = logger;
            this._engine = engine;
        }

        public void Calculate()
        {
            this._logger.LogInformation(this._engine.Process());
        }
    }
}