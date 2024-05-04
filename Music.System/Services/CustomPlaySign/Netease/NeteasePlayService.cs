using Music.System.Services.MainSign.FooterSign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.CustomPlaySign.Netease
{
    public class NeteasePlayService:INeteasePlayService
    {

        IHttpClientService httpClientService;
        public bool IsPlay;

        public static PlaybackState State
        {
            
            get { return IBasePlayService.player.PlaybackState; }
        }
        public async Task PlayListAsync(string id)
        {
            
            httpClientService = ContainerLocator.Container.Resolve<IHttpClientService>();
            string url = "https://tqlcode.com/page/music/api.php";
            byte[] commit = Encoding.UTF8.GetBytes($"types=url&id={id}&source=netease");
            byte[] data = await httpClientService.PostAsync(url, commit);
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
                    IBasePlayService.player.Init(audioFileReader);
                }

                IBasePlayService.player.Play();
            }
            catch (Exception e)
            {
               // MessageBox.Show($"{e.Message}暂不支持提供播放003");
            }

        }
    }
}
