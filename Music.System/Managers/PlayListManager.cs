/*using Music.System.Services.PlayLists;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Ioc;
using System.Windows;
using System.Runtime.Serialization;
using Prism.Regions;
using Music.Shared.Consts;
using Music.Shared.Globals.MainSign;

namespace Music.System.Managers;

public class PlayListManager
{

    IRegionNavigationJournal _navigationJournal;//导航日志，上一页，下一页
    IRegionManager _regionManager;//区域管理
    IDialogService _dialogService;
    IPlayListService _playListService;
    IEventAggregator _eventAggregator;
    public ICommand ReNamePlayListItemCommand { get; set; }
    public ICommand DeletePlayListItemCommand { get; set; }

    public PlayListManager() 
    {
        // 使用Prism服务定位器注入
        _navigationJournal= ContainerLocator.Container.Resolve<IRegionNavigationJournal>();
        _regionManager =ContainerLocator.Container.Resolve<IRegionManager>();
        _dialogService = ContainerLocator.Container.Resolve<IDialogService>();
        _playListService = ContainerLocator.Container.Resolve<IPlayListService>();
        _eventAggregator = ContainerLocator.Container.Resolve<IEventAggregator>();
    }    

    /// <summary>
    /// 跟据歌单名称对应具体歌单内容
    /// </summary>
    public void GetPlayListView(string name)
    {
        if (string.IsNullOrEmpty(name)) return;

        if (name != null)
        {
           //var viewModel=new Empty
        }
    }

    private void ExecuteOpenView(string paramters)
    {
        Navigate(paramters);
    }

    /// <summary>
    /// 导航
    /// </summary>
    /// <param name="navigatePath"></param>
    private void Navigate(string navigatePath)
    {
        if (navigatePath != null)
            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, arg =>
            {
                _navigationJournal = arg.Context.NavigationService.Journal;
            });
    }

    public static Object CreateInstance(Type type,params Object[] args)
    {
           return new PlayListManager();
    }
}

public static class New<T>
{
  //  public static readonly Func<T> Instance = Creator();

    *//*static Func<T> Creator()
    {
        *//*Type t = typeof(T);
        if (t == typeof(string))
            return Expression.Lambda<Func<T>>(Expression.Constant(string.Empty)).Compile();

        if (t.HasDefaultConstructor())
            return Expression.Lambda<Func<T>>(Expression.New(t)).Compile();

        return () => (T)FormatterServices.GetUninitializedObject(t);*//*
    }*//*
}

*//*public static bool HasDefaultConstructor(this Type t)
{
    return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
}*/
