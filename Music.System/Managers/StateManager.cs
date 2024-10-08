﻿
using NewLife.Serialization;
using System.Text.Json.Nodes;
using System.Windows.Input;

namespace Music.System.Managers
{
    public class StateManager
    {
        public static async Task SaveLocalStateData()
        {
          
            var stateJsons =await JsonComponent.GetJsonFilesContainNameAsync("State"); // 找到关于状态的所有json
            
            // 定义一个字典来存储事件与处理方法的映射关系
            Dictionary<string, Func<Task<bool>>> eventHandlers = new Dictionary<string, Func<Task<bool>>>();

            // 初始化字典，将事件名称和对应的处理方法添加到字典中
            eventHandlers.Add("musicState.json", async () =>
            {
                return await SaveMusicSource();
            });

            // 添加其他事件和处理方法...
            foreach (var stateJson in stateJsons)
            {
                // 根据 eventName 执行对应的处理方法
                if (eventHandlers.TryGetValue(stateJson, out var handler))
                {
                    bool result = await handler.Invoke();  // 使用await等待异步方法的完成，并获取其返回值
                    if (result)
                    {
                        await Task.CompletedTask;
                    }
                    else
                    {
                        // RaiseRequestClose(new DialogResult(ButtonResult.No));
                    }
                }
            }          
           
        }
        public static async Task<bool> SaveMusicSource()
        {
            if (File.Exists("musicState.json"))
            {
                string serializedSettings = await File.ReadAllTextAsync("musicState.json");
                var dataList = JsonConvert.DeserializeObject<List<MusicSourceDto>>(serializedSettings);

                if (dataList != null)
                {
                    foreach (var item in dataList)
                    {
                        var info = GlibalHeaderArgs.Instance.MusicSourceList.FirstOrDefault(data => data.Id == item.Id);
                        if (info != null)
                        {
                            info.IsSelected = item.IsSelected;
                        }
                    }
                }
            }
            return true;
        }
        string path = "settings.json";
        public  void ApplySavedState(string path2)
        {
            path2 = path;
            if (File.Exists(path2))
            {
                string serializedSettings = File.ReadAllText("settings.json");
                var settingsList = JsonConvert.DeserializeObject<List<MusicSettings>>(serializedSettings);

                foreach (var settings in settingsList)
                {
                    if (settings.Type == SettingType.StartupInfo)
                    {
                        var startupInfo = SetDetailData.Instance.CommonSetInfos.FirstOrDefault(s => s.Id == settings.Id);
                        if (startupInfo != null)
                        {
                            startupInfo.IsSelected = settings.IsSelected;
                        }
                    }
                    else if (settings.Type == SettingType.DownLoadInfo)
                    {
                        var downLoadInfo = SetDetailData.Instance.DownLoadInfos.FirstOrDefault(d => d.Id == settings.Id);
                        if (downLoadInfo != null)
                        {
                            downLoadInfo.IsSelected = settings.IsSelected;
                        }
                    }
                    else if (settings.Type == SettingType.LyricsSetInfo)
                    {
                        var lyricsSetInfo = SetDetailData.Instance.LyricsSetRowInfos.FirstOrDefault(d => d.Id == settings.Id);
                        if (lyricsSetInfo != null)
                        {
                            lyricsSetInfo.IsSelected = settings.IsSelected;
                        }
                    }
                }
            }
        }

        public static void LastBehavor()
        {
            FileQuerier fileQuerier = new FileQuerier()
            {
                FileName = "musicState.json",
            };
            fileQuerier.Loading();
          //  fileQuerier.SaveFile("");
        }
    }

    /// <summary>
    /// 文件查询器
    /// </summary>
    public class FileQuerier
    {
        public string FileName { get; internal set; }

        public string Loading()
        {
            var folder = Directory.GetCurrentDirectory();
            var path=Path.Combine(folder,FileName);

            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;

                JsonTextReader reader = new JsonTextReader(stream);
                var jsonArray = (JArray)JToken.ReadFrom(reader);

                // 获取第一个元素
                var firstElement = jsonArray[0];

                // 获取 "Name": "QQ音乐" 的元素
                var targetElement = jsonArray.FirstOrDefault(e => e["Name"].Value<string>() == "QQ音乐");
                return "";
            }
        }

        public void SaveFile(string fileName)
        {
            var folder = Directory.GetCurrentDirectory();
            var path = Path.Combine(folder, fileName);

            using (var stream = File.OpenText(path))
            {
                

                JsonTextReader reader = new JsonTextReader(stream);
                var jsonObject = (JArray)JToken.ReadFrom(reader);

                string json = jsonObject[""]!.ToString();
            }
        }
    }
}
