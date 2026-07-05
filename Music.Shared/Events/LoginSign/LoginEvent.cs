namespace Music.Shared.Events.LoginSign
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginEvent : PubSubEvent<Window>
    { }

    /// <summary>
    /// 注销
    /// </summary>
    public class LogoutEvent : PubSubEvent
    { }      //有点问题

    /// <summary>
    /// 退出
    /// </summary>
    public class QuitEvent : PubSubEvent<Window>
    { }

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

    public class SplashEvent : PubSubEvent
    {
    }
}