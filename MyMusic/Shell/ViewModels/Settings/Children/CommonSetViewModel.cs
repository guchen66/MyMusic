using Music.Shared.Globals.MainSign.Settings;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.ToolKit.Commands;
using Music.System.Services.MainSign.SettingSign;

namespace MyMusic.Shell.ViewModels.Settings.Children
{
    public class CommonSetViewModel:BindableBase
    {
        public SetDetailData SetDetailData { get; set; } = SetDetailData.Instance;
        public ScrollCommand ScrollCommand { get; } = new ScrollCommand();
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;
        public CommonSetViewModel(ISettingsService settingsService,IEventAggregator eventAggregator)
        {
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
           // _eventAggregator.GetEvent<SaveSetDataEvent>().Publish();
        }
        private DelegateCommand _saveState;
        public DelegateCommand SaveState =>
            _saveState ?? (_saveState = new DelegateCommand(ExecuteSave));

        private void ExecuteSave()
        {
             _settingsService.SaveDataStateInfos(SetDetailData.Instance.CommonSetInfos);
            //StateHelper.SaveState(SetDetailData.Instance.StartupInfos);
            
        }
        // 在视图模型的析构函数中保存数据
        public void Dispose()
        {
            _settingsService.SaveDataStateInfos(SetDetailData.Instance.CommonSetInfos);
        }
    }


}
