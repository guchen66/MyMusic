
namespace Music.Shared.Entitys
{
    public class MyBogusAttribute : Attribute
    {
        public bool Attri { get; set; } // 是否生成虚假数据
        public int Num { get; set; } // 虚假数据的数量

        public MyBogusAttribute(bool attri, int num)
        {
            Attri = attri;
            Num = num;
        }
    }

    //   [MyBogus(true, 100)]
    [SugarTable("MusicInfo", "音乐信息表")]//安全级别高，只创建，不修改和删除
    public class MusicInfo : AutoIncrementEntity
    {
        [SugarColumn(ColumnDataType = "Nvarchar(50)" ,ColumnDescription ="音乐名称")]//自定义情况Length不要设置
        public string? Name { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "歌手名称", IsNullable = true)]
        public string[]? Artists { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "专辑名称", IsNullable = true)]
        public string? Album { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", ColumnDescription = "音乐来源", IsNullable = true)]
        public string? SourceName { get; set; }
    }
}
