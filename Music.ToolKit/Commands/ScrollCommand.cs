using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Music.ToolKit.Commands
{
    //<i:Interaction.Triggers>
    //     <!-- 监听 PreviewMouseWheel 事件 -->
    //     <i:EventTrigger EventName = "PreviewMouseWheel" >
    //         < !--把事件参数直接传给 Command -->
    //         <i:InvokeCommandAction Command = "{Binding ScrollCommand}"
    //                                 EventArgsParameter="True" />
    //     </i:EventTrigger>
    // </i:Interaction.Triggers>
    public class ScrollCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is ScrollViewer;
        }

        public void Execute(object parameter)
        {
            // 这里 parameter 就是 MouseWheelEventArgs
            if (parameter is MouseWheelEventArgs args)
            {
                // 从事件参数中获取滚轮方向
                if (args.Delta > 0)
                    ((ScrollViewer)args.OriginalSource).LineUp();
                else
                    ((ScrollViewer)args.OriginalSource).LineDown();
            }
        }
    }
}