using IT.Tangdao.Core.Abstractions.Loggers;
using Music.Core.Account;
using Music.Mah.I18n;
using Music.Shared.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Abstractions.FileAccessor;
using MahApps.Metro.Controls.Dialogs;
using Music.Shared.Dtos;
using Music.Core.Repositorys;
using SqlSugar;
using IT.Tangdao.Core.Enums;
using Music.Shared.Entitys;
using MapsterMapper;
using Prism.Dialogs;

namespace Music.Mah.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        private ITangdaoLogger _logger = TangdaoLogger.Get(typeof(LoginWindowViewModel));

        private readonly LoginAccount _loginAccount;
        private readonly IReadService _readService;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public LoginWindowViewModel(IEventAggregator eventAggregator, LoginAccount loginAccount, ILoginRepository loginRepository, IMapper mapper)
        {
            _eventAggregator = eventAggregator;
            _loginRepository = loginRepository;
            _loginAccount = loginAccount;
            _mapper = mapper;
            // CloseingCommand = new DelegateCommand(ExecuteClose);
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

        private IEventAggregator _eventAggregator;

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