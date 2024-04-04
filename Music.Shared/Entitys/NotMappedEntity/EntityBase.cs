
namespace Music.Shared.Entitys.NotMappedEntity
{
    /// <summary>
    ///     自定义实体基类
    /// </summary>
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }

        /// <summary>
        ///     备注说明
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public virtual string Remark { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public virtual DateTime? CreatedTime { get; set; }

        /// <summary>
        ///     更新时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public virtual DateTime? UpdatedTime { get; set; }

        /// <summary>
        ///     创建者名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public virtual string CreatedUserName { get; set; }

        /// <summary>
        ///     修改者名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public virtual string UpdatedUserName { get; set; }

        /// <summary>
        ///     软删除
        /// </summary>
        /// <summary>
        /// 软删除 false==0,true==1
        /// </summary>
        [SugarColumn(ColumnDescription = "软删除", IsNullable = true)]
        public virtual bool IsDelete { get; set; } = false;


        public virtual void Create()
        {
            var userName = UserInfo.UserName;
            CreatedTime = DateTime.Now;

            CreatedUserName = userName;
        }

        public virtual void Modify()
        {
            var userName = UserInfo.UserName;
            UpdatedTime = DateTime.Now;

            UpdatedUserName = userName;
        }

        /// <summary>
        ///     更新信息列
        /// </summary>
        /// <returns></returns>
        public string[] UpdateColumn()
        {
            var result = new[] { nameof(UpdatedUserName), nameof(UpdatedTime) };
            return result;
        }

        /// <summary>
        ///     假删除的列，包含更新信息
        /// </summary>
        /// <returns></returns>
        public string[] FalseDeleteColumn()
        {
            var updateColumn = UpdateColumn();
            var deleteColumn = new[] { nameof(IsDelete) };
            var result = new string[updateColumn.Length + deleteColumn.Length];
            deleteColumn.CopyTo(result, 0);
            updateColumn.CopyTo(result, deleteColumn.Length);
            return result;
        }
    }
}
