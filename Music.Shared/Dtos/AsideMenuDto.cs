
namespace Music.Shared.Dtos
{
    /// <summary>
    /// 左侧菜单的DTO，初始化界面用来展示左侧菜单
    /// </summary>
    public class AsideMenuDto:BaseDto
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Content { get; set; }
        public string NameSpace { get; set; }
    }
}
