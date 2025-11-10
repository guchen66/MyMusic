using Music.Core.Repositorys;
using Music.Shared.Dtos;
using Music.Shared.Entitys;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Sqlsugar.Repositorys
{
    public class LoginRepository : SimpleClient<LoginInputInfo>, ILoginRepository
    {
        public LoginRepository(ISqlSugarClient db = null) : base(db)
        {
            base.Context = DbScoped.Sugar;
        }

        public async Task<bool> AddAsync(LoginInputInfo entity)
        {
            return await base.InsertAsync(entity);
        }

        public override async Task<bool> UpdateAsync(LoginInputInfo entity)
        {
            return await base.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public virtual async Task<LoginInputInfo> QueryAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<List<LoginInputInfo>> QueryListAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<LoginInputInfo>> QueryListAsync(Expression<Func<LoginInputInfo, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<LoginInputInfo>> QueryListAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<LoginInputInfo>().ToPageListAsync(page, size, total);
        }

        public virtual async Task<List<LoginInputInfo>> QueryListAsync(Expression<Func<LoginInputInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<LoginInputInfo>().Where(func).ToPageListAsync(page, size, total);
        }

        public async Task<LoginInputInfo> QueryAsync(Expression<Func<LoginInputInfo, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }
    }
}