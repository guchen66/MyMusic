
namespace Music.Shared.Common.Root.Events
{
    public class RootJsonFileEventArgs : EventArgs
    {
        public RootJsonFile DefaultJsonFile { get; set; }
        public RootJsonFileEventArgs(RootJsonFile rootJsonFile)
        {
            DefaultJsonFile = rootJsonFile;
        }
    }
}
