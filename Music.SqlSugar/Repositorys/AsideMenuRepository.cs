using Music.Shared.Attributes;
using Music.Shared.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Repositorys
{
    [Scanning(RegisterType = "Scoped")]
    public class AsideMenuRepository:DataRepository<AsideMenu>,IAsideMenuRepository
    {
    }
}
