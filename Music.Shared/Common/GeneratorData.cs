using NewLife.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Common
{
    public class GeneratorData
    {
        public string Title { get; set; }
        public bool IsGenerator { get; set; }
        public bool IsSeedData { get; set; }
    }

    public class GeneratorDataProvider
    {
        private static GeneratorData _jsonData;
        private static GeneratorData GetJsonData()
        {
            if (_jsonData == null)
            {
                var provider = new JsonConfigProvider()
                {
                    FileName = "AppConfig.json"
                };

                _jsonData = provider.Load<GeneratorData>("GeneratorData")!;
            }

            return _jsonData;
        }
        public static bool IsGenerated
        {
            get => GetJsonData().IsGenerator;
        }

        public static bool IsSeedData
        {
            get => GetJsonData().IsSeedData;
        }
    }
}
