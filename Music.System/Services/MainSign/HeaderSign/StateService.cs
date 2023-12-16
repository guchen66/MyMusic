
namespace Music.System.Services.MainSign.HeaderSign
{
    public class StateService : IStateService
    {
        public string GetHeader(string key)
        {
            return "";
        }

        public async Task<bool> RemoveState(MusicSourceDto musicSource)
        {
            return false;
        }

        public async Task<bool> SaveState(List<MusicSourceArgs> musicSourceList)
        {
            var settingsList = new List<MusicSourceDto>();

            // 遍历所有StartupInfo
            foreach (var item in musicSourceList)
            {
                // 创建MusicSettings对象并添加到settingsList
                var settings = new MusicSourceDto() { Id = item.Id, IsSelected = item.IsSelected, Name = item.Name, Source = item.Name };
                settingsList.Add(settings);
            }

            //返回缩进的Json
            string rememberPwd = JsonConvert.SerializeObject(settingsList, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("musicState.json"))
            {
                await writer.WriteAsync(rememberPwd);
            }
            return true;
        }

    }
}
