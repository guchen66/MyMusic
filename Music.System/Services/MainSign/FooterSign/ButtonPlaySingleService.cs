

namespace Music.System.Services.MainSign.FooterSign
{
    [Scanning(RegisterType = "Register")]
    public class ButtonPlaySingleService : IButtonPlaySingleService
    {
        private static IWavePlayer player = new WaveOutEvent();
        private static MediaFoundationReader reader = null;
        private static SampleChannel channel = null;
        private static SampleAggregator aggregator = null;
        IHttpClientService httpClientService;
        public bool IsPlay;

        public static PlaybackState State
        {
            get { return player.PlaybackState; }
        }
        public async Task PlayListAsync(string id)
        {
            httpClientService = ContainerLocator.Container.Resolve<IHttpClientService>();
            HttpClientDto httpClientDto = new HttpClientDto();
           // string url = "http://v.api.aa1.cn/api/wymusic/index.php";
            string url = "https://tqlcode.com/page/music/api.php";
            byte[] commit = Encoding.UTF8.GetBytes($"types=url&id={id}&source=netease");
            byte[] data = await httpClientService.PostAsync(url, commit, httpClientDto);
            string s= Encoding.UTF8.GetString(data);
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
                MessageBox.Show("VIP歌曲，暂不支持提供播放");
            }

        }

        public Task PlayListByIdAsync(int id)
        {
            return Task.FromResult(0);
        }

        public Task PlayListByNameAsync(string name)
        {
            return Task.FromResult(0);
        }

        public void StopPlayAsync()
        {
            if (player == null) { return; }
            player.Pause(); // 使用Pause()方法暂停播放器
         
        }

        public Task StopPlayByIdAsync(int id)
        {
            return Task.FromResult(0);
        }

        public Task StopPlayByNameAsync(string name)
        {
            return Task.FromResult(0);
        }
    }
}
