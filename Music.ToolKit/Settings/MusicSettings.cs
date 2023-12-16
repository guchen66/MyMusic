using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.Settings
{
    [Serializable]
    public class MusicSettings
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public SettingType Type { get; set; } // 修改为枚举类型
    }

    public enum SettingType
    {
        StartupInfo,
        DownLoadInfo,
        LyricsSetInfo,

        //添加其他TabItem类型
    }
}
