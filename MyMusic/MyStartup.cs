using MapsterMapper;
using Music.Shared.Entitys.Header;
using Music.SqlSugar.IRepositorys;
using Music.System.Registers;
using MyMusic.ViewModels.Asides;
using MyMusic.Views.Asides;
using Prism.Services.Dialogs;

namespace MyMusic
{
    public class MyStartup
    {
        public static void Register(IContainerRegistry containerRegistry)
        {
            
            containerRegistry.RegisterForNavigation<SetView>(); //设置信息
            containerRegistry.RegisterForNavigation<DownLoadView, DownLoadViewModel>();
            containerRegistry.RegisterForNavigation<RecentView, RecentViewModel>();
            containerRegistry.RegisterForNavigation<AsideView, AsideViewModel>();
            containerRegistry.RegisterForNavigation<HeaderView, HeaderViewModel>();
            containerRegistry.RegisterForNavigation<FooterView, FooterViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<FavorView, FavorViewModel>();
            containerRegistry.RegisterForNavigation<EmptyPlayListView, EmptyPlayListViewModel>();
            containerRegistry.RegisterForNavigation<EditPlayListView, EditPlayListViewModel>();
            containerRegistry.RegisterForNavigation<_404View, _404ViewModel>();
            containerRegistry.RegisterForNavigation<_500View, _500ViewModel>();
            ///注册对话框弹窗
            containerRegistry.RegisterDialog<LoadingDialog, LoadingDialogViewModel>();
            containerRegistry.RegisterDialog<AddPlayListDialog, AddPlayListDialogViewModel>();
            containerRegistry.RegisterDialog<DeletePlayListDialog, DeletePlayListDialogViewModel>();
            containerRegistry.RegisterDialog<ReNamePlayListDialog, ReNamePlayListDialogViewModel>();
            containerRegistry.RegisterDialog<DownLoadDialog, DownLoadDialogViewModel>();
            //   containerRegistry.RegisterDialog<PopupInputContentDialog, PopupInputContentDialogViewModel>();
            // 注册 ILog 日志接口和 NLogLogger 实现类
            containerRegistry.Register<IPlayListService, PlayListService>();
            //改用特性注册
            /*   containerRegistry.Register<ILogger, DefaultLogger>();
               containerRegistry.Register<ILoginService, LoginService>();
               containerRegistry.Register<IFavorService, FavorService>();
               containerRegistry.Register<IStateService, StateService>();
               containerRegistry.Register<IPlayListService, PlayListService>();
               containerRegistry.Register<IButtonPlaySingleService, ButtonPlaySingleService>();*/
            //注册单例
            //    containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            //注入数据库仓储
            containerRegistry.Register<IAsideMenuRepository, AsideMenuRepository>();
            containerRegistry.Register<IAsideCreateControlRepository,AsideCreateControlRepository>();
            containerRegistry.Register<IHeaderMusicSourceRepository, HeaderMusicSourceRepository>();
            // containerRegistry.RegisterScoped<I>
            //国际化注册
            containerRegistry.Register<II18NService, I18NService>();

          //  containerRegistry.Register<PlayListSettings>();
            containerRegistry.Register<IMapper, Mapper>();
            //注册服务
            containerRegistry.RegisterScoped<IHttpClientService, HttpClientService>();
            containerRegistry.RegisterScoped<IAsideMenuService, AsideMenuService>();
            containerRegistry.RegisterScoped<IAsideCreateControlService,AsideCreateControlService>();
            containerRegistry.RegisterScoped<IHeaderMusicSourceService, HeaderMusicSourceService>();


           

            //注册Dtos
            containerRegistry.RegisterScoped<IRegister, AsideMenuRegister>();
           // containerRegistry.RegisterInstance(IRegister,AsideCreateControllerRegister);
        }
    
        public static void AddSqlSugar()
        {
            SugarIocServices.AddSqlSugar(
                new IocConfig()
                {
                    ConnectionString = GetConnectionObject(),
                    DbType = IocDbType.SqlServer,
                   // IsAutoCloseConnection = true
                }
            );

            //创建数据库
            if (GeneratorDataProvider.IsGenerated)
            {
                DbScoped.SugarScope.DbMaintenance.CreateDatabase();

                ////创建表
                DbScoped.SugarScope.CodeFirst.InitTables(
                    typeof(AsideControlView),
                    typeof(AsideCreateController),
                    typeof(MusicInfo),
                    typeof(PlayListUiInfo),
                    typeof(PlayListInfo),
                    typeof(AsideMenu),typeof(MusicSourceInfo),typeof(SysUser),typeof(FavorPlayListInfo)
                );
            }
            //生成种子数据
            if (GeneratorDataProvider.IsSeedData)
            {
                // AddSeedData();
            }
        }

