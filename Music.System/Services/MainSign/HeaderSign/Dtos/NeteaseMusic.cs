
namespace Music.System.Services.MainSign.HeaderSign.Dtos
{
    public class NeteaseMusic : IMusic//, IApi
    {
        public string id { get; set; }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string name { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string[] artist { get; set; }
        public string Artists
        {
            get
            {
                string value = string.Empty;
                foreach (var tmp in artist) value += "、" + tmp;
                if (value != string.Empty) { value = value.Substring(1); }
                return value;
            }
            set
            {
                artist = value.Split('、');
            }
        }
        private string _duration;

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }


        public string album { get; set; }
        public string Album
        {
            get { return album; }
            set { album = value; }
        }
        public string url_id { get; set; }
        public string pic_id { get; set; }
        public string lyric_id { get; set; }
        public string source { get; set; }
        public string SourceName { get { return "网易云"; } }
        public MusicSource Origin { get { return MusicSource.Netease; } }
        public string CoverId { get => pic_id; set => pic_id = value; }

        public NeteaseMusic(IMusic music)
        {
            Id = music.Id;
            Name = music.Name;
            Album = music.Album;
            Artists = music.Artists;
            CoverId = music.CoverId;
        }
      
        public NeteaseMusic() { }
    }
}
