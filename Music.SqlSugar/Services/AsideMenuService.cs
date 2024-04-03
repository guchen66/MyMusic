
using Music.SqlSugar.IRepositorys;

namespace Music.SqlSugar.Services
{
    // [Scanning(RegisterType = "Scoped")]
    public class AsideMenuService:DataService<AsideMenu>, IAsideMenuService
    {
        private readonly IAsideMenuRepository _db;
        public AsideMenuService(IAsideMenuRepository repository) :base(repository)
        {
            _db= repository;
        }
    }
}
