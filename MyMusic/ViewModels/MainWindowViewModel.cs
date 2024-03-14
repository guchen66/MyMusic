
using Music.Shared.Dtos;

namespace MyMusic.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region 字段
        public DispatcherTimer _timer;
        // private Timer _timer;
        public MainArgs AppData { get; set; } = MainArgs.Instance;
        IPlayListService _playListService;
        public SimpleClient<MusicInfo> db = new SimpleClient<MusicInfo>();
        public DataRepository<PlayListInfo> db2= new DataRepository<PlayListInfo>();
        #endregion



        #region 属性

        public ObservableCollection<PlayListInfo> _playListInputDtos;
        public ObservableCollection<PlayListInfo> PlayListInputDtos
        {
            get => _playListInputDtos ?? (_playListInputDtos = new ObservableCollection<PlayListInfo>());
            set => SetProperty(ref value, _playListInputDtos);
        }
        //AsideMenus
        public ObservableCollection<AsideMenuDto> _asideMenus;
        public ObservableCollection<AsideMenuDto> AsideMenus
        {
            get => _asideMenus ?? (_asideMenus = new ObservableCollection<AsideMenuDto>());
            set => SetProperty(ref value,_asideMenus);
        }
        #endregion

        public MainWindowViewModel(IPlayListService playListService, IContainerProvider provider):base(provider)
        {
 
            _playListService = playListService;
            RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(EmptyPlayListView));
            RegionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            RegionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
            SplashScreenManager.CloseSplashScreen();

            db2.GetList().ForEach(x => PlayListInputDtos.Add(x));
            NavigateCommand = new DelegateCommand<MenuList>(ExecuteOpenView);
            InitintCommand=new DelegateCommand(ExecuteIniting);
            CreatePlayListCommand = new DelegateCommand(ExecuteCreatePlayListView);
            OpenPlayListCommand = new DelegateCommand<long?>(ExecuteOpenPlayList);
          
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
        public ICommand InitintCommand { get; set; }
        public ICommand CreatePlayListCommand { get; set; }
        public ICommand OpenPlayListCommand { get; set; }
        public ICommand OpenLyricsCommand { get; set; }
        #endregion

        #region  方法
       
        /// <summary>
        /// 初始化
        /// </summary>
        private void ExecuteIniting()
        {

           
            /* //程序启动加载上一次关闭时SetData的数据状态
             StateHelper.ApplySavedState();
             Thread thread = new Thread(() =>
             {
                 splash = new SplashWindow();
                 splash.ShowDialog();
             });
             thread.SetApartmentState(ApartmentState.STA);

             thread.Start();
             _timer = new Timer(CloseSplash, null, TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);*/
        }
      
      
        /// <summary>
        /// 打开新的空白歌单界面
        /// </summary>
        /// <param name="id"></param>
        public async void ExecuteOpenPlayList(long? id)
        {
            JsonProvider jsonProvider = new JsonProvider();
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
               
            RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative), navigationParameters);

        }

        private void ExecuteOpenView(MenuList paramters)
        {
            Navigate(paramters);
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="navigatePath"></param>
        private void Navigate(MenuList navigatePath)
        {
            if (navigatePath != null)
                RegionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath.NameSpace, arg =>
                {
                    NavigationJournal = arg.Context.NavigationService.Journal;
                });
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
                    PlayListInputDto playListInfo = new PlayListInputDto();
                    playListInfo.Id= YitIdHelper.NextId();
                    playListInfo.PlayListName = value;
                    await  _playListService.CreatePlatListAsync(playListInfo);
                    //拿到歌单名称后，实现异步将歌单添加到界面Expander内部
                   await RefreshAsync();
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
        /// 刷新歌单列表
        /// </summary>
        /// <returns></returns>
        public Task RefreshAsync()
        {
            var dataList = db2.GetList();

            PlayListInputDtos = new ObservableCollection<PlayListInfo>();

            if (dataList != null)
            {
                db2.GetList().ForEach(x => PlayListInputDtos.Add(x));
            }
            return Task.CompletedTask;
        }
        #endregion



    }
}
