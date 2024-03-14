
namespace MyMusic.ViewModels.Dialog
{
    public class LoadingDialogViewModel : BindableBase, IDialogAware
    {
        #region 字段、属性

        public string Title => "Loading";
        private Timer _timer;
        private readonly Dispatcher _dispatcher;
        public event Action<IDialogResult> RequestClose;

        #endregion

        public LoadingDialogViewModel()
        {
            _dispatcher = Application.Current.Dispatcher;
        }

        #region 方法

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
            _timer = new Timer(CloseDialog, null, TimeSpan.FromSeconds(2), Timeout.InfiniteTimeSpan);
        }

        private void CloseDialog(object state)
        {
            // 在UI线程上关闭弹窗
            _dispatcher.Invoke(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.None));
            });
        }
        #endregion

    }
}
