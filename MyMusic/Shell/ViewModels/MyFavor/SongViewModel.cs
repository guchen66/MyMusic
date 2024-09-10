using Music.Shared.Events.EmptySign;
using Music.Shared.Globals.MainSign.MyFavors;
using Music.System.Services.CustomPlaySign;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyMusic.Shell.ViewModels.MyFavor
{
    public class SongViewModel : BaseViewModel, INavigationAware
    {
        #region 字段

        private Timer _timer;
        private DispatcherTimer timer;
        private readonly IFavorService _favorService;
        private Music.System.Services.Loggers.ILogger _logger;
        IHttpClientService _httpClientService;
        IButtonPlaySingleService _buttonPlaySingleService;
        private static IWavePlayer player = new WaveOutEvent();
        #endregion

        #region  属性

        private string _musicName;

        public string MusicName
        {
            get => _musicName;
            set => SetProperty(ref _musicName, value);
        }

        public IEnumerable<IMusic> _musicInfos;
        public IEnumerable<IMusic> MusicInfos
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

        private bool _isRequestFailed;
        public bool IsRequestFailed
        {
            get { return _isRequestFailed; }
            set { SetProperty(ref _isRequestFailed, value); }
        }

        #endregion

        public SongViewModel(IFavorService favorService, IHttpClientService httpClientService, IButtonPlaySingleService buttonPlaySingleService, IContainerProvider provider) : base(provider)
        {
            _favorService = favorService;
            _httpClientService = httpClientService;
            _buttonPlaySingleService = buttonPlaySingleService;
            _logger = ContainerLocator.Container.Resolve<Music.System.Services.Loggers.ILogger>();
         /*   InitingCommand = new DelegateCommand(ExecuteIniting);
            PrePlayCommand = new DelegateCommand<object>(PrePlayExecute);
            PlayCommand = new DelegateCommand<string>(async (id) => await ExecutePlay(id));
            AddToFavorCommand = new DelegateCommand(ExecuteAddToFavor);
            DownLoadCommand = new DelegateCommand<object>(async (x) => await ExecuteDownLoad(x));

*/
        }

        /// <summary>
        /// 将歌曲添加至收藏
        /// </summary>
        private void ExecuteAddToFavor()
        {

        }

        /// <summary>
        /// 打开下载界面
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task ExecuteDownLoad(object obj)
        {
            // return null;
            DialogParameters paramters = new DialogParameters();
            paramters.Add("selectedItem", obj);
            await Task.Delay(1000);
            DialogService.ShowDialog("DownLoadDialog", paramters, DialogCompleted);
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        /// <param name="result"></param>
        private void DialogCompleted(IDialogResult result)
        {
           
        }


        #region  命令

        public ICommand InitingCommand { get; set; }
        public ICommand ClickPlayAllCommand { get; set; }
        public ICommand OpenPopupCommand { get; set; }
        public ICommand PrePlayCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand AddToFavorCommand { get; set; }
        public ICommand DownLoadCommand { get; set; }
        #endregion


        #region  方法

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

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() => { });
            //  MusicInfos = await _favorService.GetHongKongListAsync();

        }

        /// <summary>
        /// 播放歌曲
        /// </summary>
        /// <param name="name"></param>
        public async Task ExecutePlay(string id)
        {
            //主页面设置播放状态
            ViewModelManager.Footer.SetPlayState();
            //   var SourceName = SourceName;
            var model = MusicInfos.FirstOrDefault(x => x.Id == id);
            EventAggregator.GetEvent<EmptyViewMusicEvent>().Publish(model.Name);
            var playService = MusicSourceFactory.CreatePlayProvider(model.SourceName);
            await playService.PlayListAsync(id);
        }
        public static PlaybackState State
        {
            get { return player.PlaybackState; }
        }

        private static MediaFoundationReader reader = null;
        //  private static SampleAggregator aggregator = null;
        private static SampleChannel channel = null;

        public static string Source { get; set; }
        /// <summary>
        /// 准备播放的歌曲
        /// </summary>
        /// <param name="parameter"></param>
        public void PrePlayExecute(object parameter)
        {

        }

        List<int> Count = new List<int>();

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                //  string decodeStr = WebUtility.UrlDecode(DefaultPlayListName);
                //获取搜索框信息 获取音乐来源
                var TransferName = navigationContext.Parameters["PlaylistName"] as string;
                var source = navigationContext.Parameters["SourceName"] as string[];
                var tasks = new List<Task<List<IMusic>>>();

                if (source == null || TransferName == null)
                {
                    return;
                }
                foreach (var item in source)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("_404View", UriKind.Relative));
                        continue;
                    }

                    Task<List<IMusic>> task = GetMusicDataAsync(item, TransferName);
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);
                //   MusicSourceArgs
                List<IMusic> musicInfos = new List<IMusic>();
                foreach (var task in tasks)
                {
                    musicInfos.AddRange(task.Result);
                }

                MusicInfos = musicInfos;
            }
            catch (Exception)
            {
                RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("_404View", UriKind.Relative));
                throw;
            }
        }
        private async Task<List<IMusic>> GetMusicDataAsync(string source, string content)
        {
            var musicInfos = new List<IMusic>();
            if (source == "网易云音乐")
            {

                string url = "https://tqlcode.com/page/music/api.php";
                byte[] commit = Encoding.UTF8.GetBytes($"types=search&count=10&source=netease&pages=1&name={content}");
                HttpClientDto clientDto = new HttpClientDto();
                byte[] data = await _httpClientService.PostAsync(url, commit, clientDto);
                string s = Encoding.UTF8.GetString(data);
                var infos = JsonConvert.DeserializeObject<NeteaseMusic[]>(Encoding.UTF8.GetString(data));
                // 根据歌单名称加载对应的歌曲列表数据
                musicInfos.AddRange(infos);

            }
            else if (source == "酷狗音乐")
            {
                string url = "https://tqlcode.com/page/music/api.php";
                byte[] commit = Encoding.UTF8.GetBytes($"types=search&count=20&source=kugou&pages=1&name={content}");
                HttpClientDto clientDto = new HttpClientDto();
                byte[] data = await _httpClientService.PostAsync(url, commit, clientDto);
                KugouMusic[] infos = JsonConvert.DeserializeObject<KugouMusic[]>(Encoding.UTF8.GetString(data));
                musicInfos.AddRange(infos);

            }
            else if (source == "QQ音乐")
            {
                try
                {
                    string url = $"https://api.qq.jsososo.com/search?key={content}&pageNo=1&pageSize=10";
                    HttpClientDto clientDto = new HttpClientDto();
                    byte[] data = await _httpClientService.GetAsync(url, clientDto);
                    TencentMusic[] infos = JsonConvert.DeserializeObject<TencentMusic[]>(JObject.Parse(Encoding.UTF8.GetString(data))?["data"]?["list"].ToString());
                    musicInfos.AddRange(infos);

                }
                catch (Exception)
                {

                }

            }

            return musicInfos;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // 在这里可以根据传递的参数（如歌单名称）判断是否是目标导航
            // 如果是同一个歌单名称，则返回true，表示当前页面可以处理该导航
            // 如果不是同一个歌单名称，则返回false，表示需要重新创建新的页面
            var TransferName = navigationContext.Parameters["PlaylistName"] as string;
            return true; // 假设当前页面只能处理名称为"001"的歌单

        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            var TransferName = navigationContext.Parameters["PlaylistName"] as string;
        }

        // 加载歌单对应的歌曲列表数据的逻辑
        private ObservableCollection<MusicInfo> LoadSongsByPlaylistName(string playlistName)
        {
            // 根据歌单名称从数据库或其他数据源加载对应的歌曲列表数据
            // 返回一个包含歌曲的ObservableCollection<Song>对象
            return null;
        }
        #endregion
    }
}
