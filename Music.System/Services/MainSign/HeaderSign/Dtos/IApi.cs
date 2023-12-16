﻿
namespace Music.System.Services.MainSign.HeaderSign.Dtos
{
    public interface IApi
    {
        /// <summary>
        /// 获得音乐文件的链接
        /// To get the url of music files
        /// </summary>
        /// <returns>返回音乐链接</returns>
        string GetMusicUrl();

        /// <summary>
        /// 获得音乐封面链接
        /// To get the url of music covers
        /// </summary>
        /// <returns>返回封面链接</returns>
        string GetCoverUrl();

        /// <summary>
        /// 获得歌词
        /// </summary>
        /// <returns></returns>
        string GetLyric();
    }
}
