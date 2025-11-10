using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Entitys.NotMapped
{
    /// <summary>
    /// Not递增主键实体基类
    /// </summary>
    public abstract class NotAutoIncrementEntity : EntityBase
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public override int Id { get; set; }
    }
}