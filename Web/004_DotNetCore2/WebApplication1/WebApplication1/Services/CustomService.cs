using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class CustomService
    {
        public ISample Transient { get; private set; }
        public ISample Scoped { get; private set; }
        public ISample Singleton { get; private set; }

        public CustomService(ISampleTransient transient,
            ISampleScoped scoped,
            ISampleSingleton singleton)
        {
            Transient = transient;
            Scoped = scoped;
            Singleton = singleton;
        }
    }
}
