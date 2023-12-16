using Music.Shared.Globals.MainSign.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.ToolKit.Commands;
using Music.System.Services.MainSign.SettingSign;

namespace MyMusic.Shell.ViewModels.Settings.Children
{
    public class DownLoadSetViewModel:BindableBase
    {
        public SetDetailData SetDetailData { get; set; } = SetDetailData.Instance;
        public ScrollCommand ScrollCommand { get; } = new ScrollCommand();
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;
        public DownLoadSetViewModel(ISettingsService settingsService,IEventAggregator eventAggregator)
        {
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
           // _eventAggregator.GetEvent<SaveDownLoadSetDataEvent>().Publish();
        }
        private string _musicDirectory= $"E:\\home\\QQ音乐Data\\";

        public string MusicDirectory
        {
            get { return _musicDirectory; }
            set { SetProperty<string>(ref  _musicDirectory , value); }
        }


        /// <summary>
        /// 跟踪选中状态
        /// </summary>
        private DelegateCommand<DownLoadInfo> _radioButtonState;
        public DelegateCommand<DownLoadInfo> RadioButtonState =>
            _radioButtonState ?? (_radioButtonState = new DelegateCommand<DownLoadInfo>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(DownLoadInfo selectedInfo)
        {
            // 遍历 DownLoadInfos 列表
            foreach (var info in SetDetailData.Instance.DownLoadInfos)
            {
                // 如果当前遍历的项与选中的项不一致，则将其 IsSelected 属性设为 false
                if (info != selectedInfo)
                {
                    info.IsSelected = false;
                }
            }

            // 保存状态
            // StateHelper.SaveState(SetDetailData.Instance.DownLoadInfos);
            _settingsService.SaveDataStateInfos(SetDetailData.Instance.DownLoadInfos);
           
        }
        private DelegateCommand _updateDirectory;
        public DelegateCommand UpdateDirectory =>
            _updateDirectory ?? (_updateDirectory = new DelegateCommand(ExecuteUpdate));

        private void ExecuteUpdate()
        {
           
        }

        private DelegateCommand _openDirectory;
        public DelegateCommand OpenDirectory =>
            _openDirectory ?? (_openDirectory = new DelegateCommand(ExecuteChecked));

        private void ExecuteChecked()
        {
           
        }
    }
}
