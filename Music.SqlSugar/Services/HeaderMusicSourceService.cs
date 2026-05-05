using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Enums;
using Music.Shared.Attributes;
using Music.Shared.Entitys.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Services
{
    [AutoRegister(Mode = RegisterMode.Singleton)]
    public class HeaderMusicSourceService : DataService<MusicSourceInfo>, IHeaderMusicSourceService
    {
        private readonly IHeaderMusicSourceRepository _db;

        public HeaderMusicSourceService(IHeaderMusicSourceRepository repository) : base(repository)
        {
            _db = repository;
        }
    }
}