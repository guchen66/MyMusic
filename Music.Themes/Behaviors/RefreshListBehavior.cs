using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PropertyMetadata = System.Windows.PropertyMetadata;

namespace Music.Themes.Behaviors
{
    public static class RefreshItemsControlBehavior
    {
        public static readonly DependencyProperty IsChangedProperty =
            DependencyProperty.RegisterAttached("IsChanged", typeof(bool), typeof(RefreshItemsControlBehavior), new PropertyMetadata(false, OnItemIsChanged));

        public static bool GetIsChanged(DependencyObject button)
        {
            return (bool)button.GetValue(IsChangedProperty);
        }

        public static void SetIsChanged(DependencyObject button, bool value)
        {
            button.SetValue(IsChangedProperty, value);
        }

        private static void OnItemIsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ItemsControl itemsControl)
            {
                itemsControl.Items.Refresh();
             
            }
        }

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

    }

}
