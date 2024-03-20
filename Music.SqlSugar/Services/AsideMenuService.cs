using Music.Shared.Attributes;
using Music.Shared.Entitys;
using Music.SqlSugar.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Services
{
   // [Scanning(RegisterType = "Scoped")]
    public class AsideMenuService:DataService<AsideMenu>, IAsideMenuService
    {
        private readonly IAsideMenuRepository _db;
        public AsideMenuService(IAsideMenuRepository db)
        {
            base.db= db;
            _db = db;
        }
    }
}
