using Music.Shared.Entitys;
using Music.SqlSugar.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Services
{
     public class AsideCreateControlService : DataService<AsideCreateControl>, IAsideCreateControlService
    {
        private readonly IAsideCreateControlRepository _db;
        public AsideCreateControlService(IAsideCreateControlRepository db)
        {
            base.db = db;
            _db = db;
        }
    }
}
