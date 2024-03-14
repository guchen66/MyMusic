using Music.System.Mvvm;
using Music.ToolKit;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using MyMusic.Views.Errors;
using MyMusic.ViewModels.Errors;
using Music.System.Services.MainSign.PlayLists;
using Music.System.Services.MainSign.MyFavorSign;
using Music.System.Services.MainSign.SettingSign;
using Music.System.Services.MainSign.HeaderSign;
using Music.System.Services.HttpTools;
using Music.System.Services.MainSign.FooterSign;
using Music.ToolKit.Extensions;
using Music.Shared.Mvvm;
using NewLife.Configuration;
using SqlSugar.IOC;

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

         //   containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            //注入数据库仓储
            //  containerRegistry.RegisterScoped(typeof(DataRepository<MusicInfo>));

            //国际化注册
            containerRegistry.Register<II18NService, I18NService>();

            containerRegistry.Register<PlayListSettings>();
            //注册Http
            containerRegistry.RegisterSingleton<IHttpClientService, HttpClientService>();
        }

        public static void AddSqlSugar()
        {
            SugarIocServices.AddSqlSugar(new IocConfig()
            {
                ConnectionString = GetJsonData(),
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });
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


    public class TextDemo
    {
        IContainerRegistry container;
        public TextDemo(IContainerRegistry containerRegistry)
        {

            container = containerRegistry;

        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
    /*    public TextDemo RegisterService()
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前执行的程序集
            var types = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IPrismService).IsAssignableFrom(myType)); // 找到所有实现了IPrismComponent的非抽象类

            foreach (var type in types)
            {
                container.RegisterScoped(typeof(IPrismService), type); // 将找到的类型注册为IPrismComponent的实现

            }
            return this;
        }*/

        /// <summary>
        /// 注册仓储
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
      /*  public TextDemo RegisterRepository()
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前执行的程序集
            var types = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IPrismRepository).IsAssignableFrom(myType)); // 找到所有实现了IPrismComponent的非抽象类

            foreach (var type in types)
            {
                container.RegisterScoped(typeof(IPrismRepository), type); // 将找到的类型注册为IPrismComponent的实现

            }
            return this;
        }*/

        /// <summary>
        /// 注册视图
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
        public TextDemo RegisterForNavigation<TView, TViewModel>()where TView:UserControl where TViewModel : BaseViewModel
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModelsNamespace = "ViewModels";
            var viewsNamespace = "Views";

            var viewModelTypes = assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains(viewModelsNamespace) && t.Name.EndsWith("ViewModel"))
                .ToList();

            var viewTypes = assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains(viewsNamespace) && t.Name.EndsWith("View"))
                .ToList();
            
            foreach (var viewModelType in viewModelTypes)
            {               
                var viewType = viewTypes.FirstOrDefault(v => v.Name == viewModelType.Name.Replace("ViewModel", "View"));

                if (viewType != null&&viewType.Name=="FooterView")
                {
                    container.RegisterForNavigation(viewType, viewModelType.Name);
                }
            }
            return this;
        }

        /// <summary>
        /// 注册弹窗
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
        public TextDemo RegisterDialog()
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前执行的程序集

            var dialogVMTypes = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IDialogAware).IsAssignableFrom(myType));

            var doalogTypes = assembly.GetTypes()
               .Where(t => t.Namespace != null && t.Name.EndsWith("Dialog"))
               .ToList();
            foreach (var type in dialogVMTypes)
            {
                var viewType = doalogTypes.FirstOrDefault(v => v.Name == type.Name.Replace("ViewModel", "View"));
                if (viewType != null)
                {
                    container.RegisterForNavigation(viewType, type.ToString());
                }

            }
            return this;
        }
    }
}
