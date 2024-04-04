
namespace Music.Shared.Entitys.NotMappedEntity
{
    /// <summary>
    ///     递增主键实体基类
    /// </summary>
    public abstract class AutoIncrementEntity : EntityBase
    {
        /// <summary>
        ///     主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        // 注意是在这里定义你的公共实体
        public override int Id { get; set; }
    }

    /// <summary>
    ///     递增主键实体基类
    /// </summary>
    public abstract class NotAutoIncrementEntity : EntityBase
    {
        /// <summary>
        ///     主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        // 注意是在这里定义你的公共实体
        public override int Id { get; set; }
    }
}
