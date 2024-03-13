using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.EmptyPlayListSign
{
    public interface IMusicPlay
    {
        /// <summary>
        /// 播放全部
        /// </summary>
        /// <returns></returns>
        Task PlayAll();

        /// <summary>
        /// 通过Id播放
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PlayByIdAsync(long id);

        /// <summary>
        /// 通过Name播放
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task PlayByNameAsync(string name);

        /// <summary>
        /// 全部暂停
        /// </summary>
        /// <returns></returns>
        Task StopAll();

        /// <summary>
        /// 通过Id暂停
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task StopByIdAsync(long id);

        /// <summary>
        /// 通过Name暂停
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task StopByNameAsync(string name);
    }
}
