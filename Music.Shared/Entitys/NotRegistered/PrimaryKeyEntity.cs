
namespace Music.Shared.Entitys.NotRegistered
{
    /// <summary>
    /// 主键实体基类
    /// </summary>
    public abstract class PrimaryKeyEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(ColumnDescription = "Id", IsPrimaryKey = true)]
        public virtual long Id { get; set; }

        /// <summary>
        /// 拓展信息
        /// </summary>
        [SugarColumn(ColumnName = "ExtJson", ColumnDescription = "扩展信息", ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
        public virtual string ExtJson { get; set; }
    }
}
