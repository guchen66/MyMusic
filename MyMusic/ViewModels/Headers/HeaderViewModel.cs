
namespace MyMusic.ViewModels.Headers
{
    public class HeaderViewModel : BindableBase
    {
        #region 字段

        IRegionNavigationJournal _navigationJournal;//导航日志，上一页，下一页
        IRegionManager _regionManager;//区域管理
        IDialogService _dialogService;
        IEventAggregator _eventAggregator;
        IStateService _stateService;
        IPlayListService _playListService;
        #endregion

        #region 属性

        /// <summary>
        /// 音乐来源
        /// </summary>
        private MusicSourceDto _musicSourceArgs;
        public MusicSourceDto MusicSource
        {
            get { return _musicSourceArgs; }
            set
            {
                _musicSourceArgs = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 搜索文本框内容
        /// </summary>
        private string _searchContent;
        public string SearchContent
        {
            get { return _searchContent; }
            set { SetProperty(ref _searchContent, value); }
        }

        /// <summary>
        /// 搜索的历史
        /// </summary>
        private bool _searchHistoryIsOpen = false;
        public bool SearchHistoryIsOpen
        {
            get { return _searchHistoryIsOpen; }
            set
            {
                _searchHistoryIsOpen = value;
                RaisePropertyChanged("SearchHistoryIsOpen");
            }
        }

        public GlibalHeaderArgs HeaderArgs { get; set; } = GlibalHeaderArgs.Instance;
        
        private bool _isSlected;

        public bool IsSlected
        {
            get
            {
                 return _isSlected;               
               
            }
            set { SetProperty<bool>(ref _isSlected, value); }
        }

        #endregion

        public HeaderViewModel(IRegionNavigationJournal navigationJournal, IDialogService dialogService, IRegionManager regionManager, IEventAggregator eventAggregator,IStateService stateService, IPlayListService playListService)
        {
            _navigationJournal = navigationJournal;
            _dialogService = dialogService;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _stateService = stateService;
            _playListService = playListService;

            OpenSettingCommand = new DelegateCommand<string>(ExecuteSetting);
            CloseCommand = new DelegateCommand(ExecuteClosing);
            SearchCommand = new DelegateCommand<string>(ExecuteSearchSong);
            GoBackCommand = new DelegateCommand(ExecuteGoBack);
            ForWardCommand = new DelegateCommand(ExecuteForWard);
            OpenLoggerCommand = new DelegateCommand(ExecuteOpenLogger);
            DragMoveCommand = new DelegateCommand(ExecuteDragMove);
            LogoutCommand = new DelegateCommand(async () => await ExecuteRestartAsync());
            ConfirmPlaySourceCommand = new DelegateCommand<string>(async (source) => await ExecuteSourceAsync(source));
           
        }

        #region  命令

        public ICommand ConfirmPlaySourceCommand { get; set; }
        public ICommand OpenSettingCommand { get; set; }
        public ICommand CloseCommand {  get; set; }
        public ICommand SearchCommand {  get; set; }
        public ICommand GoBackCommand {  get; set; }
        public ICommand ForWardCommand { get; set; }
        public ICommand OpenLoggerCommand { get; set; }
        public ICommand DragMoveCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        #endregion

        #region  方法

        #region 导航

        private void ExecuteSetting(string paramters)
        {
            Navigate(paramters);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, arg =>
                {
                    _navigationJournal = arg.Context.NavigationService.Journal;
                });
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
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoBack();
        }

        private void ExecuteForWard()
        {
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoForward();
        }

        #endregion

        #region  搜索歌曲

        /// <summary>
        /// 构建空白界面，显示搜索的内容
        /// </summary>
        /// <param name="content"></param>
        public async void ExecuteSearchSong(string content)
        {       
            if (content == null)
            {
                PopupInputContentDialog popupDialog = new PopupInputContentDialog();
                popupDialog.Show();
                Task.Delay(1000).Wait();
                popupDialog.Close();
            }
            if (content != null)
            {
                JsonProvider jsonProvider = new JsonProvider();
                List<MusicSourceDto> list =await jsonProvider.GetMusicSourceDto();          
                var SourceName = list.Where(m => m.IsSelected == true).Select(m => m.Name).ToArray();

                var playListInfo = await _playListService.GetPlatListByNameAsync(content);
                
                //将搜索的内容和三大音乐官网的来源传输过去
                var navigationParameters = new NavigationParameters 
                {
                    { "PlaylistName", SearchContent },
                    { "Source",SourceName} 
                };
                if (navigationParameters == null)
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative));
                }
                if (navigationParameters != null)
                {
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("EmptyPlayListView", UriKind.Relative), navigationParameters);
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
          
            await _stateService.SaveState(GlibalHeaderArgs.Instance.MusicSourceList.ToList());

        }


        public static void SaveState(ObservableCollection<DownLoadInfo> items)
        {
            // 创建设置settings对象
            var settingsList = new List<MusicSettings>();

            // 遍历所有StartupInfo
            foreach (var item in items)
            {
                // 创建MusicSettings对象并添加到settingsList
                var settings = new MusicSettings() { Id = item.Id, IsSelected = item.IsSelected, Type = SettingType.DownLoadInfo };
                settingsList.Add(settings);
            }
            // 序列化保存
            string serializedSettings = JsonConvert.SerializeObject(settingsList);
            File.WriteAllText("settings.json", serializedSettings);
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
