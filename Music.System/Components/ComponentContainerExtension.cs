
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Components
{
    public static class ComponentContainerExtension
    {
        public static IContainerRegistry RegisterComponent<TComponent>(this IContainerRegistry registry,object options=default) 
            where TComponent : class, IContainerComponent,new() 
        {
            return registry.RegisterComponent<TComponent,object>(options);
        }


        public static IContainerRegistry RegisterComponent<TComponent, TComponentOptions>(this IContainerRegistry registry, TComponentOptions options = default)
            where TComponent : class, IContainerComponent, new()
        {
            return registry.RegisterComponent(typeof(TComponent), options);
        }

        public static IContainerRegistry RegisterComponent(this IContainerRegistry registry, Type componentType, object options = default)
        {
            // 创建组件依赖链
            var componentContextLinkList = ManualDependProvider.CreateDependLinkList(componentType, options);

            // 逐条创建组件实例并调用
            foreach (var context in componentContextLinkList)
            {
                // 创建组件实例
                var component = Activator.CreateInstance(context.ComponentType) as IContainerComponent;
                // 调用
                component.Load(registry, context);
            }

            return registry;
        }
    }
}
