using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// AddPlayListDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddPlayListDialog : StackPanel
    {
        public AddPlayListDialog()
        {
            InitializeComponent();
            textBox.PreviewTextInput += TextBox_PreviewTextInput;

        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 使用正则表达式来检查输入是否为汉字、数字或字母
            Regex regex = new Regex("[^a-zA-Z0-9\u4e00-\u9fa5]+"); // 匹配非汉字、数字和字母的字符
            e.Handled = regex.IsMatch(e.Text);

            // 检查输入后的文本长度是否为1，如果是，则禁止继续删除
            if (textBox.Text.Length == 20 && e.Text == "")
            {
                e.Handled = true; // 禁止输入
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text.Length > 16)
            {
                textBox.Text = textBox.Text.Substring(0, 16); // 截取前16个字符
                textBox.CaretIndex = textBox.Text.Length; // 将光标移动到末尾
            }
        }
    }
}
