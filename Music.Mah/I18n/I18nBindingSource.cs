using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Music.Mah.I18n
{
    internal sealed class I18nBindingSource : INotifyPropertyChanged
    {
        public static I18nBindingSource Instance { get; } = new();

        private I18nBindingSource()
        {
            I18nManager.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            // 通知所有属性都已更改，强制刷新所有绑定
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }

        // 索引器，用于通过键获取本地化字符串
        public string this[string key] => I18nManager.Get(key);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}