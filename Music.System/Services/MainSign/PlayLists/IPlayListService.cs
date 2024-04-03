

namespace Music.System.Services.MainSign.PlayLists
{
    public interface IPlayListService
    {
        /// <summary>
        /// 创建歌单
        /// </summary>
        /// <returns></returns>
        //public Task<bool> CreatePlatList(PlayListInputDto input);
        public Task<bool> CreatePlatListAsync(PlayListInputDto input);

        /// <summary>
        /// 移除歌单
        /// </summary>
        /// <returns></returns>
        public Task<bool> RemovePlatListById(long? id);

        /// <summary>
        /// 获取所有歌单
        /// </summary>
        /// <returns></returns>
        public Task<List<PlayListInfo>> GetAllPlayList();

        /// <summary>
        /// 跟据Id获取歌单
        /// </summary>
        /// <returns></returns>
        public Task<PlayListInfo> GetPlayListByIdAsync(long? id);

        /// <summary>
        /// 跟据Name获取歌单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<PlayListInfo> GetPlatListByNameAsync(string name);

        /// <summary>
        /// 修改歌单名称----重命名歌单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> GetReNamePlatListAsync(PlayListInfo playListInfo);
    }
}
