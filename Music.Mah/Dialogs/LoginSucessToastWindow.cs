using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Music.Mah.Dialogs
{
    /// <summary>
    /// 父类装载容器，用来显示LoginSucessView的
    /// </summary>
    public class LoginSucessToastWindow : Window, IDialogWindow
    {
        public IDialogResult Result { get; set; }

        public LoginSucessToastWindow()
        {
            WindowStyle = WindowStyle.None;      // 去掉标题栏、最小最大按钮
            AllowsTransparency = true;           // 允许窗口透明，后面 Background 才能用半透明/圆角
            Background = Brushes.WhiteSmoke;          // 临时给个颜色，实际可换 `Transparent` 或圆角背景
            ShowInTaskbar = false;               // 任务栏不显示图标
            Topmost = true;                      // 始终置顶
            ResizeMode = ResizeMode.NoResize;    // 禁止拖动边框改变大小
            SizeToContent = SizeToContent.WidthAndHeight; // 宽高按内容自动计算
            WindowStartupLocation = WindowStartupLocation.Manual; // 不居中，手动给坐标

            // 关键：在内容渲染前订阅事件
            ContentRendered += OnContentRendered;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 先设置一个大致位置，避免显示在左上角
            SetInitialPosition();
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            // 内容完全渲染后，精确设置位置
            SetPrecisePosition();
            StartAutoCloseTimer();

            // 清理事件
            ContentRendered -= OnContentRendered;
        }

        private void SetInitialPosition()
        {
            // 先设置一个大致位置（右下角区域）
            var workingArea = SystemParameters.WorkArea;
            Left = workingArea.Right - 300; // 预估宽度
            Top = workingArea.Bottom - 100; // 预估高度
        }

        private void SetPrecisePosition()
        {
            var workingArea = SystemParameters.WorkArea;
            Left = workingArea.Right - ActualWidth; // 留点边距
            Top = workingArea.Bottom - ActualHeight;
        }

        private void StartAutoCloseTimer()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                Close();
            };
            timer.Start();
        }
    }
}