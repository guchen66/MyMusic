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
        public static bool CheckPing(string host = "baidu.com", int timeout = 3000)
        {
            using var ping = new Ping();
            try
            {
                var reply = ping.Send(host, timeout);
                return reply?.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}