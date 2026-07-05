using IT.Tangdao.Core.Abstractions.FileAccessor;
using IT.Tangdao.Core.Abstractions.Loggers;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Music.Shared.Entitys.Header;
using Music.SqlSugar.IRepositorys;
using Music.SqlSugar.Repositorys;
using Music.SqlSugar.Utils;
using Music.System.Registers;
using MyMusic.ViewModels.Asides;
using MyMusic.Views.Asides;
using Prism.Services.Dialogs;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MyMusic
{
    public class MyStartup
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static ITangdaoLogger Logger = TangdaoLogger.Get<MyStartup>();

        public static void Register(IContainerRegistry containerRegistry)
        {
            //注册导航
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
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
            containerRegistry.RegisterForNavigation<RootView, RootViewModel>();

            containerRegistry.RegisterForNavigation<SplashView, SplashViewModel>();
            containerRegistry.RegisterForNavigation<MainPageView, MainPageViewModel>();

            //注册对话框弹窗
            containerRegistry.RegisterDialog<LoadingDialog, LoadingDialogViewModel>();
            containerRegistry.RegisterDialog<AddPlayListDialog, AddPlayListDialogViewModel>();
            containerRegistry.RegisterDialog<DeletePlayListDialog, DeletePlayListDialogViewModel>();
            containerRegistry.RegisterDialog<ReNamePlayListDialog, ReNamePlayListDialogViewModel>();
            containerRegistry.RegisterDialog<DownLoadDialog, DownLoadDialogViewModel>();

            containerRegistry.Register<IPlayListService, PlayListService>();

            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();

            //注入数据库仓储
            containerRegistry.Register<IAsideMenuRepository, AsideMenuRepository>();
            containerRegistry.Register<IAsideCreateControlRepository, AsideCreateControlRepository>();
            containerRegistry.Register<IHeaderMusicSourceRepository, HeaderMusicSourceRepository>();

            //国际化注册
            containerRegistry.Register<II18NService, I18NService>();

            containerRegistry.Register<IMapper, Mapper>();
            //注册服务
            containerRegistry.RegisterScoped<IHttpClientService, HttpClientService>();
            containerRegistry.RegisterSingleton<IAsideMenuService, AsideMenuService>();
            containerRegistry.RegisterScoped<IAsideCreateControlService, AsideCreateControlService>();
            containerRegistry.RegisterScoped<IHeaderMusicSourceService, HeaderMusicSourceService>();
            containerRegistry.RegisterScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            //Configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.Build();  // 返回 IConfigurationRoot
            //注册Dtos
            containerRegistry.RegisterScoped<IRegister, AsideMenuRegister>();

            //注册IT.Tangdao
            containerRegistry.RegisterSingleton<IContentAccess, ContentAccess>();
            containerRegistry.RegisterSingleton<ITangdaoLogger, TangdaoLogger>();
        }

        public static void AddSqlSugar()
        {
            SugarIocServices.AddSqlSugar(
                new IocConfig()
                {
                    ConnectionString = JsonComponent.GetJsonObjectValue("AppConfig.json", "SqlConnection:ConnectionMMsqlExtension"),
                    DbType = IocDbType.SqlServer,
                    IsAutoCloseConnection = false
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
                    typeof(AsideMenu), typeof(MusicSourceInfo), typeof(SysUser), typeof(FavorPlayListInfo)
                );
            }
            //生成种子数据
            if (GeneratorDataProvider.IsSeedData)
            {
                // AddSeedData();
            }
        }
    }

    public class RegisterDemo1
    {
        private static IContainerRegistry _containerRegistry;

        public RegisterDemo1(IContainerRegistry containerRegistry)
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

        private static Func<T, object, object, object> MagicMethod<T>(MethodInfo methodInfo)
            where T : class
        {
            MethodInfo genericHelper = typeof(RegisterDemo1).GetMethod(
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

        private static Func<TTarget, object, object, object> MagicMethodHelper<
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