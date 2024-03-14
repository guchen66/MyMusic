
using ReactiveUI;
using StartupEventArgs = System.Windows.StartupEventArgs;

namespace MyMusic;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    //初始化Root目录状态
    public RootArgs RootArgs { get; set; } = RootArgs.Instance;
    protected override async void OnStartup(StartupEventArgs e)
    {
        //CodeFirstUtils.GreateDbAndTableByCode();
        MyStartup.AddSqlSugar();
        base.OnStartup(e);

        
        //程序启动加载上一次关闭时保存的数据状态
        await StateManager.SaveLocalStateData();
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
        MyStartup.Register(containerRegistry);

        // TextDemo textDemo = new TextDemo(containerRegistry);
        // textDemo.RegisterService().RegisterRepository().RegisterForNavigation().RegisterDialog();
     
    }

}
