using IT.Tangdao.Core.Abstractions.FileAccessor;
using Music.Mah.Models;
using Music.Mah.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Mah.Shell.ViewModels
{
    public partial class AsideViewModel : BaseViewModel
    {
        private IReadOnlyList<AsideMenuModel> _asideMenuList;

        public IReadOnlyList<AsideMenuModel> AsideMenuList
        {
            get => _asideMenuList;
            set => SetProperty(ref _asideMenuList, value);
        }

        public AsideViewModel(IContainerProvider provider) : base(provider)
        {
            LoadedCommand = new DelegateCommand(ExecuteLoaded);
            NavigateCommand = new DelegateCommand<string>(ExecuteNavigateToView);
        }

        private void ExecuteLoaded()
        {
            var tangdaoDict = _readService.Default.AsConfig().SelectAppConfig("Aside").Data;
            IEnumerable<AsideMenuModel> asideMenuModels = tangdaoDict.Select(x => new AsideMenuModel(x.Key, x.Value));
            AsideMenuList = asideMenuModels.ToArray();
        }

        private void ExecuteNavigateToView(string navigatePath)
        {
            NavigateToView(navigatePath);
        }

        public ICommand LoadedCommand { get; set; }

        public ICommand NavigateCommand { get; set; }
    }
}