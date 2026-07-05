using Music.SqlSugar.Services;
using Music.SqlSugar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MyMusic.ViewModels
{
    public class SplashViewModel : BindableBase
    {
        public ICommand LoadedCommand { get; set; }
        private readonly IEventAggregator _eventAggregator;

        private string _loadingText = "正在加载...";

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        private string _version = "v1.0.0";

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        private double _scale = 1;

        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value);
        }

        private double _opacity = 1;

        public double Opacity
        {
            get => _opacity;
            set => SetProperty(ref _opacity, value);
        }

        // 🎯 新增：控制动画播放的布尔属性
        private bool _playFadeOut;

        public bool PlayFadeOut
        {
            get => _playFadeOut;
            set => SetProperty(ref _playFadeOut, value);
        }

        private string[] _loadingTexts = new[] { "正在加载...", "正在初始化...", "正在连接数据库...", "即将完成..." };
        private int _textIndex = 0;
        private System.Windows.Threading.DispatcherTimer _textTimer;

        public SplashViewModel(IEventAggregator eventAggregator)
        {
            LoadedCommand = new DelegateCommand(ExecuteLoaded);
            _eventAggregator = eventAggregator;
        }

        private void ExecuteLoaded()
        {
            StartTextRotation();
            InitTask();
        }

        private void StartTextRotation()
        {
            _textTimer = new System.Windows.Threading.DispatcherTimer();
            _textTimer.Interval = TimeSpan.FromSeconds(1.5);
            _textTimer.Tick += (s, e) =>
            {
                _textIndex = (_textIndex + 1) % _loadingTexts.Length;
                LoadingText = _loadingTexts[_textIndex];
            };
            _textTimer.Start();
        }

        private void InitTask()
        {
            Task.Run(async () =>
            {
                // 进行初始化，数据库预热
                var response = ContainerLocator.Container.Resolve<IHeaderMusicSourceService>();
                GlobalPreLoadDataProvider.MusicSourceInfos = await response.QueryListAsync();
                // await Task.Delay(1500);
            }).ContinueWith(x =>
            {
                //  Application.Current.Dispatcher.BeginInvoke(new Action(CompleteSplash));

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _eventAggregator.GetEvent<SplashEvent>().Publish();
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void CompleteSplash()
        {
            _textTimer?.Stop();

            // 🎯 触发淡出动画
            PlayFadeOut = true;

            // 等待动画完成（500ms）后跳转
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                _eventAggregator.GetEvent<SplashEvent>().Publish();
            };
            timer.Start();
        }
    }
}