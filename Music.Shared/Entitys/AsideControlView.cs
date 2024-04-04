
namespace Music.Shared.Entitys
{
    public class AsideControlView : AutoIncrementEntity
    {
        [SugarColumn(ColumnDescription = "歌单控制器对应视图Id")]
        public int ViewId { get; set; }

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
