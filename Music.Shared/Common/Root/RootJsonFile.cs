
namespace Music.Shared.Common.Root
{
    public class RootFile
    {
        public Type Type { get; set; }
        public RootJsonFile RootJsonFile { get; set; }

    }

    public class RootJsonFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }          //具体位置
        public string IsChanged { get; set; }       //Json文件是否改动过
    }


}
