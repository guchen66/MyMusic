
using Music.Shared.Mvvm;

namespace MyMusic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region  字段
       // private readonly LoginProvider _loginProvider;
        private readonly ILogger logger;
        private readonly ILoginService _loginService;
        private  IConfiguration _configuration;
        #endregion

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

        #endregion

        #region  命令

        public ICommand CloseingCommand {  get; set; }
        public ICommand LoginCommand { get; set; }  
        public ICommand CancelCommand {  get; set; }
        #endregion

        public LoginViewModel(ILogger logger,ILoginService loginService, IContainerProvider provider):base(provider)
        {
           
            this.logger = logger;

            _loginService=loginService;
           // _loginProvider = loginProvider;
            CloseingCommand = new DelegateCommand(ExecuteClose);
            LoginCommand = new DelegateCommand<Window>(async (win) => await SignInAsync(win));
            CancelCommand = new DelegateCommand(SignOutAsync);
            UserName = "admin";
            Password = "123456";


        }
        
        #region 方法

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="win"></param>
        private async Task SignInAsync(Window win)
        {          
            logger.Info($"用户进行了登录操作");
            var loginResult = await AuthenticateAsync(UserName, Password);
            if (loginResult.Status)
            {
                EventAggregator.GetEvent<LoginEvent>().Publish(win);                
                await Task.Delay(100); // 假设等待100毫秒，你可以根据实际情况调整
                SplashScreenManager.CloseSplashScreen();
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
            EventAggregator.GetEvent<LogOutEvent>().Publish();
            //Application.Current.Shutdown();
        }

        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<ApiResponse> AuthenticateAsync(string username,string password)
        {
           // string passwordMd5= MD5Extension.GetMD5Provider(username,password);
            var result = await _loginService.LoginAsync(new LoginInputDto
            {
                UserName = username,
                Password = password
            });
            if (result)
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
        #endregion

        private static bool CanSignIn(string username, string password) => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }
}
