using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music.Shared.Entitys.NotRegistered
{
    /// <summary>
    /// 框架实体基类
    /// </summary>
    public class BaseEntity : PrimaryKeyEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnDescription = "创建时间", IsOnlyIgnoreUpdate = true, IsNullable = true)]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnDescription = "更新时间", IsOnlyIgnoreInsert = true, IsNullable = true)]
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建者Id", IsOnlyIgnoreUpdate = true, IsNullable = true)]
        public virtual long? CreateUserId { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        [SugarColumn(ColumnDescription = "修改者Id", IsOnlyIgnoreInsert = true, IsNullable = true)]
        public virtual long? UpdateUserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnDescription = "创建人", IsOnlyIgnoreUpdate = true, IsNullable = true)]
        public virtual string CreateUser { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnDescription = "更新人", IsOnlyIgnoreInsert = true, IsNullable = true)]
        public virtual string UpdateUser { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        [SugarColumn(ColumnDescription = "软删除", IsNullable = true)]
        public virtual bool IsDelete { get; set; } = false;
    }
}
