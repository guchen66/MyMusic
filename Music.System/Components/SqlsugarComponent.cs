using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Components
{
    public class SqlsugarComponent : IContainerComponent
    {
        public void Load(IContainerRegistry registry, ComponentContext context)
        {
           /* registry.RegisterScoped(typeof(IBaseService<>), typeof(BaseService<>));
            registry.RegisterScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            registry.RegisterScoped<IProductDataConfigService,ProductDataConfigService>();*/
            // containerRegistry.RegisterScoped<IUserService, UserService>();
            // containerRegistry.Register<IUserRepository, UserRepository>();
        }
    }
}
