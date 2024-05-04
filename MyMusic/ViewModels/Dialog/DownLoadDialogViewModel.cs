using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModels.Dialog
{
    internal class DownLoadDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "下载";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
        public DownLoadDialogViewModel()
        {
            CancelCommand = new DelegateCommand<string>(ExecuteCancel);
            ConfirmCommand = new DelegateCommand<string>(ExecuteConfirm);
        }

        private void ExecuteConfirm(string obj)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void ExecuteCancel(string obj)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set;}
    }
}
