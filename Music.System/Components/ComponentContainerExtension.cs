namespace Music.System.Components
{
    public static class ComponentContainerExtension
    {
        public static IContainerRegistry RegisterComponent<TComponent>(this IContainerRegistry registry, object options = default)
            where TComponent : class, IContainerComponent, new()
        {
            return registry.RegisterComponent<TComponent, object>(options);
        }

        public static IContainerRegistry RegisterComponent<TComponent, TComponentOptions>(this IContainerRegistry registry, TComponentOptions options = default)
            where TComponent : class, IContainerComponent, new()
        {
            return registry.RegisterComponent(typeof(TComponent), options);
        }

        public static IContainerRegistry RegisterComponent(this IContainerRegistry registry, Type componentType, object options = default)
        {
            var component = Activator.CreateInstance(componentType) as IContainerComponent;
            // 调用
            component.Load(registry);

            return registry;
        }
    }
}