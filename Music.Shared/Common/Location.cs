namespace Music.Shared.Common
{
    /// <summary>
    /// 保存的数据的所有地址
    /// </summary>
    public class Location
    {
        static Location()
        {
            Directory.CreateDirectory(Framework);
            Directory.CreateDirectory(Recipe);
            Directory.CreateDirectory(Common);
            Directory.CreateDirectory(Cache);
            Directory.CreateDirectory(Database);
            Directory.CreateDirectory(Images);
            Directory.CreateDirectory(User);
            Directory.CreateDirectory(Profiles);
            Directory.CreateDirectory(Logger);
        }

        public static readonly string AppData = Path.Combine("E:\\MusicDatas");

        /// <summary>
        /// 用户
        /// </summary>
        public static readonly string User = Path.Combine(AppData, nameof(User));

        /// <summary>
        /// 配置
        /// </summary>
        public static readonly string Profiles = Path.Combine(AppData, nameof(Profiles));

        /// <summary>
        ///注意操作SaveFileDialog的时候使用反斜杠，否则报错System.ArgumentException:“值不在预期的范围内
        /// </summary>
        public static readonly string Images = Path.Combine(AppData, nameof(Images));

        /// <summary>
        /// 模板地址
        /// </summary>
        public static readonly string Framework = Path.Combine(AppData, nameof(Framework));

        /// <summary>
        /// 配方地址
        /// </summary>
        public static readonly string Recipe = Path.Combine(AppData, nameof(Recipe));

        /// <summary>
        /// 管理地址
        /// </summary>
        public static readonly string Common = Path.Combine(AppData, nameof(Common));

        /// <summary>
        /// 缓存地址
        /// </summary>
        public static readonly string Cache = Path.Combine(AppData, nameof(Cache));

        /// <summary>
        /// 数据库地址
        /// </summary>
        public static readonly string Database = Path.Combine(AppData, nameof(Database));

        /// <summary>
        /// Log地址
        /// </summary>
        public static readonly string Logger = Path.Combine(AppData, nameof(Logger));
    }
}