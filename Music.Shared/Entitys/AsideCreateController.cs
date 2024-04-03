using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Entitys
{
    [SugarTable("AsideCreateController", "左侧创建歌单表")]//安全级别高，只创建，不修改和删除
    public class AsideCreateController : BaseEntity
    {
        /*  [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true,ColumnDescription ="歌单名称")]
          public string? PlayListName { get; set; }*/

        //[SugarColumn(IsNullable = true)]
        //public int PlayListArgsId { get; set; }

        /*  [Navigate(NavigateType.OneToOne, nameof(PlayListArgsId))]
          public EditPlayListInfo EditPlayListInfos { get; set; }

          [SugarColumn(IsNullable = true)]
          public int PlayListUiId { get; set; }

          [Navigate(NavigateType.OneToOne, nameof(PlayListUiId))]//一对一 SchoolId是StudentA类里面的
          public PlayListUiInfo PlayListUiInfos { get; set; }*/

        //[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public int ControllerId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "音乐名称")]//自定义情况Length不要设置
        public string? Name { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "歌手名称", IsNullable = true)]
        public string[]? Artists { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "专辑名称", IsNullable = true)]
        public string? Album { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "音乐来源", IsNullable = true)]
        public string? SourceName { get; set; }

        [SugarColumn(ColumnDescription = "是否填充内容View", IsNullable = false)]
        public bool? IsFullContent { get; set; } 
    }
}
