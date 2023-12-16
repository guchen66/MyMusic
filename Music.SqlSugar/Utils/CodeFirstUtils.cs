/*using Music.Core.Components;
using Music.Core.Options;
using Music.SqlSugar.Entity;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using DbType = SqlSugar.DbType;

namespace Music.SqlSugar.Utils
{
    public static class CodeFirstUtils
    {
       *//* public static void CodeFirst(BaseOption options, string assemblyName)
        {
            if (options.InitTable)//如果需要初始化表结构
            {
                InitTable(assemblyName);
            }
            if (options.InitSeedData)
            {
                InitSeedData(assemblyName);
            }
        }*//*
        public static void GreateDbAndTableByCode()
        {
            //代码先行，通过代码创建数据库和表
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = "Data Source=.;" +
                                    "Initial Catalog=MusicDB;" +
                                    //"Integrated Security=True"+集成验证
                                    "User ID=sa;" +
                                    "Password=211314",
                InitKeyType = InitKeyType.Attribute,//释放事务
                IsAutoCloseConnection = true//读取主键自增
            });

            //CodeFirst代码先行，不用关心数据库，只需要写后台代码，需要什么对象就定义什么对象，也可以一次给生成到数据库中
            //指定哪些类来生成数据库

            //反射
            //  Assembly assembly = Assembly.LoadFrom(@"E:\VS Workspace\LogicSample\2023.4\仓库管理系统\SqlSugar.DbAccess.Model\Models\");
            //IEnumerable<Type> typelist = assembly.GetTypes().Where(c => c.Namespace == "Models");

            using (db)
            {
                {
                    db.DbMaintenance.CreateDatabase();    //如果没有数据库创建
                    db.CodeFirst.InitTables(typeof(MusicInfo));//通过实体类创建表
                                                                                     // db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Menu), typeof(Dept),typeof(User),typeof(SnowFlake));
                }
                //批量创建表

                *//*  Type[] types = Assembly.LoadFrom("SqlSugar.DbAccess.Model.dll").GetTypes()
                       //.Where(it=>it.FullName.Contains("Info"))//命名空间过滤
                       .ToArray();
                  db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//根据types创建表*/
                /*User user = new User()
                {
                    Name = "admin",
                    CreateTime = DateTime.Now,
                    Password = "0",
                    Role = "管理员"
                };
                db.Insertable(user).ExecuteCommand();
                // 批量操作 批量插入一百条数据到User
                List<User> list = new List<User>();
                for (int i = 0; i < 100; i++)
                {
                    list.Add(new User()
                    {

                        Name = "张三"+i,
                        CreateTime = DateTime.Now,
                        Password = "pwds",
                        Role = "操作员"
                    });
                }
                db.Storageable<User>(list).ExecuteCommand();*//*


                List<MusicInfo> list = new List<MusicInfo>();
                for (int i = 0; i < 20; i++)
                {
                    list.Add(new MusicInfo()
                    {
                       Name="zs"+i,
                       Artists="QQ",
                       Album = "展示",
                       SourceName="QQ"
                    });
                }
                db.Storageable<MusicInfo>(list).ExecuteCommand();



                *//* db.CodeFirst.InitTables(typeof(User));
                  User user = db.Queryable<User>().First();

                 {
                     user.Name = "admin";
                     user.Password = "0";
                     user.Role = 1;
                     user.CreateTime = DateTime.Now;
                     db.Updateable(user).ExecuteCommand();

                 }*//*
            }
        }

        /// <summary>
        /// 初始化数据库表结构
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        private static void InitTable(string assemblyName)
        {
            // 获取所有实体表-初始化表结构
            var entityTypes = DirectoryComponent.EffectiveTypes.Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.IsDefined(typeof(SugarTable), false) && u.Assembly.FullName == assemblyName);
            if (!entityTypes.Any()) return;//没有就退出
            foreach (var entityType in entityTypes)
            {
                var tenantAtt = entityType.GetCustomAttribute<TenantAttribute>();//获取Sqlsugar多租户特性
                                                                                 //  var ignoreInit = entityType.GetCustomAttribute<IgnoreInitTableAttribute>();//获取忽略初始化特性
                *//*  if (ignoreInit != null) continue;//如果有忽略初始化特性
                  if (tenantAtt == null) continue;//如果没有租户特性就下一个
                  var db = DbContext.Db.GetConnectionScope(tenantAtt.configId.ToString());//获取数据库对象
                  var splitTable = entityType.GetCustomAttribute<SplitTableAttribute>();//获取自动分表特性
                  if (splitTable == null)//如果特性是空
                      db.CodeFirst.InitTables(entityType);//普通创建
                  else
                      db.CodeFirst.SplitTables().InitTables(entityType);//自动分表创建*//*
            }
        }
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        private static void InitSeedData(string assemblyName)
        {
            // 获取所有种子配置-初始化数据
          *//*  var seedDataTypes = DirectoryComponent.EffectiveTypes.Where(u => !u.IsInterface && u is { IsAbstract: false, IsClass: true }
                && u.GetInterfaces().Any(i => i.HasImplementedRawGeneric(typeof(ISqlSugarEntitySeedData<>))) && u.Assembly.FullName == assemblyName);
            if (!seedDataTypes.Any()) return;
            foreach (var seedType in seedDataTypes)//遍历种子类
            {
                //使用与指定参数匹配程度最高的构造函数来创建指定类型的实例。
                var instance = Activator.CreateInstance(seedType);
                //获取SeedData方法
                var hasDataMethod = seedType.GetMethod("SeedData");
                //判断是否有种子数据
                var seedData = ((IEnumerable)hasDataMethod?.Invoke(instance, null))?.Cast<object>();
                if (seedData == null) continue;//没有种子数据就下一个
                var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();//获取实体类型
                var tenantAtt = entityType.GetCustomAttribute<TenantAttribute>();//获取sqlsugar租户特性
                if (tenantAtt == null) continue;//如果没有租户特性就下一个
                var db = DbContext.Db.GetConnectionScope(tenantAtt.configId.ToString());//获取数据库对象
                var config = DbContext.DbConfigs.FirstOrDefault(u => u.ConfigId == tenantAtt.configId.ToString());//获取数据库配置
                var seedDataTable = seedData.ToList().ToDataTable();//获取种子数据
                seedDataTable.TableName = db.EntityMaintenance.GetEntityInfo(entityType).DbTableName;//获取表名
                if (config.IsUnderLine)// 驼峰转下划线
                {
                    foreach (DataColumn col in seedDataTable.Columns)
                    {
                        col.ColumnName = UtilMethods.ToUnderLine(col.ColumnName);
                    }
                }
                var ignoreAdd = hasDataMethod.GetCustomAttribute<IgnoreSeedDataAddAttribute>();//读取忽略插入特性
                var ignoreUpdate = hasDataMethod.GetCustomAttribute<IgnoreSeedDataUpdateAttribute>();//读取忽略更新特性
                if (seedDataTable.Columns.Contains(SqlsugarConst.DB_PrimaryKey))//判断种子数据是否有主键
                {
                    //根据判断主键插入或更新
                    var storage = db.Storageable(seedDataTable).WhereColumns(SqlsugarConst.DB_PrimaryKey).ToStorage();
                    if (ignoreAdd == null) storage.AsInsertable.ExecuteCommand();//执行插入
                    if (ignoreUpdate == null) storage.AsUpdateable.ExecuteCommand();//只有没有忽略更新的特性才执行更新
                }
                else// 没有主键或者不是预定义的主键(有重复的可能)
                {
                    //全量插入
                    var storage = db.Storageable(seedDataTable).ToStorage();
                    if (ignoreAdd == null) storage.AsInsertable.ExecuteCommand();
                }
            }*//*
        }
    }
}
*/