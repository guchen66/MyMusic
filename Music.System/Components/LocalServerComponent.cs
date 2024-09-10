using Mapster;
using MapsterMapper;
using Music.System.Services.MainSign.DownLoads;
using MySqlConnector.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Components
{
    public class LocalServerComponent : IContainerComponent
    {
        public void Load(IContainerRegistry registry, ComponentContext context)
        {
            registry.Register<ILoginService, LoginService>();
            registry.Register<IReadService, ReadService>();
            //  registry.RegisterSingleton<Kstopa.Lx.Admin.IServices.ILogger, DefaultLogger>();
        }
    }
}
