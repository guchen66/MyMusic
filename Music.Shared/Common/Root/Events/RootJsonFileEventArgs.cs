using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
