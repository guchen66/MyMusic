using Music.System.Services.MainSign.FooterSign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.CustomPlaySign
{
    public interface IBasePlayService
    {
        public static IWavePlayer player = new WaveOutEvent();
        public static MediaFoundationReader reader = null;
        public static SampleChannel channel = null;
        public static SampleAggregator aggregator = null;


        Task PlayListAsync(string id);

        public static void Pause()
        {
            if (player == null) { return; }
            player.Pause();
        }

    }
}
