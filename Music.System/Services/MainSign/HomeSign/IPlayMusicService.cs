using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.HomeSign
{
    public interface IPlayMusicService
    {
        Task<bool> SingleMusicPlay(string name);
    }
}
