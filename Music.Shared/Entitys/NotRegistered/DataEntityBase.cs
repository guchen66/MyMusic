
namespace Music.Shared.Entitys.NotRegistered
{
    /// <summary>
    /// 业务数据实体基类(数据权限)
    /// </summary>
    public abstract class DataEntityBase : BaseEntity
    {
        /// <summary>
        /// 创建者部门Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建者部门Id")]
        public virtual long CreateOrgId { get; set; }
    }
}
