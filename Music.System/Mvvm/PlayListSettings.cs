
namespace Music.System.Mvvm
{
    public class SettingBase : BindableBase
    {
    }

    public class PlayListSettings: SettingBase
    {

        #region  字段

        IDialogService _dialogService;
        IPlayListService _playListService;
        IEventAggregator _eventAggregator;
        public ICommand ReNamePlayListItemCommand { get; set; }
        public ICommand DeletePlayListItemCommand { get; set; }

        #endregion

        public PlayListSettings()
        {
            // 使用Prism服务定位器注入
            _dialogService = ContainerLocator.Container.Resolve<IDialogService>();
            _playListService = ContainerLocator.Container.Resolve<IPlayListService>();
            _eventAggregator = ContainerLocator.Container.Resolve<IEventAggregator>();
            ReNamePlayListItemCommand = new DelegateCommand<string>(ExecuteReNameItem);
            DeletePlayListItemCommand = new DelegateCommand<long?>(ExecuteRemoveItem);
        }

        #region 方法

        /// <summary>
        /// 对歌单重命名
        /// </summary>
        /// <param name="id"></param>
        public async void ExecuteReNameItem(string name)
        {
            var playListInfo = await _playListService.GetPlatListByNameAsync(name);
            DialogParameters keyValuePairs = new DialogParameters();
            keyValuePairs.Add("PlayListName", name);
            _dialogService.ShowDialog("ReNamePlayListDialog", keyValuePairs, async arg =>
            {
                if (arg.Result == ButtonResult.Yes)
                {
                    var value = arg.Parameters.GetValue<string>("ReName");      //重命名歌单名称传递过来
                    playListInfo.PlayListName = value;
                    await _playListService.GetReNamePlatListAsync(playListInfo);
                    _eventAggregator.GetEvent<RefreshEvent>().Publish();

                }
            });

        }
        /// <summary>
        /// 跟据歌单名删除具体歌单
        /// </summary>
        /// <param name="id"></param>
        public async void ExecuteRemoveItem(long? id)
        {
            _dialogService.ShowDialog("DeletePlayListDialog", async arg =>
            {
                if (arg.Result == ButtonResult.Yes)
                {

                    await _playListService.RemovePlatListById(id);
                     _eventAggregator.GetEvent<RefreshEvent>().Publish();
                }
            });
            await Task.CompletedTask;
        }    
        #endregion

    }
}
