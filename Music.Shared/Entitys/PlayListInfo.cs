
namespace Music.Shared.Entitys
{
   
    [SugarTable("PlayListInfo", "左侧创建歌单表")]//安全级别高，只创建，不修改和删除
    public class PlayListInfo : NotAutoIncrementEntity
    {
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public virtual string? PlayListName { get; set; }

        [SugarColumn(IsNullable = true)]
        public int PlayListArgsId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(PlayListArgsId))]
        public EditPlayListInfo EditPlayListInfos { get; set; }

        [SugarColumn(IsNullable = true)]
        public int PlayListUiId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(PlayListUiId))]//一对一 SchoolId是StudentA类里面的
        public PlayListUiInfo PlayListUiInfos { get; set; }
    }


    [SugarTable("EditPlayListInfo", "左侧修改歌单表")]//安全级别高，只创建，不修改和删除
    public class EditPlayListInfo//: PlayListInfo
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity =true)]
        public  int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string? ReNameItem { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string? RemoveItem { get; set; }
    }
}
