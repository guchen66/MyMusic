
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
        IPlayListService _playListService;
        IRegionManager _regionManager;
        IHttpClientService _httpClientService;
        #endregion

        #region  属性

        private bool _isDataLoading;

        public bool IsDataLoading
        {
            get => _isDataLoading?false:true;
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

        public List<HongKongMusicDto> _musicInfos;
        public List<HongKongMusicDto> MusicInfos
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
           // SearchProgressVisibility = Visibility.Visible;
           // DataVisibility = Visibility.Hidden;
            _playMusicService = provider.Resolve<IPlayMusicService>();
            _favorService = provider.Resolve<IFavorService>();
            _playListService = provider.Resolve<IPlayListService>();
            _httpClientService = provider.Resolve<IHttpClientService>();
            InitingCommand = new DelegateCommand(ExecuteIniting);
            PrePlayCommand = new DelegateCommand<string>(PrePlayExecute);
            PlayCommand = new DelegateCommand<string>(ExecutePlay);
            FavorCommand = new DelegateCommand<string>(ExecuteFavor);
          //  BackgroundCodeInit();

            
        }

      

        #region  命令

        public ICommand InitingCommand { get; set; }
        public ICommand ClickPlayAllCommand { get; set; }
        public ICommand OpenPopupCommand { get; set; }
        public ICommand PrePlayCommand { get; set; }
        public ICommand PlayCommand {  get; set; }
        public ICommand FavorCommand {  get; set; }
        #endregion


        #region  方法


        public void BackgroundCodeInit()
        {
            //  MessageBox.Show("初始化");

            Task task = new Task(ExecuteIniting);
            task.Start();
        }
        /// <summary>
        /// 界面初始化
        /// 默认首页界面是QQ音乐的每日粤语推荐
        /// </summary>
        private async void ExecuteIniting()
        {
            MusicInfos = await _favorService.GetHongKongListAsync();

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

        public void ExecutePlay(string name)
        {
            _playMusicService.SingleMusicPlay(name);
        }
        #endregion
    }
}
