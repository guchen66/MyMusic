using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Locals
{
    public class LocalMusicFile
    {
        public int LocalId {  get; set; }
        public string Name { get; set; }
        public LocalMusicType LocalMusicType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
    public enum LocalMusicType
    {
        mp3,wav,ogg,m4a
    }
}
