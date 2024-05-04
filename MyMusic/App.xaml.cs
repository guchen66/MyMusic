
using Music.System.Components;
using Prism.Ioc;

using StartupEventArgs = System.Windows.StartupEventArgs;

namespace MyMusic;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    Mutex mutex;
    //初始化Root目录状态
    public RootArgs RootArgs { get; set; } = RootArgs.Instance;
    protected override void OnStartup(StartupEventArgs e)
    {
        mutex = new Mutex(true, "MyMusic");
        if (!mutex.WaitOne(TimeSpan.Zero, true))
        {
            MessageBox.Show("警告，已重复打开软件！", "MyMusic", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(0);
        }
        //注册SqlSugar服务
        MyStartup.AddSqlSugar();
        base.OnStartup(e);
       
        //程序启动加载上一次关闭时保存的数据状态
      //  await StateManager.SaveLocalStateData();
    }

     
    protected override void OnExit(ExitEventArgs e)
    {
        // 程序退出之前保存监控的数据
        MonitorComponent monitor = new MonitorComponent();
        monitor.MonitorCompleted += Monitor_MonitorCompleted;
        base.OnExit(e);
      
    }

    private void Monitor_MonitorCompleted(object sender, RootJsonFileEventArgs e)
    {
       var name= e.DefaultJsonFile.Name;
    }

    protected override Window CreateShell()
    {       
        return Container.Resolve<MainWindow>();
    }

    protected override void InitializeShell(System.Windows.Window shell)
    {
       
        I18NService i18NService = new I18NService();
        i18NService.Init();
        var loginView = Container.Resolve<LoginView>();
        loginView.Topmost = true;
        loginView.Activate();
        var result = loginView.ShowDialog();
        if (result == true)
        {
            base.InitializeShell(shell);
        }
        else
        {
            Application.Current?.Shutdown();
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        // 配置雪花Id算法机器码
        YitIdHelper.SetIdGenerator(new IdGeneratorOptions
        {
            WorkerId = 1,// 取值范围0~63,默认1
           // DataCenterId=1,//数据中心Id
        });

        //注册首页为默认视图
        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(HomeView));
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterComponent<LocalServerComponent>();      //注册本地服务
        containerRegistry.RegisterComponent<SqlsugarComponent>();         //注册Sqlsugar组件
        containerRegistry.RegisterComponent<PlayMusicComponent>();          //注册MahApps组件库
        containerRegistry.RegisterComponent<MapsterComponent>();          //注册Mapster映射
        MyStartup.Register(containerRegistry);

        RegistExtension register = new RegistExtension(containerRegistry);
        register.RegisterScannedTypes();

    
    }

    /// <summary>
    /// 注册Mapster
    /// </summary>
    /// <param name="containerRegistry"></param>
    private void RegisterMapper(IContainerRegistry containerRegistry)
    {

        var config = new TypeAdapterConfig();
        var assembly = Assembly.Load("Music.System");
        config.Scan(assembly);

        // 注册单例实例
        containerRegistry.RegisterInstance(typeof(TypeAdapterConfig), config);

        // 创建并注册 Mapper 实例
        var mapper = new Mapper(config);
        containerRegistry.RegisterInstance(typeof(Mapper), mapper);
    }

}


