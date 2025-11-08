namespace Music.Shared.Entitys.NotMapped
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        ///     更新时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        ///     创建者名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public virtual string CreateUserName { get; set; }

        /// <summary>
        ///     修改者名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public virtual string UpdateUserName { get; set; }

        /// <summary>
        ///     备注说明
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public virtual string Remark { get; set; }

        /// <summary>
        ///     软删除
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 1)]
        public virtual string IsDelete { get; set; }
    }
}