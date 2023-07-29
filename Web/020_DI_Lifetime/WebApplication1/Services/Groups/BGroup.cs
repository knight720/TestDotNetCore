using WebApplication1.Services.Engines;

namespace WebApplication1.Services.Groups
{
    public class BGroup : BaseGroup
    {
        public BGroup(ILogger<AGroup> logger, Engine engine) : base(logger, engine)
        {
        }
    }
}