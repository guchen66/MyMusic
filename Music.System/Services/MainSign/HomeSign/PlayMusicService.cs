using Music.SqlSugar.IServices;
using Music.SqlSugar.Services;
using Music.System.Services.MainSign.FooterSign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Music.System.Services.MainSign.HomeSign
{
    public class PlayMusicService : IPlayMusicService
    {
        private static IWavePlayer player = new WaveOutEvent();
        private static MediaFoundationReader reader = null;
        private static SampleChannel channel = null;
        private static SampleAggregator aggregator = null;
        IHttpClientService httpClientService;
        IHeaderMusicSourceService _headerMusicSourceService;
        public static PlaybackState State
        {
            get { return player.PlaybackState; }
        }
        public async Task<bool> SingleMusicPlay(string name)
        {
            httpClientService = ContainerLocator.Container.Resolve<IHttpClientService>();
            _headerMusicSourceService = ContainerLocator.Container.Resolve<IHeaderMusicSourceService>();

            HttpClientDto httpClientDto = new HttpClientDto();
            // string url = "http://v.api.aa1.cn/api/wymusic/index.php";
            string url = "https://tqlcode.com/page/music/api.php";
            byte[] commit = Encoding.UTF8.GetBytes($"types=url&name={name}&source=netease");
            byte[] data = await httpClientService.PostAsync(url, commit, httpClientDto);
            string s = Encoding.UTF8.GetString(data);
            JObject json = JObject.Parse(Encoding.UTF8.GetString(data));
            var Source = json["url"].ToString();
            AudioFileReader audioFileReader;
            try
            {
                if (State == PlaybackState.Stopped)
                {
                    audioFileReader = new AudioFileReader(Source);
                    /*  reader = new MediaFoundationReader(Source);
                      channel = new SampleChannel(reader);
                      aggregator = new SampleAggregator(channel);
                      aggregator.NotificationCount = reader.WaveFormat.SampleRate / 100;
                      aggregator.PerformFFT = true;*/
                    //  aggregator.FftCalculated += DrawFFT;
                    player.Init(audioFileReader);
                }

                player.Play();
            }
            catch (Exception e)
            {
              //  MessageBox.Show($"{e.Message}暂不支持提供播放001");
            }

           return true;
        }
    }
}
