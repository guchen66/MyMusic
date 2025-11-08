using IT.Tangdao.Core.Abstractions.FileAccessor;
using Music.MD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.MD.Shell.ViewModels
{
    public partial class AsideViewModel : BindableBase
    {
        private IReadOnlyList<AsideMenuModel> _asideMenuList;

        public IReadOnlyList<AsideMenuModel> AsideMenuList
        {
            get => _asideMenuList;
            set => SetProperty(ref _asideMenuList, value);
        }

        public void Get1()
        {
            //Get();
        }
    }
}