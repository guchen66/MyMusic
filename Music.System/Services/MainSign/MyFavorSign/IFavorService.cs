using Music.SqlSugar.Db;

namespace Music.System.Services.MainSign.MyFavorSign
{
    public interface IFavorService
    {
        /// <summary>
        /// 获取港台歌曲
        /// </summary>
        /// <returns></returns>
        Task<List<MusicInfoDto>> GetHongKongListAsync();

        void AddPlayListToFavor(string musicName);

        Task<bool> FlushAsync();

        Task GetSongList();

        DbContext<FavorPlayListInfo> GetFavorDbContext { get; }
    }
}