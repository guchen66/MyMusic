using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Music.Mah.Dialogs.ViewModels.Toast
{
    public class LoginSucessViewModel : BindableBase, IDialogAware
    {
        public string Title => "LoadSucess";
        public DialogCloseListener RequestClose { get; set; }
        private Timer _timer;
        private readonly Dispatcher _dispatcher;
        public ICommand CloseCommand { get; set; }

        public LoginSucessViewModel()
        {
            _dispatcher = Application.Current.Dispatcher;
            CloseCommand = new DelegateCommand<string>(ExecuteClose);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // 在2秒后关闭弹窗
            _timer = new Timer(ExecuteClose, null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }

        private void ExecuteClose(object state)
        {
            // 在UI线程上关闭弹窗
            _dispatcher.Invoke(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.None));
            });
        }
    }
}