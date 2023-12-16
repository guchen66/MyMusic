using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.FooterSign
{
    public interface IButtonPlaySingleService
    {
        /// <summary>
        /// 按顺序全部播放
        /// </summary>
        /// <returns></returns>
        Task PlayListAsync(string id);

        /// <summary>
        /// 跟据Id播放当前歌曲
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PlayListByIdAsync(int id);

        /// <summary>
        /// 跟据Name播放当前歌曲
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PlayListByNameAsync(string name);

        /// <summary>
        /// 全部暂停
        /// </summary>
        /// <returns></returns>
        void StopPlayAsync();

        /// <summary>
        /// 跟据Id暂停当前歌曲
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task StopPlayByIdAsync(int id);

        /// <summary>
        /// 跟据Name暂停当前歌曲
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task StopPlayByNameAsync(string name);
    }
}
