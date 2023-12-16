using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Consts
{
    public class LoginSettings : BindableBase
    {
        private string _username;
        private string _password;
        private bool _autoLogin;
        private CultureInfo _language;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool AutoLogin
        {
            get => _autoLogin;
            set => SetProperty(ref _autoLogin, value);
        }

        public CultureInfo Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }
    }
}
