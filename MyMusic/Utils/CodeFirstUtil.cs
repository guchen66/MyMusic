
namespace MyMusic.Utils
{
    public static class CodeFirstUtils
    {

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

            using (db)
            
                {
                    db.DbMaintenance.CreateDatabase();    //如果没有数据库创建
                    db.CodeFirst.InitTables(typeof(MusicInfo),typeof(PlayListInfo), typeof(EditPlayListInfo),typeof(PlayListUiInfo));//通过实体类创建表

                    

                }
            }
           /* var bogusDatas = GetBogusData();
          
            using (db)
            {
                foreach (var data in bogusDatas)
                {
                    // 插入假数据到 Customer 表中
                    db.Insertable(data).ExecuteCommand();
                }
            }*/
        }
       /* public static List<MusicInfo> GetBogusData()
        {
            Randomizer.Seed = new Random(123456);
            var userData = new Faker<MusicInfo>("zh_CN")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o => o.SourceName, (f, c) => f.Name.FullName())
                .RuleFor(c => c.Name, (f, c) => f.Name.FullName())
                .RuleFor(c => c.Artists, f => f.Internet.Password())
                .RuleFor(o => o.Album, f => f.Internet.Email())
                .RuleFor(o => o.CreateTime, f => f.Date.Past(3));                       //时间选择过去三年的随机时间
            return userData.Generate(100);
        }*/

}

      