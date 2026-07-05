using System.Windows;
using System.Windows.Media.Animation;

namespace Music.Themes.Behaviors
{
    public static class StoryboardHelper
    {
        // 定义附加属性：StoryboardKey
        public static readonly DependencyProperty StoryboardKeyProperty =
            DependencyProperty.RegisterAttached(
                "StoryboardKey",
                typeof(string),
                typeof(StoryboardHelper),
                new PropertyMetadata(null, OnStoryboardKeyChanged));

        // 定义附加属性：PlayStoryboard（触发播放）
        public static readonly DependencyProperty PlayStoryboardProperty =
            DependencyProperty.RegisterAttached(
                "PlayStoryboard",
                typeof(bool),
                typeof(StoryboardHelper),
                new PropertyMetadata(false, OnPlayStoryboardChanged));

        // Getter / Setter for StoryboardKey
        public static string GetStoryboardKey(DependencyObject obj)
            => (string)obj.GetValue(StoryboardKeyProperty);

        public static void SetStoryboardKey(DependencyObject obj, string value)
            => obj.SetValue(StoryboardKeyProperty, value);

        // Getter / Setter for PlayStoryboard
        public static bool GetPlayStoryboard(DependencyObject obj)
            => (bool)obj.GetValue(PlayStoryboardProperty);

        public static void SetPlayStoryboard(DependencyObject obj, bool value)
            => obj.SetValue(PlayStoryboardProperty, value);

        // 当 StoryboardKey 改变时，查找并缓存 Storyboard
        private static void OnStoryboardKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue is string key)
            {
                // 查找资源中的 Storyboard
                if (element.Resources.Contains(key))
                {
                    var storyboard = element.Resources[key] as Storyboard;
                    if (storyboard != null)
                    {
                        // 设置 Target 为当前元素
                        Storyboard.SetTarget(storyboard, element);
                    }
                }
            }
        }

        // 当 PlayStoryboard 变为 true 时，播放动画
        private static void OnPlayStoryboardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue is bool play && play)
            {
                var key = GetStoryboardKey(element);
                if (!string.IsNullOrEmpty(key) && element.Resources.Contains(key))
                {
                    var storyboard = element.Resources[key] as Storyboard;
                    if (storyboard != null)
                    {
                        // 确保 Target 正确设置
                        Storyboard.SetTarget(storyboard, element);

                        // 创建副本以避免多次播放冲突
                        var clone = storyboard.Clone();
                        clone.Begin(element);
                    }
                }
            }
        }
    }
}