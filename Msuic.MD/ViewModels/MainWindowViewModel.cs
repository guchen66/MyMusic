namespace Music.MD.ViewModels
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

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
            _regionManager.RegisterViewWithRegion(RegionNames.AsideRegion, typeof(AsideView));
            _regionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
            UpdateCommand = new DelegateCommand(ExecuteUpdate);
        }

        private void ExecuteUpdate()
        {
            string name = Thread.CurrentThread.CurrentUICulture.Name == "zh-CN" ? "en" : "zh-CN";
            I18nManager.CurrentUICulture = new CultureInfo(name); // 自动刷新
            ConfigHelper.SaveConfig("resxKey", name);
        }

        public ICommand UpdateCommand { get; set; }
    }
}