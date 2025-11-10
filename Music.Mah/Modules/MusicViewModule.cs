namespace Music.Mah.Modules
{
    public class MusicViewModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册视图
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<AsideView>();
            containerRegistry.RegisterForNavigation<FavoriteView>();
            containerRegistry.RegisterForNavigation<CommendView>();
            containerRegistry.RegisterForNavigation<DownLoadView>();

            ///注册对话框弹窗
        }
    }
}