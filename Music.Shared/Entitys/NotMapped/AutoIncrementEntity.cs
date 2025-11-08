namespace Music.Shared.Entitys.NotMapped
{
    /// <summary>
    ///  递增主键实体基类
    /// </summary>
    public abstract class AutoIncrementEntity : EntityBase
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public override int Id { get; set; }
    }
}