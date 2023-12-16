﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModels.Dialog
{
    public class DeletePlayListDialogViewModel : BindableBase, IDialogAware
    {

        #region 字段、属性

        public string Title { get; set; } = "删除歌单弹窗";
        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 歌单名称
        /// </summary>
        private string _playListName;

        public string PlayListName
        {
            get { return _playListName; }
            set { SetProperty<string>(ref _playListName, value); }
        }
        #endregion

        public DeletePlayListDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand<string>(ExecuteCloseDialog);
        }

        #region 命令
        public ICommand CloseDialogCommand { get; set; }
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
