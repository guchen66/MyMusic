
namespace Music.Shared.Globals.MainSign.Settings
{
    public class SetDetailData : BindableBase
    {
        public static readonly SetDetailData Instance = new Lazy<SetDetailData>(() => new SetDetailData()).Value;
        public SetDetailData()
        {
            Init();
            //  LoadState();
        }

        public ObservableCollection<CommonSetInfo> CommonSetInfos { get; set; }
        public ObservableCollection<DownLoadInfo> DownLoadInfos { get; set; }
        public ObservableCollection<LyricsSetInfo> LyricsSetRowInfos { get; set; }
        public ObservableCollection<LyricsSetInfo> LyricsSetAlignInfos { get; set; }
        public void Init()
        {
            CommonSetInfos = new ObservableCollection<CommonSetInfo>()
            {
                 new CommonSetInfo(){ Id=1, IsSelected=false ,TextContent="开机自动启动音乐"},
                 new CommonSetInfo(){ Id=2, IsSelected=false ,TextContent="自动播放音乐"},
                 new CommonSetInfo(){ Id=3, IsSelected=false ,TextContent="自动打开歌词"},
                 new CommonSetInfo(){ Id=4, IsSelected=false ,TextContent="禁用屏幕DPI适配"},
                 new CommonSetInfo(){ Id=5, IsSelected=false ,TextContent="禁用硬件加速"},


            };
            DownLoadInfos = new ObservableCollection<DownLoadInfo>()
            {
                 new DownLoadInfo(){ Id=6, IsSelected=false ,TextContent="不分文件夹"},
                 new DownLoadInfo(){ Id=7, IsSelected=false ,TextContent="按歌手分文件夹"},
                 new DownLoadInfo(){ Id=8, IsSelected=false ,TextContent="按专辑分文件夹"},
            };

            LyricsSetRowInfos = new ObservableCollection<LyricsSetInfo>()
            {
                 new LyricsSetInfo(){ Id=10, IsSelected=false ,TextContent="不分文件夹"},
                 new LyricsSetInfo(){ Id=11, IsSelected=false ,TextContent="按歌手分文件夹"},
                 new LyricsSetInfo(){ Id=12, IsSelected=false ,TextContent="按专辑分文件夹"},
            };

            LyricsSetAlignInfos = new ObservableCollection<LyricsSetInfo>()
            {
                 new LyricsSetInfo(){ Id=13, IsSelected=false ,TextContent="不分文件夹"},
                 new LyricsSetInfo(){ Id=14, IsSelected=false ,TextContent="按歌手分文件夹"},
                 new LyricsSetInfo(){ Id=15, IsSelected=false ,TextContent="按专辑分文件夹"},
            };
        }

    }
}
