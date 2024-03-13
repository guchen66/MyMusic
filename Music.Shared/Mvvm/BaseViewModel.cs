﻿using Music.Shared.Globals.MainSign;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Music.Shared.Mvvm
{
    public class BaseViewModel:BindableBase
    {
        public DispatcherTimer Timer;
        public MainArgs AppData { get; set; } = MainArgs.Instance;
        public IRegionNavigationJournal NavigationJournal;//导航日志，上一页，下一页
        public IRegionManager RegionManager;//区域管理
        public IDialogService DialogService;
      //  IPlayListService _playListService;
        public IEventAggregator EventAggregator;

        public IContainerProvider _containerProvider;

        public BaseViewModel(IContainerProvider provider)
        {
            NavigationJournal=provider.Resolve<IRegionNavigationJournal>();
            RegionManager=provider.Resolve<IRegionManager>();
            DialogService=provider.Resolve<IDialogService>();
            EventAggregator =provider.Resolve<IEventAggregator>();

            Timer = new DispatcherTimer();
        }
    }
}