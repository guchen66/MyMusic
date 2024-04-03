
using Music.Shared.Dtos;

namespace Music.SqlSugar.IServices
{
    public interface IAsideCreateControlService : IDataService<AsideCreateController>
    {
        /// <summary>
        /// 创建歌单
        /// </summary>
        /// <returns></returns>
        //public Task<bool> CreatePlatList(PlayListInputDto input);
        public Task<bool> CreatePlatListAsync(AsideCreateControllerDto input);

        /// <summary>
        /// 移除歌单
        /// </summary>
        /// <returns></returns>
        public Task<bool> RemovePlatListById(long? id);

 
        /// <summary>
        /// 跟据Id获取歌单
        /// </summary>
        /// <returns></returns>
        public Task<AsideCreateControllerDto> GetPlayListByIdAsync(long? id);

        /// <summary>
        /// 跟据Name获取歌单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<AsideCreateControllerDto> GetPlatListByNameAsync(string name);

        /// <summary>
        /// 修改歌单名称----重命名歌单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> GetReNamePlatListAsync(AsideCreateController playListInfo);
    }
}
