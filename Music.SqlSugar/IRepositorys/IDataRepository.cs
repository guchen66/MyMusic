using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.IRepositorys
{
    /// <summary>
    /// 定义一个仓储层
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataRepository<TEntity> where TEntity : class, new()
    {
        #region 同步增删改查

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool DeleteById(int id);

        TEntity QueryById(int id);

        TEntity Query(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TEntity> QueryList();

        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<TEntity> QueryList(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<TEntity> QueryList(int page, int size, ref int total);

        /// <summary>
        /// 自定义条件分页查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<TEntity> QueryList(Expression<Func<TEntity, bool>> func, int page, int size, ref int total);

        #endregion 同步增删改查

        #region 异步增删改查

        Task<bool> AddAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<TEntity> QueryByIdAsync(int id);

        Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> func);

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

        #endregion 异步增删改查
    }
}