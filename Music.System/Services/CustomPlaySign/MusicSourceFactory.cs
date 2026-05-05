using Music.System.Services.CustomPlaySign.Kugou;
using Music.System.Services.CustomPlaySign.Netease;
using Music.System.Services.CustomPlaySign.QQ;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.CustomPlaySign
{
    public class MusicSourceFactory
    {
        private static readonly ConcurrentDictionary<string, Lazy<IBasePlayService>> _instances = new();

        public static IBasePlayService CreatePlayProvider(string sourceName)
        {
            if (string.IsNullOrEmpty(sourceName)) return null;

            var lazy = _instances.GetOrAdd(sourceName, key => new Lazy<IBasePlayService>(() => key switch
            {
                "酷狗" => new KugouPlayService(),
                "QQ" => new QQPlayService(),
                "网易云" => new NeteasePlayService(),
                _ => null
            }));

            return lazy.Value;
        }
    }
}