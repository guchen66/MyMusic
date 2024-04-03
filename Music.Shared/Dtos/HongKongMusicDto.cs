using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Dtos
{
    /// <summary>
    /// 个人喜欢港曲，所以推荐列表获取的是港区的
    /// </summary>
    public class HongKongMusicDto : BaseDto
    {
        /// <summary>
        /// 音乐链接ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 封面id，只有网易云需要，其他音乐默认为ID
        /// </summary>
        public string CoverId { get; set; }

        /// <summary>
        /// 用于正确产生接口产生的艺术家数组名称
        /// </summary>
        public string Singer
        {
            get;
            set;
        }

        /// <summary>
        /// 音乐名称
        /// </summary>
        public string MusicName { get; set; }

        /// <summary>
        /// 音乐专辑
        /// </summary>
        public string MusicAlbum { get; set; }

        /// <summary>
        /// 音乐时长
        /// </summary>
        public string MusicTime { get; set; }

        /// <summary>
        /// 音乐来源
        /// </summary>
      //  public MusicSource Origin { get; }
    }
}
