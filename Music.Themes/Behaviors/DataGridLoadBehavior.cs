using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Music.Themes.Behaviors
{
    public class DataGridLoadBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty LoadingProperty = DependencyProperty.Register(
            "Loading",
            typeof(bool),
            typeof(DataGridLoadBehavior),
            new PropertyMetadata(false));

        public bool Loading
        {
            get { return (bool)GetValue(LoadingProperty); }
            set { SetValue(LoadingProperty, value); }
        }

        private static void OnLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as DataGridLoadBehavior;
            if (behavior == null) return;

            var associatedObject = behavior.AssociatedObject;
            if (associatedObject is Grid panel)
            {
                foreach (var child in panel.Children)
                {
                    if (child is ProgressBar bar)
                    {
                        bar.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        //private void SetIsDataLoading(FrameworkElement element, bool isLoading)
        //{
        //    element.SetValue(IsDataLoadingProperty, isLoading);
        //}

        //public static readonly DependencyProperty IsDataLoadingProperty = DependencyProperty.RegisterAttached(
        //    "IsDataLoading",
        //    typeof(bool),
        //    typeof(DataGridLoadBehavior),
        //    new PropertyMetadata(true,OnDataLoadingChanged));

        //private static void OnDataLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
            
        //}

        protected override void OnAttached()
        {
            base.OnAttached();
          
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
