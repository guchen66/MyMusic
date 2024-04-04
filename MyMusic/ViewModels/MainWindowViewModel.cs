using Prism.Regions;

namespace MyMusic.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region 字段
        public DispatcherTimer _timer;
        public IMapper _mapper;
        // private Timer _timer;
        private readonly IPlayListService _playListService;
        private readonly IAsideMenuService _asideMenuService;
        private readonly IAsideCreateControlService _asideCreateControlService;
        #endregion

        #region 属性

        /// <summary>
        /// 是否新增和删除歌单
        /// </summary>
        private bool _isChanged;

        public bool IsChanged
        {
            get => _isChanged;
            set => SetProperty(ref _isChanged, value);
        }

        /// <summary>
        /// 左侧新建的歌单列表控制器
        /// </summary>
        public ObservableCollection<AsideCreateControllerDto> _asedeCreateplayListDtos;
        public ObservableCollection<AsideCreateControllerDto> AsedeCreateplayListDtos
        {
            get => _asedeCreateplayListDtos ?? (_asedeCreateplayListDtos = new ObservableCollection<AsideCreateControllerDto>());
            set => SetProperty(ref value, _asedeCreateplayListDtos);
        }

        /// <summary>
        /// 左侧的菜单栏列表
        /// </summary>
        public ThreadSafeObservableCollection<AsideMenuDto> _asideMenus;
        public ThreadSafeObservableCollection<AsideMenuDto> AsideMenus
        {
            get => _asideMenus ?? (_asideMenus = new ThreadSafeObservableCollection<AsideMenuDto>());
            set => SetProperty(ref value, _asideMenus);
        }

        #endregion

        public MainWindowViewModel(IMapper mapper, IContainerProvider provider):base(provider)
        {        
            _mapper= mapper;
            _playListService = provider.Resolve<IPlayListService>();
            _asideMenuService= provider.Resolve<IAsideMenuService>();
            _asideCreateControlService = provider.Resolve<IAsideCreateControlService>();
          //  RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(EmptyPlayListView));
            RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
            RegionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            RegionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
         //   SplashScreenManager.CloseSplashScreen();
            
            NavigateCommand = new DelegateCommand<AsideMenuDto>(ExecuteOpenView);
            LoadedCommand = new DelegateCommand(async()=>await ExecuteLoaded());
            CreatePlayListCommand = new DelegateCommand(ExecuteCreatePlayListView);
            OpenPlayListCommand = new DelegateCommand<bool?>(ExecuteOpenPlayList);
          
            OpenLyricsCommand = new DelegateCommand(ExecuteOpenLyrics);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
          //  _timer.Tick += _timer_Tick;
            _timer.Start();

            EventAggregator.GetEvent<RefreshEvent>().Subscribe(async () => await RefreshAsync());

            PlayListSignValue playListSignValue=new PlayListSignValue();
            playListSignValue.CalculationCompleted += PlayListSignValue_CalculationCompleted;
            playListSignValue.CalculateData(null,()=>new PlayListInputDto());


        }

        private void PlayListSignValue_CalculationCompleted(object sender, PlayListArgs e)
        {
            if (e.GetplayListInputDto != null) // 添加防呆判断，确保结果不为null
            {
               // PlayListInputDtos.Add(e.GetplayListInputDto);
            }
        }

      /*  /// <summary>
        /// 实时刷新创建的歌单名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void _timer_Tick(object sender, EventArgs e)
        {
            db2.GetList().ForEach(x=> PlayListInputDtos.Add(x));
            var list = await db2.GetListAsync();
            foreach (var item in list)
            {
                PlayListInputDtos.Add(item);
            }

        }*/


        #region 命令

        public ICommand NavigateCommand { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand CreatePlayListCommand { get; set; }
        public ICommand OpenPlayListCommand { get; set; }
        public ICommand OpenLyricsCommand { get; set; }
        #endregion

        #region  方法
       
        /// <summary>
        /// 初始化加载界面
        /// </summary>
        private async Task ExecuteLoaded()
        {
            var asideMenus = await _asideMenuService.QueryListAsync();
            var asideMenuDtos = _mapper.Map<List<AsideMenuDto>>(asideMenus);

            // 加载左侧菜单选项
            foreach (var menu in asideMenuDtos)
            {
                Dispatcher.CurrentDispatcher.Invoke(() => AsideMenus.Add(menu));
            }

            //加载左侧新建歌单列表控制器选项
            await RefreshAsync();
        }
      
      
        /// <summary>
        /// 打开新的空白歌单界面
        /// </summary>
        /// <param name="id"></param>
        public void ExecuteOpenPlayList(bool? isExistContext)
        {
            if (isExistContext is bool isExist && isExist)
            {
                RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EditPlayListView", UriKind.Relative));
            }
            else
            {
                RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative));
            }
            /*   JsonProvider jsonProvider = new JsonProvider();
               var playListInfo = await _playListService.GetPlayListByIdAsync(id);

               List<MusicSourceDto> list = await jsonProvider.GetMusicSourceDto();
               var SourceName = list.Where(m => m.IsSelected == true).Select(m => m.Name).ToArray();

               // 导航到空白模板界面 并传递导航参数
               //将搜索的内容和三大音乐官网的来源传输过去
               var navigationParameters = new NavigationParameters
               {
                   { "PlaylistName", playListInfo.PlayListName },
                   { "Source",SourceName}
               };
                  */
            //  RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative), navigationParameters);
          /*  bool isExist = isExistContext ?? false;
            string viewName = isExist ? "EditPlayListView" : "EmptyPlayListView";
            RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(viewName, UriKind.Relative));*/

        }

        /// <summary>
        /// 导航，打开指定页面
        /// </summary>
        /// <param name="paramters"></param>
        private void ExecuteOpenView(AsideMenuDto paramters)
        {
            NavigationToView(paramters.NameSpace);
        }


        /// <summary>
        /// 新增歌单
        /// </summary>
        public void ExecuteCreatePlayListView()
        {
            DialogService.ShowDialog("AddPlayListDialog",  async arg =>
            {
                if (arg.Result == ButtonResult.Yes)
                {
                    var value = arg.Parameters.GetValue<string>("PlayListName");      //歌单名称传递过来

                    var s= await _asideCreateControlService.QueryAsync(x=>x.Name==value);
                    if (s!=null) 
                    {
                        MessageBox.Show("歌单已存在");
                        return;
                    }
                    else
                    {
                        AsideCreateControllerDto controllerDto = new AsideCreateControllerDto();
                        controllerDto.Id = YitIdHelper.NextId();
                        controllerDto.PlayListName = value;
                        controllerDto.IsExistContent = false;
                        await _asideCreateControlService.CreatePlatListAsync(controllerDto);
                    //    IsChanged = !IsChanged;
                        //拿到歌单名称后，实现异步将歌单添加到界面Expander内部
                        await RefreshAsync();
                    }
                }
            });
        }

       
        /// <summary>
        /// 打开歌词通知
        /// </summary>
        private void ExecuteOpenLyrics()
        {
            // 创建通知控件
            DialogService.ShowDialog("AppNotification", null, result => { });
        }

        /// <summary>
        /// 刷新新建的歌单列表
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync()
        {
            // 清除现有的歌单列表
            AsedeCreateplayListDtos.Clear();
            //加载左侧新建个单列表控制器选项
            var asideCreateControllers = await _asideCreateControlService.QueryListAsync(x => x.IsDelete == false);
            var asidasideCreateControllerseMenuDtos = _mapper.Map<List<AsideCreateControllerDto>>(asideCreateControllers);
            foreach (var menu in asidasideCreateControllerseMenuDtos)
            {
                Dispatcher.CurrentDispatcher.Invoke(() => AsedeCreateplayListDtos.Add(menu));
            }
        }
        #endregion

    }
  
}
