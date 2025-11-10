using IT.Tangdao.Core.Abstractions.FileAccessor;
using Music.Mah.Shell.Views;
using Prism.Dialogs;
using Prism.Mvvm;
using System.Configuration;

namespace Music.Mah.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
            _regionManager.RegisterViewWithRegion(RegionNames.AsideRegion, typeof(AsideView));
            _regionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
            UpdateCommand = new DelegateCommand(ExecuteUpdate);
            LoadedCommand = new DelegateCommand(ExecuteLoaded);
        }

        private void ExecuteLoaded()
        {
            _dialogService.Show("LoginSucess", null, null, "ToastWindow");
        }

        private void ExecuteUpdate()
        {
            string name = Thread.CurrentThread.CurrentUICulture.Name == "zh-CN" ? "en" : "zh-CN";
            I18nManager.CurrentUICulture = new CultureInfo(name); // 自动刷新
            ConfigHelper.SaveConfig("resxKey", name);
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand LoadedCommand { get; set; }
    }
}