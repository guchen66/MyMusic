using IT.Tangdao.Core.Abstractions.FileAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.MD.Shell.ViewModels
{
    public partial class AsideViewModel : BindableBase
    {
        private IReadService _readService;
        private IEventAggregator _eventAggregator;

        public AsideViewModel(IReadService readService, IEventAggregator eventAggregator)
        {
            _readService = readService;
            _eventAggregator = eventAggregator;
        }

        private void Get()
        {
        }
    }
}