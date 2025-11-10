using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Mah.Mvvm
{
    public class BaseViewModel : BindableBase
    {
        public readonly IReadService _readService;
        public readonly IEventAggregator _eventAggregator;
        public readonly IRegionManager _regionManager;
        public IRegionNavigationJournal _navigationJournal;
        private readonly IContainerProvider _containerProvider;

        public BaseViewModel(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
            _readService = containerProvider.Resolve<IReadService>();
            _eventAggregator = containerProvider.Resolve<IEventAggregator>();
            _navigationJournal = containerProvider.Resolve<IRegionNavigationJournal>();
            _regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void NavigateToView(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, arg =>
                {
                    _navigationJournal = arg.Context.NavigationService.Journal;
                });
        }
    }
}