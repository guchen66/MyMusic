
namespace Music.Shared.Entitys
{
    [SugarTable("PlayListUiInfo", "歌单Ui信息表")]
    public class PlayListUiInfo : BaseEntity
    {

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public virtual string? PlayListName { get; set; }

        /// <summary>
        /// 音乐链接ID
        /// </summary>
        public string MusicId { get; set; }

        /// <summary>
        /// 封面id，只有网易云需要，其他音乐默认为ID
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string CoverId { get; set; }

        /// <summary>
        /// 用于正确产生接口产生的艺术家数组名称
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string Singer {  get; set; }

        /// <summary>
        /// 音乐名称
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string MusicName { get; set; }

        /// <summary>
        /// 音乐专辑
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string MusicAlbum { get; set; }

        /// <summary>
        /// 音乐时长
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string MusicTime { get; set; }
    }

}
