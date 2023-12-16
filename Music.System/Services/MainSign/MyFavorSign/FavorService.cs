
namespace Music.System.Services.MainSign.MyFavorSign
{
    public class FavorService : IFavorService
    {
        public async Task<List<HongKongDto>> GetHongKongList()
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
        public async Task<List<HongKongDto>> GetHongKongListAsync()
        {
            List<HongKongDto> HongKongDataList = new List<HongKongDto>();

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

                                HongKongDto honKongData = new HongKongDto();
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
