
namespace Music.SqlSugar.IServices
{
    /// <summary>
    /// 定义一个服务层
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataService<TEntity> where TEntity : class, new()
    {
        #region 增删改查

        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<TEntity> QueryAsync(int id);
        Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> func);
        #endregion

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryListAsync();

        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryListAsync(int page, int size, RefAsync<int> total);

        /// <summary>
        /// 自定义条件分页查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);
    }
}
