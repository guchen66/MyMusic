using Music.Shared.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals
{
    public static class UserInfo
    {
        //用户ID
        public static int UserId { get; set; }

        //用户名称
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static SysUser User { get; set; }
    }
}
