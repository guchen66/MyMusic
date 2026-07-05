using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Music.Themes.Behaviors
{
    public class TextTruncateBehavior : Behavior<TextBlock>
    {
        // 最大显示字数（默认16）
        public static readonly DependencyProperty MaxDisplayLengthProperty =
            DependencyProperty.Register(
                nameof(MaxDisplayLength),
                typeof(int),
                typeof(TextTruncateBehavior),
                new PropertyMetadata(16));

        // 是否启用（默认true）
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(
                nameof(IsEnabled),
                typeof(bool),
                typeof(TextTruncateBehavior),
                new PropertyMetadata(true));

        public int MaxDisplayLength
        {
            get => (int)GetValue(MaxDisplayLengthProperty);
            set => SetValue(MaxDisplayLengthProperty, value);
        }

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        // 存储原始文本的字典（因为TextBlock没有Tag，用这个来保存）
        private readonly Dictionary<TextBlock, string> _originalTextCache = new Dictionary<TextBlock, string>();

        protected override void OnAttached()
        {
            base.OnAttached();

            // 监听 Text 属性变化
            var dpd = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));
            if (dpd != null)
            {
                dpd.AddValueChanged(AssociatedObject, OnTextChanged);
            }

            // 加载完成后执行一次
            AssociatedObject.Loaded += OnLoaded;

            // 如果已经加载完成，直接执行
            if (AssociatedObject.IsLoaded)
            {
                ApplyTruncate();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var dpd = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));
            if (dpd != null)
            {
                dpd.RemoveValueChanged(AssociatedObject, OnTextChanged);
            }

            AssociatedObject.Loaded -= OnLoaded;
            _originalTextCache.Remove(AssociatedObject);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ApplyTruncate();
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            ApplyTruncate();
        }

        private void ApplyTruncate()
        {
            if (AssociatedObject == null || !IsEnabled) return;

            // 获取原始文本
            string originalText = AssociatedObject.Text;
            if (string.IsNullOrEmpty(originalText))
            {
                // 如果文本为空，清空缓存
                _originalTextCache.Remove(AssociatedObject);
                AssociatedObject.ToolTip = null;
                return;
            }

            // 如果缓存中没有，或者缓存中的文本和当前文本不同（说明绑定的数据变了）
            if (!_originalTextCache.TryGetValue(AssociatedObject, out string cachedText) || cachedText != originalText)
            {
                // 如果当前文本已经是截断后的格式，且缓存中有原始文本，说明是重新进入，需要恢复
                // 但如果缓存中没有，或者缓存不同，则把当前文本作为原始文本保存
                _originalTextCache[AssociatedObject] = originalText;
            }

            // 获取缓存的原始文本
            string original = _originalTextCache[AssociatedObject];

            // 如果原始文本长度超过限制，进行截断
            if (original.Length > MaxDisplayLength)
            {
                AssociatedObject.Text = original.Substring(0, MaxDisplayLength) + "...";
                AssociatedObject.ToolTip = original;  // 完整文本显示在ToolTip
            }
            else
            {
                AssociatedObject.Text = original;
                AssociatedObject.ToolTip = null;
            }
        }
    }
}