using WebApplication1.Services.Engines;

namespace WebApplication1.Services.Groups
{
    public class AGroup : BaseGroup
    {
        public AGroup(ILogger<AGroup> logger, Engine engine) : base(logger, engine)
        {
        }
    }
}