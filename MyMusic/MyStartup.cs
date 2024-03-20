
namespace MyMusic
{
    public class MyStartup
    {
        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SetView>();  //设置信息
            containerRegistry.RegisterForNavigation<DownLoadView, DownLoadViewModel>();
            containerRegistry.RegisterForNavigation<RecentView, RecentViewModel>();

            containerRegistry.RegisterForNavigation<HeaderView, HeaderViewModel>();
            containerRegistry.RegisterForNavigation<FooterView, FooterViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<FavorView, FavorViewModel>();
            containerRegistry.RegisterForNavigation<EmptyPlayListView, EmptyPlayListViewModel>();
            containerRegistry.RegisterForNavigation<_404View, _404ViewModel>();
            containerRegistry.RegisterForNavigation<_500View, _500ViewModel>();
            ///注册对话框弹窗
            containerRegistry.RegisterDialog<LoadingDialog, LoadingDialogViewModel>();
            containerRegistry.RegisterDialog<AddPlayListDialog, AddPlayListDialogViewModel>();
            containerRegistry.RegisterDialog<DeletePlayListDialog, DeletePlayListDialogViewModel>();
            containerRegistry.RegisterDialog<ReNamePlayListDialog, ReNamePlayListDialogViewModel>();
         //   containerRegistry.RegisterDialog<PopupInputContentDialog, PopupInputContentDialogViewModel>();
            // 注册 ILog 日志接口和 NLogLogger 实现类
            containerRegistry.Register<ILogger, DefaultLogger>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IFavorService, FavorService>();
            containerRegistry.Register<IStateService, StateService>();
            containerRegistry.Register<IPlayListService, PlayListService>();
            containerRegistry.Register<IButtonPlaySingleService, ButtonPlaySingleService>();
            //注册单例
        //    containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            //注入数据库仓储
         //   containerRegistry.RegisterScoped(typeof(DataRepository<MusicInfo>));
            containerRegistry.RegisterScoped<IAsideMenuRepository, AsideMenuRepository>();
            containerRegistry.RegisterScoped<IAsideCreateControlRepository, AsideCreateControlRepository>();
            // containerRegistry.RegisterScoped<I>
            //国际化注册
            containerRegistry.Register<II18NService, I18NService>();

            containerRegistry.Register<PlayListSettings>();
            //注册服务
            containerRegistry.RegisterSingleton<IHttpClientService, HttpClientService>();
            containerRegistry.RegisterSingleton<IAsideMenuService, AsideMenuService>();
            containerRegistry.RegisterSingleton<IAsideCreateControlService, AsideCreateControlService>();

            //注册Dtos
            containerRegistry.RegisterScoped<IRegister, AsideMenuRegister>();
        }

