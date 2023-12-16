using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core.Options
{
    /// <summary>
    /// 默认业务配置
    /// </summary>
    public class BaseOption
    {
        /// <summary>
        /// 初始化表
        /// </summary>
        public bool InitTable { get; set; } = false;

        /// <summary>
        /// 初始化表内部数据
        /// </summary>
        public bool InitSeedData { get; set; } = false;
    }
}
