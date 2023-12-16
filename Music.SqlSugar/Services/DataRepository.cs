using Music.SqlSugar.Db;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Services
{
    public class DataRepository<T> :SimpleClient<T> where T : class,new()
    {
        public ITenant itenant = null;
        public DataRepository(ISqlSugarClient db=null):base(db) 
        {
            Context = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConnectionDbConfig.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

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
