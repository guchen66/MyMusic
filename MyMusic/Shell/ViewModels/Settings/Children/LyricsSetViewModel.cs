using Music.Shared.Globals.MainSign.Settings;
using Music.ToolKit.Commands;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.System.Services.MainSign.SettingSign;

namespace MyMusic.Shell.ViewModels.Settings.Children
{
    public class LyricsSetViewModel : BindableBase
    {
        public SetDetailData SetDetailData { get; set; } = SetDetailData.Instance;
        public ScrollCommand ScrollCommand { get; } = new ScrollCommand();
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;
        public LyricsSetViewModel(ISettingsService settingsService, IEventAggregator eventAggregator)
        {
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
            // _eventAggregator.GetEvent<SaveDownLoadSetDataEvent>().Publish();
        }

        /// <summary>
        /// 行数选中状态
        /// </summary>
        private DelegateCommand<LyricsSetInfo> _rowState;
        public DelegateCommand<LyricsSetInfo> RowState =>
            _rowState ?? (_rowState = new DelegateCommand<LyricsSetInfo>(ExecuteRowStateSaveCmd));

        private void ExecuteRowStateSaveCmd(LyricsSetInfo selectedInfo)
        {
            // 遍历 DownLoadInfos 列表
            foreach (var info in SetDetailData.Instance.LyricsSetRowInfos)
            {
                // 如果当前遍历的项与选中的项不一致，则将其 IsSelected 属性设为 false
                if (info != selectedInfo)
                {
                    info.IsSelected = false;
                }
            }
            _settingsService.SaveDataStateInfos(SetDetailData.Instance.LyricsSetRowInfos);

        }

       
    }
}
