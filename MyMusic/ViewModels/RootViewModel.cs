using IT.Tangdao.Core.Abstractions.Loggers;
using MyMusic.Views.Asides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModels
{
    public class RootViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private ITangdaoLogger logger = TangdaoLogger.Get<RootViewModel>();

        public RootViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _regionManager.RegisterViewWithRegion(RegionNames.RootRegion, typeof(SplashView));
            _eventAggregator.GetEvent<SplashEvent>().Subscribe(() => ShowMainPage(), ThreadOption.UIThread);
        }

        private void ShowMainPage()
        {
            _regionManager.RequestNavigate(RegionNames.RootRegion, new Uri("MainPageView", UriKind.Relative));
        }
    }
}