namespace MyMusic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region 字段

        private readonly ILogger _logger;
        private readonly ILoginService _loginService;
        private IConfiguration _configuration;
        private readonly IAuthorService _authorService;

        #endregion 字段

        #region 属性

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { SetProperty<string>(ref _username, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

        #endregion 属性

        #region 命令

        public ICommand CloseingCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion 命令

        public LoginViewModel(ILogger logger, ILoginService loginService, IAuthorService authorService, IContainerProvider provider) : base(provider)
        {
            _logger = logger;
            _loginService = loginService;
            _authorService = authorService;
            CloseingCommand = new DelegateCommand(ExecuteClose);
            LoginCommand = new DelegateCommand<Window>(async (win) => await SignInAsync(win));
            CancelCommand = new DelegateCommand(SignOutAsync);
            UserName = "admin";
            Password = "123456";
        }

        #region 方法

        /// <summary>
        /// 异步登录，因为需要启动时读取网络数据
        /// </summary>
        /// <param name="win"></param>
        private async Task SignInAsync(Window win)
        {
            _logger.Info($"用户进行了登录操作");
            var loginResult = await AuthenticateAsync(UserName, Password);
            if (loginResult.Status)
            {
                EventAggregator.GetEvent<LoginEvent>().Publish(win);
            }
            else
            {
                MessageBox.Show(loginResult.Result.ToString());
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void SignOutAsync()
        {
            EventAggregator.GetEvent<LogoutEvent>().Publish();

            //Application.Current.Shutdown();
        }

        private void Quit(Window win)
        {
            EventAggregator.GetEvent<QuitEvent>().Publish(win);
        }

        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<ApiResponse> AuthenticateAsync(string username, string password)
        {
            // string passwordMd5= MD5Extension.GetMD5Provider(username,password);
            var result = await _authorService.VerifyAsync(new LoginInputDto
            {
                UserName = username,
                Password = password
            });
            if (result.IsSuccess)
            {
                return new ApiResponse(true, LoginConst.LoginSucess);
            }
            return new ApiResponse(false, LoginConst.LoginFailed);
        }

        private void ExecuteClose()
        {
            SplashWindow splashWindow = new SplashWindow();
            splashWindow.Show();
        }

        #endregion 方法

        private static bool CanSignIn(string username, string password) => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }
}