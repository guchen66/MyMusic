using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.EmptyPlayListSign
{
    public class MusicPlayFactory
    {
        public IMusicPlay CreatePlayer(string selectType)
        {
            if (selectType==null)
            {
                return null;
            }

            if (selectType.Equals("Netease"))
            {
                return new NeteasePlay();
            }

            if (selectType.Equals("Kugou"))
            {
                return new KugouPlay();
            }

            if (selectType.Equals("QQ"))
            {
                return new QQPlay();
            }

            return null;
        }
    }
}
