
using Music.Core.Helpers;
using Music.Shared.Attributes;
using Music.SqlSugar.Db;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using MessageBox = System.Windows.MessageBox;

namespace Music.System.Services.MainSign.MyFavorSign
{
    [Scanning(RegisterType = "Register")]
    public class FavorService : IFavorService
    {

        List<HongKongMusicDto> HongKongDataList;
        public async Task<List<HongKongMusicDto>> GetHongKongListAsync()
        {
            HongKongDataList = new List<HongKongMusicDto>();
            var ping=  NetConnHelper.CheckPing();
            if (ping)
            {
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


                    }

                }
            }
            else
            {
                MessageBox.Show("网络未连接");
            }

            // 使用 HttpClient 异步获取网页内容
          

            return HongKongDataList;
        }

        public Task GetSongList()
        {
            return Task.FromResult(0);
        }

        
        public void AddPlayListToFavor(string musicName)
        {
            HongKongMusicDto musicDto = HongKongDataList.Where(x => x.MusicName == musicName).FirstOrDefault();
            FavorPlayListInfo favorPlayListInfo=new FavorPlayListInfo();
            favorPlayListInfo.Name = musicDto.MusicName;
            // favorPlayListInfo.Artists = musicDto.MusicAlbum;
            GetFavorDbContext.Context.Insertable(favorPlayListInfo).ExecuteCommand();
        }

        public DbContext<FavorPlayListInfo> GetFavorDbContext=> ContainerLocator.Container.Resolve<DbContext<FavorPlayListInfo>>();

    }
}
