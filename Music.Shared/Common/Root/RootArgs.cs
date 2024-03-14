
namespace Music.Shared.Common.Root
{
    public class RootArgs : BindableBase
    {
        public static readonly RootArgs Instance = new Lazy<RootArgs>(() => new RootArgs()).Value;
        public RootArgs()
        {
            Init();
        }
        public void Init()
        {
            RootJsonFileList = new ObservableCollection<RootJsonFile>
            {
                new RootJsonFile(){ Id=1, Name=""},

            };
        }

        public ObservableCollection<RootJsonFile> RootJsonFileList { get; set; }
    }
}
