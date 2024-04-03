using Music.SqlSugar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Db
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> QueryListAsync();
    }

    public class BaseRepository<TEntity> : DbContext<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        public async Task<List<TEntity>> QueryListAsync()
        {
            return await Db.Queryable<TEntity>().ToListAsync();
        }
    }
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> QueryListAsync();
    }

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly IDataRepository<TEntity> db;

        public async Task<List<TEntity>> QueryListAsync()
        {
            return await db.QueryListAsync();
        }
    }
    public interface IAsideMenuService2 : IBaseService<AsideMenu>
    {

    }
    public class AsideMenuService2 : BaseService<AsideMenu>, IAsideMenuService2
    {
       /* private readonly IAsideMenuRepository _db;
        public AsideMenuService(IAsideMenuRepository db)
        {
            base.db = db;
            _db = db;
        }*/
    }
}