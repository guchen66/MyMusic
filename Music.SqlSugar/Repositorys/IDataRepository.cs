using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Repositorys
{
    /// <summary>
    /// 定义一个仓储层
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataRepository<TEntity> where TEntity : class, new()
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
