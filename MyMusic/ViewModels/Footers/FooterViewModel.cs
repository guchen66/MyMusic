
using Music.Shared.Events.EmptySign;
using Music.System.Services.CustomPlaySign;
using Music.System.Services.MainSign.HomeSign;

namespace MyMusic.ViewModels.Footers
{
    public class FooterViewModel : BaseViewModel
    {
        private Timer _timer;
        private DispatcherTimer timer;
        private const string SHOW_LRC_ICON = "pack://application:,,,/Assets/Images/Music.jpg";
        IButtonPlaySingleService _buttonPlaySingleService;
        IPlayMusicService _playMusicService;
        public FooterViewModel(IPlayMusicService playMusicService,IButtonPlaySingleService buttonPlaySingleService, IContainerProvider provider) : base(provider)
        {
            _playMusicService = playMusicService;
            _buttonPlaySingleService = buttonPlaySingleService;
            InitingCommand = new DelegateCommand(ExecuteIniting);
            PlayCommand = new DelegateCommand(async()=>await ExecutePlay());
            StopPlayCommand = new DelegateCommand(ExecuteStop);
            EventAggregator.GetEvent<EmptyViewMusicEvent>().Subscribe(GetMusicName);
            ViewModelManager.Footer = this;
        }


        #region  命令
        public ICommand InitingCommand { get;  set; }
        public ICommand DragMoveCommand { get; set; }
        public ICommand MusicPositionChangedCommand { get; set; }
        public ICommand MusicPositionBeginChangedCommand { get; set; }
        public ICommand CoverClickCommand { get; set; }
        public ICommand MouseMovedCoverCommand { get; set; }
        public ICommand MouseLeftCoverCommand { get; set; }

        public ICommand ClickPreviousMusicCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand StopPlayCommand { get; set; }
        public ICommand ClickNextMusicCommand { get; set; }
        public ICommand VolumeChangedCommand { get; set; }
        #endregion


        #region 属性
        private string _musicName = "LX音乐";
        public string MusicName
        {
            get { return _musicName; }
            set
            {
                _musicName = value;
                RaisePropertyChanged("MusicName");
            }
        }

        private string _musicArtist = "LX音乐";
        public string MusicArtist
        {
            get { return _musicArtist; }
            set
            {
                _musicArtist = value;
                RaisePropertyChanged("MusicArtist");
            }
        }

        private string _musicConnection = " - ";
        public string MusicConnection
        {
            get { return _musicConnection; }
            set
            {
                _musicConnection = value;
                RaisePropertyChanged("MusicConnection");
            }
        }

        private TimeSpan _musicMaxTime = new TimeSpan(1);
        public TimeSpan MusicMaxTime
        {
            get { return _musicMaxTime; }
            set
            {
                _musicMaxTime = value;
                RaisePropertyChanged("MusicMaxTime");
            }
        }

        private TimeSpan _musicNowTime = new TimeSpan(0);
        public TimeSpan MusicNowTime
        {
            get { return _musicNowTime; }
            set
            {
                _musicNowTime = value;
                RaisePropertyChanged("MusicNowTime");
            }
        }


        private Uri _musicSource;
        public Uri MusicSource
        {
            get { return _musicSource; }
            set
            {
                _musicSource = value;
                RaisePropertyChanged("MusicSource");
            }
        }

        private Visibility _playButtonVisibility = Visibility.Visible;
        public Visibility PlayButtonVisibility
        {
            get { return _playButtonVisibility; }
            set
            {
                _playButtonVisibility = value;
                RaisePropertyChanged("PlayButtonVisibility");
            }
        }

        private Visibility _pauseButtonVisibility = Visibility.Hidden;
        public Visibility PauseButtonVisibility
        {
            get { return _pauseButtonVisibility; }
            set
            {
                _pauseButtonVisibility = value;
                RaisePropertyChanged("PauseButtonVisibility");
            }
        }

        private Uri _coverSource = new Uri("/Assets/Images/Music.jpg", UriKind.Relative);
        public Uri CoverSource
        {
            get { return _coverSource; }
            set
            {
                _coverSource = value;
                RaisePropertyChanged("CoverSource");
            }
        }

        private Uri _hiddenCoverSource = new Uri(SHOW_LRC_ICON);
        public Uri HiddenCoverSource
        {
            get { return _hiddenCoverSource; }
            set
            {
                _hiddenCoverSource = value;
                RaisePropertyChanged("HiddenCoverSource");
            }
        }

        private Visibility _hiddenCoverVisibility = Visibility.Hidden;
        public Visibility HiddenCoverVisibility
        {
            get { return _hiddenCoverVisibility; }
            set
            {
                _hiddenCoverVisibility = value;
                RaisePropertyChanged("HiddenCoverVisibility");
            }
        }

        private PointCollection _points = new PointCollection();
        public PointCollection Points
        {
            get { return _points; }
            set
            {
                _points = value;
                RaisePropertyChanged("Points");
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 界面初始化
        /// </summary>
        private void ExecuteIniting()
        {

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
           // Task.Delay(1000);
            EventAggregator.GetEvent<EmptyViewMusicEvent>().Subscribe(GetMusicName);
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        private void GetMusicName(string obj)
        {
            Name = obj;
        }

        /// <summary>
        /// 设置播放状态
        /// </summary>
        public void SetPlayState()
        {
            /* MusicName = PlayerManager.PlayMusic.Name;
             MusicArtist = PlayerManager.PlayMusic.Artists;
             MusicConnection = " - ";*/
            PlayButtonVisibility = Visibility.Hidden;
            PauseButtonVisibility = Visibility.Visible;
        }

        /// <summary>
        /// 设置停止播放状态
        /// </summary>
        public void SetStopState()
        {
            /* MusicName = "微音乐";
             MusicArtist = "微音乐";
             MusicConnection = " - ";*/
            PlayButtonVisibility = Visibility.Visible;
            PauseButtonVisibility = Visibility.Hidden;
           
            //  CoverSource = new Uri("/Resources/DefaultCover.png", UriKind.Relative);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void ExecuteStop()
        {
            IBasePlayService.Pause();
          //  _buttonPlaySingleService.StopPlayAsync();
            PlayButtonVisibility = Visibility.Visible;
            PauseButtonVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// 点击播放
        /// </summary>
        public async Task ExecutePlay()
        {
            var result = Name;
            SetPlayState();
            var playService = MusicSourceFactory.CreatePlayProvider("网易云");
            await playService.PlayListAsync(result);
         //   await _playMusicService.SingleMusicPlay(Name);
          //  _buttonPlaySingleService.PlayListAsync(id);
        }
        #endregion


    }
}
