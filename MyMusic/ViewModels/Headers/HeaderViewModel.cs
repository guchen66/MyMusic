
using Music.Shared.Entitys.Header;
using SqlSugar;
using Prism.Ioc;
namespace MyMusic.ViewModels.Headers
{
    public class HeaderViewModel : BaseViewModel
    {
        #region 字段

        private readonly IStateService _stateService;
        private readonly IPlayListService _playListService;
        IHeaderMusicSourceService _headerMusicSourceService;
        IAsideCreateControlService _asideCreateControlService;
        #endregion

        #region 属性

        /// <summary>
        /// 音乐来源
        /// </summary>
        private MusicSourceDto _musicSourceArgs;
        public MusicSourceDto MusicSource
        {
            get => _musicSourceArgs;
            set => SetProperty(ref _musicSourceArgs, value);
        }

        private ObservableCollection<MusicSourceInfo> _musicSourceInfoList;

        public ObservableCollection<MusicSourceInfo> MusicSourceInfoList
        {
            get => _musicSourceInfoList??(_musicSourceInfoList=new ObservableCollection<MusicSourceInfo>());
            set => SetProperty(ref _musicSourceInfoList, value);
        }

        /// <summary>
        /// 搜索文本框内容
        /// </summary>
        private string _searchContent;
        public string SearchContent
        {
            get => _searchContent;
            set => SetProperty(ref _searchContent, value);
        }

        /// <summary>
        /// 搜索的历史
        /// </summary>
        private bool _searchHistoryIsOpen = false;
        public bool SearchHistoryIsOpen
        {
            get => _searchHistoryIsOpen;
            set => SetProperty(ref _searchHistoryIsOpen, value);
        }

        public GlibalHeaderArgs HeaderArgs { get; set; } = GlibalHeaderArgs.Instance;
        
        private bool _isSlected;

        public bool IsSlected
        {
            get => _isSlected;
            set => SetProperty(ref _isSlected, value);
        }

        #endregion

        public HeaderViewModel(IHeaderMusicSourceService headerMusicSourceService, IStateService stateService, IPlayListService playListService,IContainerProvider provider) : base(provider)
        {
  
            _stateService = stateService;
            _playListService = playListService;
            _headerMusicSourceService=headerMusicSourceService;
            _asideCreateControlService = ContainerLocator.Container.Resolve<IAsideCreateControlService>();
            OpenSettingCommand = new DelegateCommand<string>(ExecuteSetting);
            CloseCommand = new DelegateCommand(ExecuteClosing);
            SearchCommand = new DelegateCommand<string>(ExecuteSearchSong);
            GoBackCommand = new DelegateCommand(ExecuteGoBack);
            ForWardCommand = new DelegateCommand(ExecuteForWard);
            OpenLoggerCommand = new DelegateCommand(ExecuteOpenLogger);
            DragMoveCommand = new DelegateCommand(ExecuteDragMove);
            LogoutCommand = new DelegateCommand(async () => await ExecuteRestartAsync());
            ConfirmPlaySourceCommand = new DelegateCommand<string>(async (source) => await ExecuteSourceAsync(source));
            InitializedCommand = new DelegateCommand(ExecuteInit);
        }      

        #region  命令

        public ICommand ConfirmPlaySourceCommand { get; set; }                   //确认播放源
        public ICommand OpenSettingCommand { get; set; }
        public ICommand CloseCommand {  get; set; }
        public ICommand SearchCommand {  get; set; }
        public ICommand GoBackCommand {  get; set; }
        public ICommand ForWardCommand { get; set; }
        public ICommand OpenLoggerCommand { get; set; }
        public ICommand DragMoveCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand InitializedCommand { get; set; }

        #endregion

        #region  方法

        #region 初始化查看是否选中播放源

        public void ExecuteInit()
        {
            Task.Run(async () => { await ExecuteHeaderMusicSourceInfo(); });
        }
        /// <summary>
        /// 查看Header是否选中播放源
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteHeaderMusicSourceInfo()
        {

            var musicSourceInfos = await _headerMusicSourceService.QueryListAsync();
            MusicSourceInfoList = musicSourceInfos.ToObservableCollection();
        }
        #endregion

        #region 导航

