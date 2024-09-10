using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core.Helpers
{
    public class NetConnHelper
    {
        public static bool CheckPing()
        {
            bool onLine = false;
            Ping ping=new Ping();
            PingReply pingReply = ping.Send("baidu.com");
            if (pingReply.Status==IPStatus.Success)
            {
                onLine = true;
                return onLine;
            }
            return onLine;
        }   
    }
}
