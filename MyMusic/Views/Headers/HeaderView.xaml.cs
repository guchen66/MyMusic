using MyMusic.Providers;
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

namespace MyMusic.Views
{
    /// <summary>
    /// HeaderView.xaml 的交互逻辑
    /// </summary>
    public partial class HeaderView : UserControl
    {
        public HeaderView()
        {
            InitializeComponent();
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button == null) return;
            string? tag = button.Tag.ToString();

            ResxLanguageConfigHelper.SetLanguage(tag);
        }  
    }
}
