using HtmlAgilityPack;
using Music.System.Services.MainSign.MyFavorSign.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Managers
{
    public class HtmlAgilityPackManager
    {

        public static void GetNetData()
        {
            // 获取网页的Html
            var html = @"https://y.qq.com/n/ryqq/toplist/59/";
            var web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("UTF-8"); // 指定字符集
            // web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299";
            var htmlDoc = web.Load(html);

            // 选择所有歌曲所在的 li 元素
            var songNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='topList_mod_songlist ']//li");

            //获取类名为songlist__list的所有ul
            var contentDiv = htmlDoc.DocumentNode.Descendants("ul")
                            .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains("songlist__list"));
            foreach (var songNode in contentDiv)
            {
                var songNameNode = songNode.SelectSingleNode(".//a[@title][@class='songlist__cover']"); // 获取class=songlist__cover的title
                var songName = songNameNode.GetAttributeValue("title", "");

                var artistNode = songNode.SelectSingleNode(".//a[@title][@class='playlist__author']"); // 获取歌手名称
                var artist = artistNode?.GetAttributeValue("title", "");

                var timeNode = songNode.SelectSingleNode(".//div[@class='songlist__time']"); // 获取时长
                var time = timeNode?.InnerText?.Trim();

                // 在这里处理获取到的歌曲信息，例如输出或存储到数据结构中
                Console.WriteLine("歌曲名称： " + songName);
                Console.WriteLine("歌手： " + artist);
                Console.WriteLine("时长： " + time);
                Console.WriteLine();
            }

        }


        /// <summary>
        /// 获取香港地区榜单
        /// </summary>
        public static List<HongKongDto> GetHonKongData()
        {
            List<HongKongDto> HonKongDataList = new List<HongKongDto>();
            // 获取网页的Html
            var html = @"https://y.qq.com/n/ryqq/toplist/59/";
            var web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("UTF-8"); // 指定字符集
            // web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299";
            var htmlDoc = web.Load(html);

            var contentDiv = htmlDoc.DocumentNode.Descendants("ul")
                         .Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains("songlist__list"));

            foreach (var ulNode in contentDiv)
            {
                var songNodes = ulNode.SelectNodes(".//li");
                if (songNodes != null)
                {
                    foreach (var songNode in songNodes)
                    {
                        //获取专辑名称
                        var songNameNode = songNode.SelectSingleNode(".//a[@title][@class='songlist__cover']");
                        var musicAlbum = songNameNode.GetAttributeValue("title", "");

                        //歌曲名称
                        var songNameNode2 = songNode.SelectSingleNode(".//span[@class='songlist__songname_txt']");
                        var musicName = songNameNode2?.InnerText?.Trim();

                        // 获取歌手名称
                        var artistNode = songNode.SelectSingleNode(".//a[@title][@class='playlist__author']");
                        var singer = artistNode?.GetAttributeValue("title", "");

                        var timeNode = songNode.SelectSingleNode(".//div[@class='songlist__time']"); // 获取时长
                        var time = timeNode?.InnerText?.Trim();

                        // 在这里处理获取到的歌曲信息，例如输出或存储到数据结构中
                        /*    Console.WriteLine("专辑名称： " + songName);
                            Console.WriteLine("歌曲名称： " + songName2);
                            Console.WriteLine("歌手： " + artist);
                            Console.WriteLine("时长： " + time);
                            Console.WriteLine();*/

                        HongKongDto honKongData = new HongKongDto();
                        honKongData.MusicAlbum = musicAlbum;
                        honKongData.MusicName = musicName;
                        honKongData.Singer = singer;
                        honKongData.MusicTime = time;
                        HonKongDataList.Add(honKongData);

                    }
                }
            }

            return HonKongDataList;
        }

    }
}
