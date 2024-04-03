using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.HeaderSign.Dtos
{
    public class MusicSourceManager
    {
        public IEnumerable<MusicSourceDto> service;
        public MusicSourceManager()
        {
            service = new List<MusicSourceDto>();
        }

        public List<MusicSourceDto> QueryMusicSource()
        {
           
            return service.ToList();
        }
    }
}
