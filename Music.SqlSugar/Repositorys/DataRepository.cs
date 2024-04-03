using Music.Shared.Common;
using Music.Shared.Entitys;
using Music.SqlSugar.Db;
using Music.SqlSugar.IRepositorys;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Repositorys
{
    public class DataRepository<TEntity> : SimpleClient<TEntity>,IDataRepository<TEntity> where TEntity : class, new()
    {
        public DataRepository(ISqlSugarClient db = null) : base(db)
        {
            base.Context = DbScoped.Sugar;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public virtual async Task<TEntity> QueryAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<List<TEntity>> QueryListAsync()
        {
            return await base.GetListAsync();

        }

        public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryListAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().ToPageListAsync(page, size, total);
        }

        public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page, size, total);
        }

        public async Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }
    }
}
