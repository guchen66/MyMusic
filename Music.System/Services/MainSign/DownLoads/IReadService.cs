using Music.Shared.Args;
using Music.Shared.Locals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.DownLoads
{
    public interface IReadService
    {
        public Task<ApiResponse> ReadLocalFile(string fileType);

        public LocalMusicFile GetLocalMusicFile();

        public IReadService AddLocalFile(LocalMusicFile fileName);
    }
}
