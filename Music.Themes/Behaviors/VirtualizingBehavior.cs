using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Music.Themes.Behaviors
{
    /// <summary>
    /// 对ListBox和ListView进行虚拟化操作
    /// </summary>
    public class VirtualizingBehavior : Behavior<ListBox>
    {
        protected override void OnAttached() =>
            VirtualizingPanel.SetIsVirtualizing(AssociatedObject, true);
    }
}