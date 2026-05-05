using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Managers
{
    /// <summary>
    /// 本地数据管理类
    /// </summary>
    public class LocalDataManager
    {
        /// <summary>
        /// 从根目录加载本地路径数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public static void LoadForm(string fileName)
        {
            if (!File.Exists(fileName)) return;
        }
    }
}