        private void ExecuteSetting(string paramters)
        {
            NavigationToView(paramters);
        }

        #endregion

        #region  关闭按钮

        public void ExecuteClosing()
        {
            Application.Current.Shutdown();
            //Application.Current.MainWindow.Close(); //关闭主窗口，报错
        }

        #endregion

        #region  前进后退

        private void ExecuteGoBack()
        {
            RegionManager.Regions["ContentRegion"].NavigationService.Journal.GoBack();
        }

        private void ExecuteForWard()
        {
            RegionManager.Regions["ContentRegion"].NavigationService.Journal.GoForward();
        }

        #endregion

        #region  搜索歌曲

        /// <summary>
        /// 构建空白界面，显示搜索的内容
        /// </summary>
        /// <param name="content"></param>
        public async void ExecuteSearchSong(string content)
        {       
            //防呆
            if (content == null)
            {
                PopupInputContentDialog popupDialog = new PopupInputContentDialog();
                popupDialog.Show();
                Task.Delay(1500).Wait();
                popupDialog.Close();return;
            }
            if (content != null)
            {
                var musicSourceInfos = await _headerMusicSourceService.QueryListAsync();

                var SourceName = musicSourceInfos.Where(x => x.IsSelected == true).Select(x=>x.SourceName).ToArray();

                //将搜索的内容和三大音乐官网的SourceName传输过去
                var navigationParameters = new NavigationParameters 
                {
                    { "PlaylistName", SearchContent },
                    { "SourceName",SourceName} 
                };
                if (navigationParameters == null)
                {
                    RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative));
                }
                if (navigationParameters != null)
                {
                    RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative), navigationParameters);
                }
            }

        }

        #endregion

        #region  一键换肤

        private DelegateCommand<object> _skinCmd;
        public DelegateCommand<object> SkinCommand =>
            _skinCmd ?? (_skinCmd = new DelegateCommand<object>(DoSkinCmd));

        public void DoSkinCmd(object obj)
        {


        }
        #endregion

        #region  退出主界面，重新登录

        private async Task ExecuteRestartAsync()
        {
            // 关闭当前窗口 
            Process.Start(Process.GetCurrentProcess().ProcessName);
            Application.Current.Shutdown();

            await Task.Run(() =>
            {
                // 关闭当前窗口
                Task.Delay(TimeSpan.FromSeconds(1));
                // 在STA线程上打开登录窗口
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LoginView loginWindow = new LoginView();
                    loginWindow.Show();
                }, DispatcherPriority.Normal, null);

            });
        }
        #endregion

        #region  确认播放源
       
        private async Task ExecuteSourceAsync(string source)
        {
            var music=await _headerMusicSourceService.QueryAsync(x=>x.SourceName==source);
            music.IsSelected= !music.IsSelected;
            await _headerMusicSourceService.UpdateAsync(music);
        }

        #endregion

        #region 查看本地日志

        private void ExecuteOpenLogger()
        {

            // 从配置中获取日志文件夹路径
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            try
            {
                if (string.IsNullOrWhiteSpace(logDirectory))
                {
                    // 如果日志文件夹路径为空（可能是配置里没有定义），则弹出一个错误的对话框
                    MessageBox.Show("NLog配置错误.", "Error");
                    return;
                }
                // 检查日志文件夹是否存在和可访问
                if (!Directory.Exists(logDirectory))
                {
                    // 日志文件夹路径不正确，弹出错误对话框
                    MessageBox.Show($"Cannot access log directory: {logDirectory}", "Error");
                    return;
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"\"{logDirectory}\"",
                    Verb = "runas"          //以管理员的身份运行
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open log directory: {ex.Message}", "Error");
            }

        }
        #endregion

        #region 鼠标左键移动

        private void ExecuteDragMove()
        {
            Application.Current.MainWindow.DragMove();

        }

        private void HandleHeaderViewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 检查是否点击了关闭程序重新登录的按钮
            var closeButton = ((Control)sender)?.Template?.FindName("BackLoginName", (Control)sender) as Button;
            if (closeButton != null && closeButton.IsPressed)
            {
                return;
            }

            // 执行拖动命令
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMoveCommand.Execute(null);
            }
        }
        #endregion
      
        #endregion
    }
}
