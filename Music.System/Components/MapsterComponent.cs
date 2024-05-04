using Mapster;
using MapsterMapper;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Components
{
    public class MapsterComponent : IContainerComponent
    {
        public void Load(IContainerRegistry registry, ComponentContext context)
        {
            var config = new TypeAdapterConfig();
            var assembly = Assembly.Load("Music.System");
            config.Scan(assembly);

            // 注册单例实例
            registry.RegisterInstance(typeof(TypeAdapterConfig), config);

            // 创建并注册 Mapper 实例
            var mapper = new Mapper(config);
            registry.RegisterInstance(typeof(Mapper), mapper);
            registry.Register<IMapper, Mapper>();

           /* MapsterIocService.RegisterMapster(x =>
            {
                x.Adapt(registry);
            });*/
        }
    }

    public class MapsterIocService
    {
        public static void RegisterMapster(Action<Mapper> mapper)
        {
            MapsterExtension.Mapper = mapper;
        }
    }

    public static class MapsterExtension
    {
        public static Action<Mapper> Mapper { get; set; }
    }
}
