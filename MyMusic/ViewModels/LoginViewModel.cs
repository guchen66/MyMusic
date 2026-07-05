using IT.Tangdao.Core.Abstractions.Loggers;
using IT.Tangdao.Core.Threading;
using System.Diagnostics;

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

        private LoginInputDto _loginInputDto;

        public LoginInputDto LoginInputDto
        {
            get { return _loginInputDto ?? (_loginInputDto = new LoginInputDto()); }
            set { SetProperty(ref _loginInputDto, value); }
        }

        #endregion 属性

        #region 命令

        public ICommand CloseingCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand LoadedCommand { get; set; }

        #endregion 命令

        private Stopwatch stopwatch;
        private ITangdaoLogger logger = TangdaoLogger.Get<LoginViewModel>();

        public LoginViewModel(ILogger logger, ILoginService loginService, IAuthorService authorService, IContainerProvider provider) : base(provider)
        {
            _loginService = loginService;
            _authorService = authorService;
            LoginCommand = new DelegateCommand<Window>((win) => SignIn(win));
            CancelCommand = new DelegateCommand(SignOutAsync);
            LoadedCommand = new DelegateCommand(InitLoad);
        }

        #region 方法

        public void InitLoad()
        {
            var jsonDto = JsonComponent.GetJsonObject<LoginInputDto>("loginAccount.json");
            LoginInputDto.UserName = jsonDto.UserName;
            LoginInputDto.Password = jsonDto.Password;
            UIAmbientContext.SetObject("LoginInput", LoginInputDto);
        }

        /// <summary>
        /// 异步登录，因为需要启动时读取网络数据
        /// </summary>
        /// <param name="win"></param>
        private async void SignIn(Window win)
        {
            var loginResult = await AuthenticateAsync(LoginInputDto.UserName, LoginInputDto.Password);
            if (loginResult.IsSuccess)
            {
                EventAggregator.GetEvent<LoginEvent>().Publish(win);
            }
            else
            {
                MessageBox.Show(loginResult.Message);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void SignOutAsync()
        {
            EventAggregator.GetEvent<LogoutEvent>().Publish();
        }

        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<IResponseResult> AuthenticateAsync(string username, string password)
        {
            // string passwordMd5= MD5Extension.GetMD5Provider(username,password);
            var result = await _authorService.VerifyAsync(new LoginInputDto
            {
                UserName = username,
                Password = password
            });
            if (result.IsSuccess)
            {
                return ResponseResult.Success(LoginConst.LoginSucess);
            }
            return ResponseResult.Failure(LoginConst.LoginFailed);
        }

        #endregion 方法

        private static bool CanSignIn(string username, string password) => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }
}