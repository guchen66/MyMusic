using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals.LoginSign
{
    public class LoginSignArgs : BindableBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public static bool IsSelected { get; set; } = false;
    }
}
