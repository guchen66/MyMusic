namespace Music.Shared.Dtos
{
    /// <summary>
    /// 左侧新建歌单的DTO
    /// </summary>
    public class PlayListInputDto : BaseDto
    {
        /// <summary>
        /// 歌单Id
        /// </summary>
        private int _playListId;

        public int PlayListId
        {
            get { return _playListId; }
            set { SetProperty(ref _playListId, value); }
        }

        /// <summary>
        /// 歌单名称
        /// </summary>
        private string _playListName;

        public string PlayListName
        {
            get { return _playListName; }
            set { SetProperty(ref _playListName, value); }
        }

        /// <summary>
        /// 歌单展示内容
        /// </summary>
        private PlayListShowContentDto _playListShowContentDto;

        public PlayListShowContentDto PlayListShowContentDtos
        {
            get { return _playListShowContentDto; }
            set { SetProperty(ref _playListShowContentDto, value); }
        }

        /* private PlayListEditDto _playListEditDto;

         public PlayListEditDto PlayListEditDtos
         {
             get { return _playListEditDto; }
             set { SetProperty<PlayListEditDto>(ref _playListEditDto, value); }
         }*/
    }

    /// <summary>
    /// 歌单展示内容
    /// </summary>
    public class PlayListShowContentDto : BaseDto
    {
        private int _musicId;

        public int Name
        {
            get { return _musicId; }
            set { SetProperty(ref _musicId, value); }
        }

        private string _musicName;

        public string MusicName
        {
            get { return _musicName; }
            set { SetProperty(ref _musicName, value); }
        }

        private string _artists;

        public string Artists
        {
            get { return _artists; }
            set { SetProperty(ref _artists, value); }
        }
        private string _album;

        public string Album
        {
            get { return _album; }
            set { SetProperty(ref _album, value); }
        }

        private string _sourceName;

        public string SourceName
        {
            get { return _sourceName; }
            set { SetProperty(ref _sourceName, value); }
        }
    }
}
