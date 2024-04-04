
namespace Music.Shared.Entitys.Header
{
    [SugarTable("MusicSourceInfo", "音乐来源")]
    public class MusicSourceInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? Key { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? SourceName { get; set; }

        /// <summary>
        /// 新增的点击，最好设置默认值，否则重新建表会报错
        /// </summary>
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool? IsSelected { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string? Site { get; set; }
    }
}
