namespace Music.MD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ITangdaoLogger Logger = TangdaoLogger.Get(typeof(App));

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            var loginView = Container.Resolve<LoginWindow>();
            loginView.Topmost = true;
            loginView.Activate();
            var result = loginView.ShowDialog();
            if (result == false)
            {
                Application.Current?.Shutdown();
            }
            else
            {
                base.InitializeShell(shell);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            LogPathConfig.SetRoot($@"{Location.Logger}");

            MyStartup.SetLanguage();
            MyStartup.AddSqlSugar();
            MyStartup.SetMapster(containerRegistry);
            containerRegistry.RegisterSingleton<IReadService, ReadService>();
            containerRegistry.RegisterScoped<ILoginRepository, LoginRepository>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AccountModule>(InitializationMode.WhenAvailable);
            moduleCatalog.AddModule<TestModule>(InitializationMode.WhenAvailable);
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}