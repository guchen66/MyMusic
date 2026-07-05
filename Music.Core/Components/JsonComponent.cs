using Music.Shared.Dtos;
using NewLife.Configuration;
using NewLife.Serialization;
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

        public static T GetJsonObject<T>(string fileName) where T : class, new()
        {
            var provider = new JsonConfigProvider() { FileName = fileName };
            T input = provider.Load<T>("Account");
            return input;
        }

        public static LoginInputDto GetJsonObject(string fileName)
        {
            var json = File.ReadAllText(fileName);
            var input = json.ToJsonEntity<LoginInputDto>();
            return input;
        }
    }
}