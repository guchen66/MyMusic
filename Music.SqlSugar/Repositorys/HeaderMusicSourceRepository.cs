using Music.Shared.Attributes;
using Music.Shared.Entitys;
using Music.Shared.Entitys.Header;
using Music.SqlSugar.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Repositorys
{
    // [Scanning(RegisterType = "Scoped")]
    public class HeaderMusicSourceRepository : DataRepository<MusicSourceInfo>, IHeaderMusicSourceRepository
    {
    }
}
