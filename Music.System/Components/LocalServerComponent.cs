namespace Music.System.Components
{
    public class LocalServerComponent : IContainerComponent
    {
        public void Load(IContainerRegistry registry)
        {
            registry.Register<ILoginService, LoginService>();
            //  registry.RegisterSingleton<Kstopa.Lx.Admin.IServices.ILogger, DefaultLogger>();
        }
    }
}