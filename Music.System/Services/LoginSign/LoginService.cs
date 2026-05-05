using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Helpers;

namespace Music.System.Services.LoginSign
{
    [AutoRegister(Mode = RegisterMode.Scoped)]
    public class LoginService : ILoginService, IDisposable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;
        private readonly IContainerProvider _container;
        private SubscriptionToken _loginEventToken;  // 这里声明

        public LoginService(IEventAggregator eventAggregator, ILogger logger)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
            _loginEventToken = _eventAggregator.GetEvent<LoginEvent>().Subscribe(ExecuteLogin, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<LogoutEvent>().Subscribe(async () => await LogoutAsync());
            // _eventAggregator.GetEvent<QuitEvent>().Subscribe(Quit);
        }

        public void ExecuteLogin(Window win)
        {
            win.DialogResult = true;    //Window.DialogResult 属性表示对话框的返回值=true表示APp.xaml.cs中的MainWindow已经成功登录
        }

        public Task LogoutAsync()
        {
            // 查找当前活动窗口并关闭
            var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            activeWindow.DialogResult = false;
            // await Task.Delay(100); // 假设等待100毫秒，你可以根据实际情况调整
            activeWindow?.Close();
            //  var s1=_container.Resolve<LoginView>();
            return Task.CompletedTask;
        }

        public Task<bool> RegisterAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _eventAggregator.GetEvent<LoginEvent>().Unsubscribe(_loginEventToken);
        }

        public Task<bool> LoginAsync(LoginInputDto loginArgs)
        {
            throw new NotImplementedException();
        }
    }
}