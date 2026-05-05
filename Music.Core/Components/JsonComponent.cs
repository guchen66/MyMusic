using NewLife.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core.Components
{
    /// <summary>
    /// 使用NewLife和Newtonsoft.Json进行封装
    /// </summary>
    public class JsonComponent
    {
        public static string GetJsonObjectValue(string fileName, string keyValue)
        {
            string result = "";
            var provider = new JsonConfigProvider() { FileName = fileName };
            result = provider.GetSection(keyValue).Value;
            return result;
        }
    }
}