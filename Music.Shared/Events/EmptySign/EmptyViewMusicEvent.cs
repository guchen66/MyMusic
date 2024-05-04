using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Events.EmptySign
{
    /// <summary>
    /// 空白界面向FooterView界面传递音乐名称，或者url
    /// </summary>
    public class EmptyViewMusicEvent:PubSubEvent<string>
    {
    }
}
