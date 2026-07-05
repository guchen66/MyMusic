using Music.SqlSugar.IRepositorys;
using NewLife.Log;
using System.Diagnostics;

namespace Music.SqlSugar.Services
{
    // [Scanning(RegisterType = "Scoped")]
    public class AsideMenuService : DataService<AsideMenu>, IAsideMenuService
    {
        private readonly IAsideMenuRepository _db;

        public AsideMenuService(IAsideMenuRepository repository) : base(repository)
        {
            _db = repository;
        }
    }
}