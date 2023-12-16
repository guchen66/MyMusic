using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.I18nResource
{
    public static class Languages
    {
        public static List<CultureInfo> AvailableCultureInfos { get; } = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("zh-CN"),
            //new CultureInfo("ja"),
        };
    }
}
