
namespace Music.System.Services.MainSign.MyFavorSign.Dtos
{
    public class FavorDto : BaseDto
    {
        /// <summary>
        /// 歌曲
        /// </summary>
        private string _song;

        public string Song
        {
            get { return _song; }
            set { SetProperty(ref _song, value); }
        }

        /// <summary>
        /// 专辑
        /// </summary>
        private string _album;

        public string Album
        {
            get { return _album; }
            set { SetProperty(ref _album, value); }
        }

        /// <summary>
        /// 歌手
        /// </summary>
        private string[] _singer;

        public string Singer
        {
            get
            {
                string value = string.Empty;
                foreach (string singer in _singer) { value += "、" + singer; }
                if (value != string.Empty) { value = value.Substring(1); }
                return value;
            }
            set
            {
                _singer = value.Split('、');
            }
        }

    }
}
