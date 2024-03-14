
namespace MyMusic.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        #region 字段

        private Timer _timer;
        private DispatcherTimer timer;
        private readonly IFavorService _favorService;
        IPlayListService _playListService;
        IRegionManager _regionManager;
        IHttpClientService _httpClientService;
        #endregion

        #region  属性
        /// <summary>
        /// 传递过来的歌单Name
        /// </summary>
        private string _transferName;

        public string TransferName
        {
            get { return _transferName; }
            set { SetProperty<string>(ref _transferName, value); }
        }

        public List<HongKongDto> _musicInfos;
        public List<HongKongDto> MusicInfos
        {
            get { return _musicInfos; }
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
        /// 显示转圈圈
        /// </summary>
        private Visibility _searchProgressVisibility;
        public Visibility SearchProgressVisibility
        {
            get { return _searchProgressVisibility; }
            set
            {
                _searchProgressVisibility = value;
                this.RaisePropertyChanged("SearchProgressVisibility");
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

        #endregion

        public HomeViewModel(IContainerProvider provider):base(provider)
        {
            SearchProgressVisibility = Visibility.Visible;
            DataVisibility = Visibility.Hidden;
            _favorService = provider.Resolve<IFavorService>();
            _playListService = provider.Resolve<IPlayListService>();
            _httpClientService = provider.Resolve<IHttpClientService>();
            // InitCommand=DialogHost.OpenDialogCommand;
            InitingCommand = new DelegateCommand(ExecuteIniting);
            PrePlayCommand = new DelegateCommand<string>(PrePlayExecute);
            PlayCommand = new DelegateCommand<string>(ExecutePlay);
        }


        #region  命令

        public ICommand InitingCommand { get; set; }
        public ICommand ClickPlayAllCommand { get; set; }
        public ICommand OpenPopupCommand { get; set; }
        public ICommand PrePlayCommand { get; set; }
        public ICommand PlayCommand {  get; set; }
        #endregion


        #region  方法

        /// <summary>
        /// 界面初始化
        /// 默认首页界面是QQ音乐的每日粤语推荐
        /// </summary>
        private async void ExecuteIniting()
        {
            MusicInfos = await _favorService.GetHongKongListAsync();
            /*timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();*/
            CloseSplash("");
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            MusicInfos = await _favorService.GetHongKongListAsync();
            CloseSplash("");
        }

        /// <summary>
        /// 准备播放的歌曲
        /// </summary>
        /// <param name="parameter"></param>
        public async void PrePlayExecute(string playList)
        {
            //异步加载音乐
            /*await Task.Run(new Action(() =>
            {
                PrePlay();
            }));*/
        }

      /*  public string GetMusicUrl()
        {
            string url = "https://tqlcode.com/page/music/api.php";
            byte[] commit = Encoding.UTF8.GetBytes($"types=url&id={Id}&source=netease");
            byte[] data = _httpClientService.PostAsync(url, commit);
            JObject json = JObject.Parse(Encoding.UTF8.GetString(data));
            return json["url"].ToString();
        }*/

        /// <summary>
        /// 找到数据之后展示数据，结束转圈圈
        /// </summary>
        /// <param name="state"></param>
        private void CloseSplash(object state)
        {
            SearchProgressVisibility = Visibility.Collapsed;
            DataVisibility = Visibility.Visible;
        }

        public void OnDialogClosed()
        {
            // 在3秒后关闭弹窗
            _timer = new Timer(CloseSplash, null, TimeSpan.FromSeconds(3), Timeout.InfiniteTimeSpan);
        }

        public void ExecutePlay(string name)
        {

        }
        #endregion
    }
}
