using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyMusic.ViewModels.Dialog
{
    public class AddPlayListDialogViewModel : BindableBase, IDialogAware
    {

        #region 字段、属性

        public ILogger _logger;
        public event Action<IDialogResult> RequestClose;
        public string Title { get; set; } = "新建歌单";

        /// <summary>
        /// 创建歌单名称
        /// </summary>
        public string _playListName;

        public string PlayListName
        {
            get { return _playListName; }
            set { SetProperty<string>(ref _playListName, value); }
        }

        /// <summary>
        /// 歌单长度
        /// </summary>
        private int _playListNameLength=20;

        public int PlayListNameLength
        {
            get { return _playListNameLength; }
            set { SetProperty<int>(ref _playListNameLength, value); PlayListNameLength = _playListName.Length; }
        }

        #endregion

        public AddPlayListDialogViewModel()
        {
            _logger = ContainerLocator.Container.Resolve<ILogger>();
            CloseDialogCommand = new DelegateCommand<string>(ExecuteCloseDialog);
        }

        #region 命令

        public ICommand CloseDialogCommand { get; set; }
        public ICommand TextChangedCommand {  get; set; }
        #endregion

        #region 方法

        //弹窗可以关闭
        public bool CanCloseDialog()
        {
            return true;
        }

        //弹框关闭时触发
        public void OnDialogClosed()
        {
            var parame = new DialogParameters();
            parame.Add("myParam", PlayListName);
           
        }

        //弹框打开时触发
        public void OnDialogOpened(IDialogParameters parameters)
        {
          //  parameters.GetValue<string>("message");
        }

        private void ExecuteCloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.Yes;
            }
                
            else if (parameter?.ToLower() == "false")
            {
                result = ButtonResult.No;
            }
            var parame = new DialogParameters();
            parame.Add("PlayListName", PlayListName);

            //双向传递，这里将歌单名称和返回结果传递过去
            RequestClose?.Invoke(new DialogResult(result, parame));
        }

        //触发窗体关闭事件
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        #endregion

    }
}
