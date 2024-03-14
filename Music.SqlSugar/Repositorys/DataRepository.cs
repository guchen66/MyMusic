using Music.Shared.Common;
using Music.Shared.Entitys;
using Music.SqlSugar.Db;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Repositorys
{
    public class DataRepository<T> : SimpleClient<T> where T : class, new()
    {
        public ITenant itenant = null;
        public DataRepository(ISqlSugarClient db = null) : base(db)
        {
          /*  Context = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConnectionDbConfig.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });*/
            base.Context = DbScoped.Sugar;
            //创建数据库
            if (GeneratorDataProvider.IsGenerated)
            {
                base.Context.DbMaintenance.CreateDatabase();

                //创建表
                base.Context.CodeFirst.InitTables(
                    typeof(AsideMenu));
            }
            //生成种子数据
            if (GeneratorDataProvider.IsSeedData)
            {
                // AddSeedData();
            }
        }


        private static readonly Lazy<SqlSugarClient> _db = new Lazy<SqlSugarClient>(() =>
        {
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConnectionDbConfig.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            // 配置 SqlSugarClient 对象

            return db;
        });

        public static SqlSugarClient GetClient()
        {
            return _db.Value;
        }
    }
}
