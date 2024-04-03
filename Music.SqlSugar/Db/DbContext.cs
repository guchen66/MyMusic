using Music.Shared.Entitys.Header;
using NewLife.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.SqlSugar.Db
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = GetConnectionObject(),
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据

        public SimpleClient<MusicSourceInfo> SourceDb { get { return new SimpleClient<MusicSourceInfo>(Db); } }//用来处理MusicSourceInfo表的常用操作

        public static string GetConnectionObject()
        {
            string result = "";
            var provider = new JsonConfigProvider() { FileName = "AppConfig.json" };
            result = provider.GetSection("SqlConnection:ConnectionMMsql").Value;
            return result;
        }

    }
}
