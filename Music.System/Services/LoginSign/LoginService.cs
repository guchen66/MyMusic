using Music.Core.Components;
using Music.System.Services.MainSign.HeaderSign.Dtos;
using Music.System.Services.Loggers;
using Music.System.Services.LoginSign.Dtos;
using Music.Shared.Args;
using Music.Shared.Events.LoginSign;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Music.Shared.Attributes;

namespace Music.System.Services.LoginSign
{
    [Scanning(RegisterType= "Singleton")]
    public class LoginService : ILoginService
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;
        private readonly IContainerProvider _container;
        public LoginService(IEventAggregator eventAggregator, ILogger logger, IContainerProvider container)
        {
            this._logger = logger;
            _eventAggregator = eventAggregator;
            _container = container;
            _eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginExecute);
            _eventAggregator.GetEvent<LogOutEvent>().Subscribe(async()=>await LogoutAsync());
        }

        public void LoginExecute(Window win)
        {
            win.DialogResult = true;    //Window.DialogResult 属性表示对话框的返回值=true表示APp.xaml.cs中的MainWindow已经成功登录，                                        //  win.Close();             //然后关闭LoginView
            // 关闭当前登录界面
            LogoutAsync();
        }

        public Task LogoutAsync()
        {
            // 查找当前活动窗口并关闭
            var activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            activeWindow?.Close();
            return Task.CompletedTask;
        }

        public async Task<bool> LoginAsync(LoginInputDto loginArgs)
        {
            if (loginArgs == null) return false;
            LoginInputDto canLoginArgs = await IsCanLoginAsync(); // 调用异步的IsCanLoginAsync方法获取登录参数
            var result = canLoginArgs.UserName == loginArgs.UserName && canLoginArgs.Password == loginArgs.Password;
            return result;
           
        }
       
        public Task<bool> RegisterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SavePasswordAsync()
        {
            if (File.Exists("rememberPwd.json"))
            {
                string serializedSettings = await File.ReadAllTextAsync("rememberPwd.json");
                var dataList = JsonConvert.DeserializeObject<LoginInputDto>(serializedSettings);

                if (dataList != null)
                {
                   // GlobalLoginArgs.Instance.LoginAccountList=
                       string s= dataList.Password;
                }
            }
            return true;
        }

        public async Task<LoginInputDto> IsCanLoginAsync()
        {
            string[] jsonFileName =await JsonComponent.GetJsonFileNamesAsync();  // 先找到所有的json文件
            var json =await JsonComponent.GetDecisionJsonAsync(jsonFileName.FirstOrDefault(x => x.Contains("Account")), "Account"); // 获得名称包含Account的
            LoginInputDto loginDto = JsonConvert.DeserializeObject<LoginInputDto>(json);
            return loginDto;
        }
    }
}
