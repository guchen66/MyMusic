namespace Music.Core.Account
{
    public class LoginAccount
    {
        private readonly IEventAggregator _eventAggregator;

        public LoginAccount(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<LoginEvent>().Subscribe(Login, ThreadOption.UIThread, true);
        }

        private void Login(Window window)
        {
            window.DialogResult = true;
        }
    }
}