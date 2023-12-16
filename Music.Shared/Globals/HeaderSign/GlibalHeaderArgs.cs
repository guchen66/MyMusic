using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Music.Shared.Globals.HeaderSign
{
    public class GlibalHeaderArgs : BindableBase
    {
        public static readonly GlibalHeaderArgs Instance = new Lazy<GlibalHeaderArgs>(() => new GlibalHeaderArgs()).Value;
        public GlibalHeaderArgs()
        {
            Init();
        }
        public void Init()
        {
            MusicSourceList = new ObservableCollection<MusicSourceArgs>
            {
                new MusicSourceArgs(){ Id=1, Name="网易云",Source="网易云"},
                new MusicSourceArgs(){ Id=2, Name="QQ音乐",Source = "QQ音乐"},
                new MusicSourceArgs(){ Id=3, Name="酷狗音乐",Source = "酷狗音乐"}
            };
        }

        public ObservableCollection<MusicSourceArgs> MusicSourceList { get; set; }
    }
}
