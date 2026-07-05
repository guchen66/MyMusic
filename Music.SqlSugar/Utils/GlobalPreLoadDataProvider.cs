using Music.Shared.Dtos;
using Music.Shared.Entitys.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Utils
{
    /// <summary>
    /// 全局预加载数据提供器
    /// </summary>
    public class GlobalPreLoadDataProvider
    {
        public static List<AsideMenu> AsideMenus { get; set; }

        public static List<AsideCreateController> AsideCreateControllers { get; set; }

        public static List<MusicSourceInfo> MusicSourceInfos { get; set; }
    }
}