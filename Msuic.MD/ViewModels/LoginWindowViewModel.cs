using Music.Shared.Dtos;

namespace Music.MD.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        private ITangdaoLogger _logger = TangdaoLogger.Get(typeof(LoginWindowViewModel));

        private readonly LoginAccount _loginAccount;
        private readonly IReadService _readService;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private IEventAggregator _eventAggregator;

        public LoginWindowViewModel(IEventAggregator eventAggregator, LoginAccount loginAccount, ILoginRepository loginRepository, IMapper mapper)
        {
            _eventAggregator = eventAggregator;
            _loginRepository = loginRepository;
            _loginAccount = loginAccount;
            _mapper = mapper;
            LoginCommand = new DelegateCommand<Window>(ExecuteLogin);
            LoadedCommand = new DelegateCommand(ExecuteLoaded);
        }

        #region 属性

        private LoginInputDto _loginInputDto;

        public LoginInputDto LoginInputDto
        {
            get => _loginInputDto ?? (_loginInputDto = new LoginInputDto());
            set => SetProperty(ref _loginInputDto, value);
        }

        #endregion 属性

        #region 方法

        private async void ExecuteLoaded()
        {
            var all = await _loginRepository.QueryListAsync();
            var model = all.FirstOrDefault();
            LoginInputDto = _mapper.Map<LoginInputDto>(model);
        }

        private void ExecuteLogin(Window window)
        {
            _logger.WriteLocal("开始登录");
            VerifyLogin();
            _eventAggregator.GetEvent<LoginEvent>().Publish(window);
        }

        private bool VerifyLogin()
        {
            if (LoginInputDto.UserName != null && LoginInputDto.Password != null)
            {
                return true;
            }
            return false;
        }

        #endregion 方法

        #region 命令

        public ICommand LoadedCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion 命令
    }
}