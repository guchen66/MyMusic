
namespace MyMusic.ViewModels.Errors
{
    public class _404ViewModel : BindableBase
    {
        private bool _isRequestFailed=true;
        public bool IsRequestFailed
        {
            get { return _isRequestFailed; }
            set { SetProperty(ref _isRequestFailed, value); }
        }
    }
}
