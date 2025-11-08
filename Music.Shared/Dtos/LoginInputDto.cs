namespace Music.Shared.Dtos
{
    public class LoginInputDto : BaseDto
    {
        private DateTime _loginTime;

        public DateTime LoginTime
        {
            get => _loginTime;
            set => SetProperty(ref _loginTime, value);
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
    }
}