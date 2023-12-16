
namespace Music.System.Services.MainSign.MyFavorSign
{
    public interface IFavorService
    {
        /// <summary>
        /// 获取港台歌曲
        /// </summary>
        /// <returns></returns>
        Task<List<HongKongDto>> GetHongKongListAsync();
        Task GetSongList();
    }
}
