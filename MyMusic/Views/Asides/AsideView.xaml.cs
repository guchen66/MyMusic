using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyMusic.Views.Asides
{
    /// <summary>
    /// AsidesView.xaml 的交互逻辑
    /// </summary>
    public partial class AsideView : UserControl
    {
        public AsideView()
        {
            InitializeComponent();
         //   GridTitle.PreviewMouseDown += GridTitle_PreviewMouseDown;
            /*btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }

            };
            //鼠标双击事件
            GridTitle.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            };*/



        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        /*   private void GridTitle_MouseDown(object sender, MouseButtonEventArgs e)
           {
               DragMove();

           }
   */

        private DateTime lastClickTime = DateTime.Now;
        private const int doubleClickInterval = 300; // 双击间隔时间阈值（单位：毫秒）

      /*  private void GridTitle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                DateTime currentClickTime = DateTime.Now;
                TimeSpan timeSinceLastClick = currentClickTime - lastClickTime;

                if (timeSinceLastClick.TotalMilliseconds <= doubleClickInterval)
                {
                    ToggleWindowState();
                }

                lastClickTime = currentClickTime;
            }
        }*/
      /*  private void ToggleWindowState()
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }*/


    }
}
