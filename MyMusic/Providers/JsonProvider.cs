
namespace MyMusic.Providers
{
    public class JsonProvider
    {
        /// <summary>
        /// 读取本地json数组文件 找到音乐来源
        /// </summary>
        /// <returns></returns>
        public async Task<List<MusicSourceDto>> GetMusicSourceDto()
        {
            string[] jsonFileName = await JsonComponent.GetJsonFileNamesAsync();  // 先找到所有的json文件
            var json = await JsonComponent.GetJsonFileByNameAsync("musicState"); // 获得名称是musicState的
            var jsonContent = await File.ReadAllTextAsync(json);
            var music = JsonConvert.DeserializeObject<List<MusicSourceDto>>(jsonContent);
            return music;
        }
    }
}
