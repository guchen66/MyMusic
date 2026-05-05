using DryIoc;
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

        private string _downLoadMusicName;

        public string DownLoadMusicName
        {
            get => _downLoadMusicName;
            set => SetProperty(ref _downLoadMusicName, value);
        }

        private IMusic _music;

        public IMusic Music
        {
            get => _music;
            set => SetProperty(ref _music, value);
        }

        public IDialogParameters Parameters { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            DownLoadMusicName = string.Empty;
            Music = null;
            Parameters = null;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Music = parameters.GetValue<IMusic>("selectedItem");
            Parameters.Add("selectedItem", Music);
            DownLoadMusicName = Music.Name;
            DownLoadMusicName = $"《{DownLoadMusicName}》";
        }

        public DownLoadDialogViewModel()
        {
            CancelCommand = new DelegateCommand<string>(ExecuteCancel);
            ConfirmCommand = new DelegateCommand<string>(ExecuteConfirm);
        }

        private void ExecuteConfirm(string obj)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, Parameters));
        }

        private void ExecuteCancel(string obj)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}