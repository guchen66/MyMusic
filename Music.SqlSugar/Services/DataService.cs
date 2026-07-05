using Music.SqlSugar.IRepositorys;

namespace Music.SqlSugar.Services
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class, new()
    {
        private IDataRepository<TEntity> db;            //这个注入是从子类来的

        public DataService(IDataRepository<TEntity> repository)
        {
            db = repository;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await db.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await db.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await db.DeleteByIdAsync(id);
        }

        public async Task<TEntity> QueryAsync(int id)
        {
            return await db.QueryByIdAsync(id);
        }

        public virtual async Task<List<TEntity>> QueryListAsync()
        {
            return await db.QueryListAsync();
        }

        public async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func)
        {
            return await db.QueryListAsync(func);
        }

        public async Task<List<TEntity>> QueryListAsync(int page, int size, RefAsync<int> total)
        {
            return await db.QueryListAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await db.QueryListAsync(page, size, total);
        }

        public async Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await db.QueryAsync(func);
        }

        public bool Add(TEntity entity)
        {
            return db.Add(entity);
        }

        public bool DeleteById(int id)
        {
            return db.DeleteById(id);
        }

        public TEntity QueryById(int id)
        {
            return db.QueryById(id);
        }

        public TEntity Query(Expression<Func<TEntity, bool>> func)
        {
            return db.Query(func);
        }

        public List<TEntity> QueryList()
        {
            return db.QueryList();
        }

        public List<TEntity> QueryList(Expression<Func<TEntity, bool>> func)
        {
            return db.QueryList(func);
        }

        public List<TEntity> QueryList(int page, int size, ref int total)
        {
            return db.QueryList(page, size, ref total);
        }

        public List<TEntity> QueryList(Expression<Func<TEntity, bool>> func, int page, int size, ref int total)
        {
            return db.QueryList(func, page, size, ref total);
        }

        public bool Update(TEntity entity)
        {
            return db.Update(entity);
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            return db.DeleteByIdAsync(id);
        }

        public Task<TEntity> QueryByIdAsync(int id)
        {
            return db.QueryByIdAsync(id);
        }
    }
}