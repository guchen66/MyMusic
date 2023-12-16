using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Music.ToolKit.Extensions
{
    public class JsonExtension
    {
        // 读取指定名称（包括文件路径）的json文件，返回json字符串
        public static string GetJsonFromResource(string resourceName)
        {
            var folder = Directory.GetCurrentDirectory(); // 获取应用程序当前工作目录
            var path = Path.Combine(folder, resourceName); // 使用 Path.Combine 组合路径

            try
            {
                using (var stream = File.OpenText(path))
                {
                    if (stream == null) return null;
                    JsonTextReader reader = new JsonTextReader(stream);
                    JObject jsonObject = (JObject)JToken.ReadFrom(reader);

                    string json = jsonObject["Account"]["username"].ToString();
                    string json2 = jsonObject["Account"].ToString();

                    return json;
                }
            }
            catch (FileNotFoundException ex)
            {
                // 文件不存在时的异常处理
                // 这里你可以选择返回一个默认值，抛出自定义异常，或者记录错误日志等
                Console.WriteLine("文件不存在：" + ex.Message);
                return "默认的用户名"; // 返回默认值
            }
            catch (Exception ex)
            {
                // 其他异常的处理
                Console.WriteLine("发生异常：" + ex.Message);
                throw; // 抛出异常，交给调用方处理
            }
        }


        /// <summary>
        /// 获取根目录下的所有json文件
        /// </summary>
        /// <returns></returns>
        public static string[] GetJsonFileNames()
        {
            string rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

            string[] jsonFilePaths = Directory.GetFiles(rootDirectory, "*.json");

            return jsonFilePaths.Select(Path.GetFileName).ToArray()!;
        }


        /// <summary>
        /// 跟据json名称确定指定的json文件
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static string GetDecisionJson(string resourceName)
        {
            var folder = Directory.GetCurrentDirectory(); // 获取应用程序当前工作目录
            var path = Path.Combine(folder, resourceName); // 使用 Path.Combine 组合路径

            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;

                JsonTextReader reader = new JsonTextReader(stream);

                JObject jsonObject = (JObject)JToken.ReadFrom(reader);

                string json = jsonObject["Account"]!.ToString();

                return json;
            }
        }
        public static async Task<string[]> GetJsonFileNamesAsync()
        {
            string rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string[] jsonFilePaths = await Task.Run(() => Directory.GetFiles(rootDirectory, "*.json"));
            return jsonFilePaths.Select(Path.GetFileName).ToArray();
        }

        public static async Task<string> GetDecisionJsonAsync(string resourceName)
        {
            var folder = Directory.GetCurrentDirectory(); // 获取应用程序当前工作目录
            var path = Path.Combine(folder, resourceName); // 使用 Path.Combine 组合路径

            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;
                JsonTextReader reader = new JsonTextReader(stream);
                JObject jsonObject = (JObject)await JToken.ReadFromAsync(reader);

                string json = jsonObject["Account"]!.ToString();
                return json;
            }
        }

        /* public static Task SerializationJson()
         {

         }*/

    }
}
