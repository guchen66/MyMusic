using IT.Tangdao.Core.Paths;
using Music.Shared.Enums;

namespace Music.Shared.Locals
{
    /// <summary>
    /// 本地音乐信息
    /// </summary>
    public class LocalMusicInfo
    {
        /// <summary>
        /// 唯一标识符，统一数据库DB和DTO
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 本地音乐路
        /// </summary>
        public AbsolutePath AbsolutePath { get; set; }

        /// <summary>
        /// 音乐名称
        /// </summary>
        public string MusicName { get; set; }

        /// <summary>
        /// 本地音乐类型
        /// </summary>
        public LocalMusicType LocalMusicType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}