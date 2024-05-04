
using Music.Shared.Attributes;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using MessageBox = System.Windows.MessageBox;

namespace Music.System.Services.MainSign.MyFavorSign
{
    [Scanning(RegisterType = "Register")]
    public class FavorService : IFavorService
    {
        public  Task<List<HongKongMusicDto>> GetHongKongList()
        {
            List<HongKongMusicDto> HonKongDataList = new List<HongKongMusicDto>();
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

                        HongKongMusicDto honKongData = new HongKongMusicDto();
                        honKongData.MusicAlbum = musicAlbum;
                        honKongData.MusicName = musicName;
                        honKongData.Singer = singer;
                        honKongData.MusicTime = time;
                        HonKongDataList.Add(honKongData);

                    }
                }
            }

            return Task.FromResult(HonKongDataList);    
        }
        public async Task<List<HongKongMusicDto>> GetHongKongListAsync()
        {
            List<HongKongMusicDto> HongKongDataList = new List<HongKongMusicDto>();

            // 使用 HttpClient 异步获取网页内容
            using (var httpClient = new HttpClient())
            {
                var path = "https://y.qq.com/n/ryqq/toplist/59/";
                if (path == null)
                {
                    return null;
                }
                try
                {
                    var html = await httpClient.GetStringAsync(path);
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);

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

                                HongKongMusicDto honKongData = new HongKongMusicDto();
                                honKongData.MusicAlbum = musicAlbum;
                                honKongData.MusicName = musicName;
                                honKongData.Singer = singer;
                                honKongData.MusicTime = time;
                                HongKongDataList.Add(honKongData);

                            }
                        }
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("qq音乐接口连接失败");
                }

            }

            return HongKongDataList;
        }
        public Task GetSongList()
        {
            return Task.FromResult(0);
        }
    }
}
