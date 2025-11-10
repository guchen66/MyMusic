using Music.Shared.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Sqlsugar.Extensions
{
    public static class QueryFilterExtensions
    {
        public static void GlobalFilter(this ISqlSugarClient client)
        {
            // DbScoped.Sugar.QueryFilter.AddTableFilter<UserInfo>(it => it.IsDelete == "0");
            //client.QueryFilter.Add(new TableFilterItem<LoginInputInfo>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<WareHouse>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<WorkStation>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<WorkStep>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<RoleInfo>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<GoodInfo>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<DeviceInfo>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<ProductType>(it => it.IsDelete == "0" || it.IsDelete == null));
            //client.QueryFilter.Add(new TableFilterItem<ProductDataConfig>(it => it.IsDelete == "0" || it.IsDelete == null));
        }
    }
}