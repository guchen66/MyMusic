using Music.System.Services.MainSign.HomeSign;
using System.Windows.Controls;

namespace MyMusic.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region 字段

        private Timer _timer;
        private DispatcherTimer timer;
        private readonly IFavorService _favorService;
        private readonly IPlayMusicService _playMusicService;
        private IPlayListService _playListService;
        private IRegionManager _regionManager;
        private IHttpClientService _httpClientService;

        #endregion 字段

        #region 属性

        private bool _isDataLoading;

        public bool IsDataLoading
        {
            get => _isDataLoading ? false : true;
            set => SetProperty(ref _isDataLoading, value);
        }

        /// <summary>
        /// 传递过来的歌单Name
        /// </summary>
        private string _transferName;

        public string TransferName
        {
            get { return _transferName; }
            set { SetProperty<string>(ref _transferName, value); }
        }

        public List<MusicInfoDto> _musicInfos;

        public List<MusicInfoDto> MusicInfos
        {
            get { return _musicInfos ?? (_musicInfos = new List<MusicInfoDto>()); }
            set { SetProperty(ref _musicInfos, value); }
        }

        private ObservableCollection<object> _menus;

        public ObservableCollection<object> Menus
        {
            get { return _menus; }
            set
            {
                _menus = value;
                this.RaisePropertyChanged("Menus");
            }
        }

        /// <summary>
        /// 找到数据之后展示数据，结束转圈圈
        /// </summary>
        private Visibility _dataVisibility;

        public Visibility DataVisibility
        {
            get { return _dataVisibility; }
            set
            {
                _dataVisibility = value;
                this.RaisePropertyChanged("DataVisibility");
            }
        }

        #endregion 属性

        public HomeViewModel(IContainerProvider provider) : base(provider)
        {
            _playMusicService = provider.Resolve<IPlayMusicService>();
            _favorService = provider.Resolve<IFavorService>();
            _playListService = provider.Resolve<IPlayListService>();
            _httpClientService = provider.Resolve<IHttpClientService>();
            LoadedCommand = new DelegateCommand<RoutedEventArgs>(async (x) => await ExecuteLoaded(x));
            LoadedCommand2 = new DelegateCommand(ExecuteLoaded2);
            PrePlayCommand = new DelegateCommand<string>(PrePlayExecute);
            PlayCommand = new DelegateCommand<string>(async (musicName) => await ExecutePlay(musicName));
            FavorCommand = new DelegateCommand<string>(ExecuteFavor);
            FlushCommand = new DelegateCommand(async () => await ExecuteFlush());
        }

        #region 命令

        public ICommand LoadedCommand { get; set; }
        public ICommand LoadedCommand2 { get; set; }
        public ICommand ClickPlayAllCommand { get; set; }
        public ICommand OpenPopupCommand { get; set; }
        public ICommand PrePlayCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand FavorCommand { get; set; }
        public ICommand FlushCommand { get; set; }

        #endregion 命令

        #region 方法

        /// <summary>
        /// 界面初始化
        /// 默认首页界面是QQ音乐的每日粤语推荐
        /// </summary>
        private async Task ExecuteLoaded(RoutedEventArgs e)
        {
            MusicInfos = await _favorService.GetHongKongListAsync();
            if (MusicInfos.Count == 0)
            {
                RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("_404View", UriKind.Relative));
            }

            e.Handled = true;
        }

        private void ExecuteLoaded2()
        {
            //  Logger.Info($"HomeViewModel的Loaded开始执行：{stopwatch.ElapsedMilliseconds}");
            // MusicInfos = await _favorService.GetHongKongListAsync();
            //  Logger.Info($"HomeViewModel的Loaded执行结束：{stopwatch.ElapsedMilliseconds}");
            /* if (MusicInfos.Count==0)
             {
                 RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("_404View", UriKind.Relative));
             }*/
        }

        /// <summary>
        /// 执行收藏，收藏歌曲到收藏界面
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ExecuteFavor(string obj)
        {
            _favorService.AddPlayListToFavor(obj);
        }

        /// <summary>
        /// 刷新当前歌单
        /// </summary>
        private async Task ExecuteFlush()
        {
            await _favorService.FlushAsync();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            //  MusicInfos = await _favorService.GetHongKongListAsync();
            CloseSplash("");
        }

        /// <summary>
        /// 准备播放的歌曲
        /// </summary>
        /// <param name="parameter"></param>
        public async void PrePlayExecute(string playList)
        {
            await Task.Run(() => { });
            //异步加载音乐
            /*await Task.Run(new Action(() =>
            {
                PrePlay();
            }));*/
        }

        /// <summary>
        /// 找到数据之后展示数据，结束转圈圈
        /// </summary>
        /// <param name="state"></param>
        private void CloseSplash(object state)
        {
            IsDataLoading = false;
            DataVisibility = Visibility.Visible;
        }

        public void OnDialogClosed()
        {
            // 在3秒后关闭弹窗
            _timer = new Timer(CloseSplash, null, TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);
        }

        public async Task ExecutePlay(string name)
        {
            await _playMusicService.SingleMusicPlay(name);
        }

        #endregion 方法
    }
}