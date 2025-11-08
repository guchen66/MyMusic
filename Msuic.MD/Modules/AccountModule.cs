namespace Music.MD.Modules
{
    public class AccountModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<LoginAccount>();
        }
    }
}