        public static void AddSqlSugar()
        {
            SugarIocServices.AddSqlSugar(new IocConfig()
            {
                ConnectionString = GetJsonData(),
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });

            //创建数据库
            if (GeneratorDataProvider.IsGenerated)
            {
                DbScoped.SugarScope.DbMaintenance.CreateDatabase();

                ////创建表
                DbScoped.SugarScope.CodeFirst.InitTables(
                    typeof(AsideControlView), typeof(AsideCreateControl), typeof(MusicInfo), typeof(PlayListUiInfo), typeof(PlayListInfo), typeof(AsideMenu));
            }
            //生成种子数据
            if (GeneratorDataProvider.IsSeedData)
            {
                // AddSeedData();
            }
           
        }
        /// <summary>
        /// 使用NewLife读取Json
        /// </summary>
        /// <returns></returns>
        public static string GetJsonData()
        {
            string result = "";
            var provider = new JsonConfigProvider()
            {
                FileName = "AppConfig.json"
            };
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

        Dictionary<string, Action<Type, Type>> Dicts = new Dictionary<string, Action<Type, Type>>();
        /// <summary>
        /// 动态注册仓储和数据服务
        /// </summary>
        public void RegisterScannedTypes()
        {
            var assemblies = new[]
            {
                // 这里添加您的程序集名称，不包括 .dll 扩展名
                "MyMusic",
                "Music.Core",
                "Music.System",
                "Music.SqlSugar"
            };

            foreach (var assemblyName in assemblies)
            {
                var assembly = Assembly.Load(assemblyName);
                FindScanningAttributedClasses(assembly);
            }

        }
        private void FindScanningAttributedClasses(Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                 .Where(type => Attribute.IsDefined(type, typeof(ScanningAttribute)));

            foreach (var type in typesToRegister)
            {
                Type[] interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    var scanAttribute = (ScanningAttribute)type.GetCustomAttribute(typeof(ScanningAttribute));
                    if (inter != null)
                    {
                        switch (scanAttribute.RegisterType)
                        {
                            case "Scpoed": _containerRegistry.RegisterScoped(inter, type); break;
                            case "Singleton": _containerRegistry.RegisterSingleton(inter, type); break;
                            default:
                                break;
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 反射太影响性能了，暂时搁浅
        /// </summary>
        public void RegisterAllTypes()
        {
            var assembly = Assembly.LoadFrom("Music.SqlSugar.dll");
            var assembly2 = Assembly.LoadFrom("Music.System.dll");
            var assembly3 = Assembly.LoadFrom("MyMusic.dll");
            var typesToRegister = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(ScanningAttribute)));
            var typesToRegister2 = assembly2.GetTypes()
               .Where(type => Attribute.IsDefined(type, typeof(ScanningAttribute)));
            var typesToRegister3 = assembly3.GetTypes()
               .Where(type => Attribute.IsDefined(type, typeof(ScanningAttribute)));

            foreach (var type in typesToRegister)
            {
                Type[] interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    _containerRegistry.RegisterScoped(inter, type);
                }
            }
            foreach (var type2 in typesToRegister2)
            {
                Type[] interfaces = type2.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    _containerRegistry.RegisterSingleton(inter, type2);
                }
            }
            foreach (var type3 in typesToRegister3)
            {
                Type[] interfaces = type3.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    _containerRegistry.Register(inter, type3);
                }
            }
        }
 /*       public void RegisterAllTypes()
        {
          
        }*/

        private void RegisterTypesFromAssembly<TAttribute>(string assemblyPath, Action<Type, Type> registerAction)
            where TAttribute : Attribute
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var typesToRegister = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(TAttribute)));

            foreach (var type in typesToRegister)
            {
                Type[] interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    registerAction(inter, type);
                }
            }
        }
        

        public void AddRegister1(Type from,Type to)
        {
            _containerRegistry.RegisterScoped(from,to);
        }
        public void AddRegister2(Type from, Type to)
        {
            _containerRegistry.RegisterSingleton(from,to);
        }
        public void AddRegister3(Type from, Type to)
        {
            _containerRegistry.Register(from,to);
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
            MethodInfo methodInfo = typeof(IContainerRegistry).GetMethod("RegisterSingleton", new Type[] { typeof(Type), typeof(Type) });
            Func<IContainerRegistry, object, object, object> Register = MagicMethod<IContainerRegistry>(methodInfo);

            Register(_containerRegistry, typeof(IAsideMenuService), typeof(AsideMenuService));
        }

        static Func<T, object, object, object> MagicMethod<T>(MethodInfo methodInfo) where T : class
        {
            MethodInfo genericHelper = typeof(RegisterDemo).GetMethod("MagicMethodHelper", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo constructHelper = genericHelper.MakeGenericMethod(typeof(T), methodInfo.GetParameters()[0].ParameterType, methodInfo.GetParameters()[1].ParameterType, methodInfo.ReturnType);

            object ret = constructHelper.Invoke(null, new object[] { methodInfo });
            return (Func<T, object, object, object>)ret;
        }

        static Func<TTarget, object, object, object> MagicMethodHelper<TTarget, TParam1, TParam2, TReturn>(MethodInfo method)
            where TTarget : class , new()
        {
            // 将方法转为委托
            Func<TTarget, TParam1, TParam2, TReturn> func = (Func<TTarget, TParam1, TParam2, TReturn>)Delegate.CreateDelegate
                (typeof(Func<TTarget, TParam1, TParam2, TReturn>), method);

            // 创建一个更弱的委托调用上面的委托
            Func<TTarget, object, object, object> ret = (TTarget target, object param1, object param2) => func(target, (TParam1)param1, (TParam2)param2);
            return ret;
        }
    }

}