        /// <summary>
        /// 使用NewLife读取Json
        /// MultipleActiveResultSets=True; 允许打开多个结果集
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionObject()
        {
            string result = "";
            var provider = new JsonConfigProvider() { FileName = "AppConfig.json" };
            result = provider.GetSection("SqlConnection:ConnectionMMsql").Value;
            return result;
        }
    }

    public class RegistExtension
    {
        IContainerRegistry _containerRegistry;

        public RegistExtension(IContainerRegistry containerRegistry)
        {
            _containerRegistry = containerRegistry;
        }

        public void RegisterScannedTypes()
        {
            var assemblies = new[] { "Music.SqlSugar", "Music.System", "MyMusic", };
            foreach (var assemblyName in assemblies)
            {
                //1、先是加载程序集
                var assembly = Assembly.Load(assemblyName);
                //2、找到类中标注了特性ScanningAttribute的所有类
                var typesToRegister = assembly
                    .GetTypes()
                    .Where(type => Attribute.IsDefined(type, typeof(ScanningAttribute)));

                // 3、创建一个字典，键是接口类型，值是实现类的类型 因为标注了特性ScanningAttribute的类只有一个接口
                var typeInterfaceDicts = typesToRegister.ToDictionary(
                    type => type.GetInterfaces()[0],
                    type => type);

                foreach (var typeInterDict in typeInterfaceDicts)
                {
                    //4、找到关于类中特性的RegisterType
                    var attribute = typeInterDict.Value.GetCustomAttribute<ScanningAttribute>(false);
                    if (attribute != null)
                    {
                        switch (attribute.RegisterType)
                        {
                            case "Register":
                                Register(typeInterDict.Key, typeInterDict.Value);
                                break;
                            case "RegisterScoped":
                                RegisterScoped(typeInterDict.Key, typeInterDict.Value);
                                break;
                            case "RegisterSingleton":
                                RegisterScoped(typeInterDict.Key, typeInterDict.Value);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 特性注册标注Register
        /// </summary>
        private void Register(Type interfaceType, Type implementationType)
        {
            _containerRegistry.Register(interfaceType, implementationType);
        }

        /// <summary>
        /// 特性注册标注RegisterScoped
        /// </summary>
        private void RegisterScoped(Type interfaceType, Type implementationType)
        {
            _containerRegistry.RegisterScoped(interfaceType, implementationType);
        }
    }

    public class RegisterDemo
    {
        private static IContainerRegistry _containerRegistry;

        public RegisterDemo(IContainerRegistry containerRegistry)
        {
            _containerRegistry = containerRegistry;
        }

        public static void GetRegister()
        {
            MethodInfo methodInfo = typeof(IContainerRegistry).GetMethod(
                "RegisterSingleton",
                new Type[] { typeof(Type), typeof(Type) }
            );
            Func<IContainerRegistry, object, object, object> Register =
                MagicMethod<IContainerRegistry>(methodInfo);

            Register(_containerRegistry, typeof(IAsideMenuService), typeof(AsideMenuService));
        }

        static Func<T, object, object, object> MagicMethod<T>(MethodInfo methodInfo)
            where T : class
        {
            MethodInfo genericHelper = typeof(RegisterDemo).GetMethod(
                "MagicMethodHelper",
                BindingFlags.Static | BindingFlags.NonPublic
            );
            MethodInfo constructHelper = genericHelper.MakeGenericMethod(
                typeof(T),
                methodInfo.GetParameters()[0].ParameterType,
                methodInfo.GetParameters()[1].ParameterType,
                methodInfo.ReturnType
            );

            object ret = constructHelper.Invoke(null, new object[] { methodInfo });
            return (Func<T, object, object, object>)ret;
        }

        static Func<TTarget, object, object, object> MagicMethodHelper<
            TTarget,
            TParam1,
            TParam2,
            TReturn
        >(MethodInfo method)
            where TTarget : class, new()
        {
            // 将方法转为委托
            Func<TTarget, TParam1, TParam2, TReturn> func =
                (Func<TTarget, TParam1, TParam2, TReturn>)
                    Delegate.CreateDelegate(
                        typeof(Func<TTarget, TParam1, TParam2, TReturn>),
                        method
                    );

            // 创建一个更弱的委托调用上面的委托
            Func<TTarget, object, object, object> ret = (
                TTarget target,
                object param1,
                object param2
            ) => func(target, (TParam1)param1, (TParam2)param2);
            return ret;
        }
    }

}
