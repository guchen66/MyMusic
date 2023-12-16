using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Music.Shared.Events.LoginSign
{
    public class LoginEvent : PubSubEvent<Window> { }

    public class LogOutEvent : PubSubEvent { }

    /// <summary>
    /// 后退
    /// </summary>
    public class GoBackEvent : PubSubEvent
    {

    }
    /// <summary>
    /// 前进
    /// </summary>
    public class ForWardEvent : PubSubEvent
    {

    }
    /// <summary>
    /// 搜索
    /// </summary>
    public class SearchEvent : PubSubEvent<string>
    {

    }

    /// <summary>
    /// 刷新
    /// </summary>
    public class RefreshEvent : PubSubEvent
    {

    }
}
