using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Entitys
{
    /// <summary>
    /// 收藏的歌单信息
    /// </summary>
    public class FavorPlayListInfo : AutoIncrementEntity
    {
        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "音乐名称")]//自定义情况Length不要设置
        public string? Name { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "歌手名称", IsNullable = true)]
        public string[]? Artists { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "专辑名称", IsNullable = true)]
        public string? Album { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "音乐来源", IsNullable = true)]
        public string? SourceName { get; set; }
    }
}
