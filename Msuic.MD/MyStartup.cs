namespace Music.MD
{
    internal class MyStartup
    {
        private static ITangdaoLogger Logger = TangdaoLogger.Get(typeof(App));

        public static void SetLanguage()
        {
            var language = ConfigHelper.ReadConfig("resxKey");
            if (language == null) return;
            SetCulture(language);
        }

        public static void AddSqlSugar()
        {
            var mysql = ConfigurationManager.ConnectionStrings["MusicDb"]?.ConnectionString;
            SugarIocServices.AddSqlSugar(new IocConfig()
            {
                ConnectionString = mysql,
                DbType = IocDbType.MySql,
                IsAutoCloseConnection = true,
            });

            //创建数据表
            ConfigurationManager.AppSettings["IsAutoTable"].TryToBool(out var autoGenTable);
            if (autoGenTable)
            {
                StaticConfig.CodeFirst_MySqlCollate = "utf8mb4_0900_ai_ci";//较高版本支持
                DbScoped.Sugar.DbMaintenance.CreateDatabase();

                ////创建表
                DbScoped.Sugar.CodeFirst.InitTables(
                    typeof(LoginInputInfo), typeof(RoleInfo)
                );
            }
            //生成种子数据
            ConfigurationManager.AppSettings["IsAutoSeed"].TryToBool(out var autoGenSeed);
            if (autoGenSeed)
            {
                try
                {
                    TangdaoFakeDataGenerator<LoginInputInfo> Generator = new TangdaoFakeDataGenerator<LoginInputInfo>();
                    var loginList = SetAutoSeed<LoginInputInfo>(100);
                    var roleList = SetAutoSeed<RoleInfo>(100);
                    // 使用明确的批量插入
                    Stopwatch sw = Stopwatch.StartNew();
                    sw.Start();
                    var LoginRows = DbScoped.Sugar.Insertable(loginList).ExecuteCommand();
                    var RoleRows = DbScoped.Sugar.Insertable(roleList).ExecuteCommand();
                    Logger.WriteLocal($"自动种子数据生成成功，插入 {LoginRows} 条数据");

                    // 验证插入的数据
                    var count = DbScoped.Sugar.Queryable<LoginInputInfo>().Count();
                    Logger.WriteLocal($"当前表总数据量: {count} 条,耗时：{sw.ElapsedMilliseconds}");
                }
                catch (Exception ex)
                {
                    Logger.WriteLocal($"种子数据插入失败: {ex.Message}");
                }
            }
            //过滤
            //DbScoped.Sugar.QueryFilter.AddTableFilter<LoginInputInfo>(it => it.IsDelete == "0");
        }

        /// <summary>
        /// 扫描指定Map
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static void SetMapster(IContainerRegistry containerRegistry)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var config = new TypeAdapterConfig();
            config.Apply(new UserRegister());
            containerRegistry.RegisterInstance(config);
            containerRegistry.Register<IMapper>(() => new Mapper(config));
            Logger.WriteLocal($"耗时：{sw.ElapsedMilliseconds}");
        }

        /// <summary>
        /// 扫描所有Map
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static void SetMapsterList(IContainerRegistry containerRegistry)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var assembly = Assembly.Load("Music.Core");
            var config = new TypeAdapterConfig();
            config.Scan(assembly);
            containerRegistry.RegisterInstance(config);
            containerRegistry.Register<IMapper>(() => new Mapper(config));
            Logger.WriteLocal($"耗时：{sw.ElapsedMilliseconds}");
        }

        public static List<T> SetAutoSeed<T>(int genCount) where T : class, new()
        {
            TangdaoFakeDataGenerator<T> Generator = new TangdaoFakeDataGenerator<T>();
            return Generator.GenerateRandomData(genCount);
        }

        private static void SetCulture(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}