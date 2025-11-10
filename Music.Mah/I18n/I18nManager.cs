using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Resources;

namespace Music.Mah.I18n
{
    public static class I18nManager
    {
        private static readonly ResourceManager _res = new ResourceManager(AppResourceNames.EN, typeof(I18nManager).Assembly);

        private static readonly ResourceManager _zh_CNres = new ResourceManager(AppResourceNames.ZH_CN, typeof(I18nManager).Assembly);

        public static string Get(string key)
        {
            // 优化1：缓存当前资源管理器，避免每次判断
            var resourceMgr = CurrentUICulture.ToString() == "en" ? _res : _zh_CNres;

            // 优化2：使用更高效的文化信息比较
            // 或者直接缓存结果（如果内存允许）
            return resourceMgr.GetString(key, CurrentUICulture);
        }

        public static CultureInfo CurrentUICulture
        {
            get => Thread.CurrentThread.CurrentUICulture;  // 获取当前线程的UI文化
            set
            {
                // 如果新值与当前值相同，直接返回（避免不必要的更新）
                if (Equals(Thread.CurrentThread.CurrentUICulture, value)) return;

                // 设置当前线程的UI文化（影响资源查找）
                Thread.CurrentThread.CurrentUICulture = value;
                // 设置当前线程的区域文化（影响数字、日期格式等）
                Thread.CurrentThread.CurrentCulture = value;

                // 触发语言改变事件
                OnLanguageChanged();
            }
        }

        public static event Action LanguageChanged;

        private static void OnLanguageChanged() => LanguageChanged?.Invoke();
    }
}