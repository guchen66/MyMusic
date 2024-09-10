using Music.Shared.Args;
using Music.Shared.Locals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.DownLoads
{
    public class ReadService : IReadService
    {
        public IReadService AddLocalFile(LocalMusicFile fileName)
        {
            return LocalMusicFileExtension.Get<IReadService>(fileName);
        }

        public LocalMusicFile GetLocalMusicFile()
        {
            return LocalMusicFileExtension.Get<LocalMusicFile>();
        }

        public async Task<ApiResponse> ReadLocalFile(string fileType)
        {
            await Task.Delay(1000);
            var result = "" as object;
            return new ApiResponse(true,result);
        }
    }

    public static class LocalMusicFileExtension
    {
        public static T Get<T>()
        {
            return default(T);
        }
        public static T Get<T>(LocalMusicFile fileName)
        {
            return default(T);
        }
    }


}
