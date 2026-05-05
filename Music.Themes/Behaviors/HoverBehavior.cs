using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;

namespace Music.Themes.Behaviors
{
    public class HoverBehavior : Behavior<FrameworkElement>
    {
        // 定义可绑定的属性：放大倍数和动画时长
        public double ScaleFactor { get; set; } = 1.2;

        public double Duration { get; set; } = 0.3;

        protected override void OnAttached()
        {
            base.OnAttached();
            // 监听鼠标进入和离开事件
            AssociatedObject.MouseEnter += OnMouseEnter;
            AssociatedObject.MouseLeave += OnMouseLeave;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= OnMouseEnter;
            AssociatedObject.MouseLeave -= OnMouseLeave;
            base.OnDetaching();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            // 找到目标 TextBlock（假设在 StackPanel 内）
            if (AssociatedObject is StackPanel stackPanel &&
                stackPanel.Children[1] is TextBlock textBlock)
            {
                StartAnimation(textBlock, textBlock.FontSize * ScaleFactor);
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (AssociatedObject is StackPanel stackPanel &&
                stackPanel.Children[1] is TextBlock textBlock)
            {
                StartAnimation(textBlock, textBlock.FontSize / ScaleFactor);
            }
        }

        private void StartAnimation(TextBlock target, double targetFontSize)
        {
            // 创建字体大小动画
            var animation = new DoubleAnimation
            {
                To = targetFontSize,
                Duration = TimeSpan.FromSeconds(Duration),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            target.BeginAnimation(TextBlock.FontSizeProperty, animation);
        }
    }
}