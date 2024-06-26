﻿
namespace Music.Shared.Globals.HeaderSign
{
    [IniSection(SectionName = "SearchOption")]
    public class SearchSourceArgs
    {
        public bool Netease { get; set; }
        public bool Kugou { get; set; }
        public bool Tencent { get; set; }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IniSectionAttribute : Attribute
    {
        #region --字段--
        private string _sectionName;
        #endregion

        #region --属性--
        /// <summary>
        /// 获取或设置配置项 (Ini) 节名称
        /// </summary>
        public string SectionName
        {
            get { return _sectionName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("传入的字符串不允许用来设置节名称, 原因: 节名称不能为空", "value");
                }
                _sectionName = value;
            }
        }
        #endregion
    }
}
