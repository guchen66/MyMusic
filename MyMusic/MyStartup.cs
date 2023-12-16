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

namespace MyMusic
{
    public class MyStartup
    {
        public void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SetView>();  //设置信息
            containerRegistry.RegisterForNavigation<DownLoadView, DownLoadViewModel>();
            containerRegistry.RegisterForNavigation<RecentView, RecentViewModel>();

            containerRegistry.RegisterForNavigation<HeaderView, HeaderViewModel>();
            containerRegistry.RegisterForNavigation<FooterView, FooterViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<FavorView, FavorViewModel>();
            containerRegistry.RegisterForNavigation<EmptyPlayList, EmptyPlayListViewModel>();
            containerRegistry.RegisterForNavigation<_404View,_404ViewModel>();
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
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();

            //注入数据库仓储
            //  containerRegistry.RegisterScoped(typeof(DataRepository<MusicInfo>));

            //国际化注册
            containerRegistry.Register<II18NService, I18NService>();

            containerRegistry.Register<PlayListSettings>();
            //注册Http
            containerRegistry.RegisterSingleton<IHttpClientService, HttpClientService>();
        }
    }
}
