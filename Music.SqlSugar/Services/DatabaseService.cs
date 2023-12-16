using Music.SqlSugar.Db;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Services
{
    public class DatabaseService
    {
        private static  Lazy<SqlSugarClient> _db => new Lazy<SqlSugarClient>(() =>
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
