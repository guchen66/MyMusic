using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Dtos
{
    /// <summary>
    /// 注册参数
    /// </summary>
    public class SignUpDto
    {
        public string SessionId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string VerificationCode { get; set; }
    }

    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginInputDto : BaseDto
    {

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty<string>(ref _userName, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

    }
}
