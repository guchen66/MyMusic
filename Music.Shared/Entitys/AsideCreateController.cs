
namespace Music.Shared.Entitys
{
    [SugarTable("AsideCreateController", "左侧创建歌单表")]//安全级别高，只创建，不修改和删除
    public class AsideCreateController : AutoIncrementEntity
    {
        [SugarColumn(ColumnDescription = "歌单控制器Id")]
        public int ControllerId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(20)", ColumnDescription = "歌单名称")]//自定义情况Length不要设置
        public string? Name { get; set; }

        [SugarColumn(ColumnDescription = "是否填充内容View", IsNullable = false)]
        public bool? IsFullContent { get; set; }
      
        public int AsideControlViewId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(AsideControlViewId))]//一对一 SchoolId是StudentA类里面的
        public AsideControlView AsideControlView { get; set; } //不能赋值只能是null
    }
}
