using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals.LoginSign
{
    public class GlobalLoginArgs
    {
        public static readonly GlobalLoginArgs Instance = new Lazy<GlobalLoginArgs>(() => new GlobalLoginArgs()).Value;


        public GlobalLoginArgs()
        {
            Init();
        }
        public void Init()
        {
            LoginAccountList = new ObservableCollection<LoginSignArgs>
            {
                new LoginSignArgs(){ Id=1, UserName="zs",Password="12345"},
                new LoginSignArgs(){ Id=2, UserName="li",Password = "12345"},
                new LoginSignArgs(){ Id=3, UserName="ww",Password = "12345"}
            };
        }

        public ObservableCollection<LoginSignArgs> LoginAccountList { get; set; }
    }
}
