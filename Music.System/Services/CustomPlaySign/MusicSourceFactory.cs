using Music.System.Services.CustomPlaySign.Kugou;
using Music.System.Services.CustomPlaySign.Netease;
using Music.System.Services.CustomPlaySign.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.CustomPlaySign
{
    public class MusicSourceFactory
    {
        public static IBasePlayService CreatePlayProvider(string sourceName)
        {
            if (string.IsNullOrEmpty(sourceName)) return null;
            if (sourceName.Equals("酷狗"))
            {
                return new KugouPlayService();
            }
            else if (sourceName.Equals("QQ"))
            {
                return new QQPlayService();
            }
            else if(sourceName.Equals("网易云"))
            {
                return new NeteasePlayService();
            }
            return null;
        }
    }
}
