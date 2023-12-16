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

namespace MyMusic.Views.Dialog
{
    /// <summary>
    /// ReNamePlayListDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ReNamePlayListDialog : UserControl
    {
        public ReNamePlayListDialog()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text.Length > 16)
            {
                textBox.Text = textBox.Text.Substring(0, 16); // 截取前16个字符
                textBox.CaretIndex = textBox.Text.Length; // 将光标移动到末尾
            }
        }
    }
}
