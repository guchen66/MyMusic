using IT.Tangdao.Core.Abstractions.FileAccessor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.MD.Models
{
    public class AsideMenuModel
    {
        public string Title { get; set; }

        public string View { get; set; }

        public AsideMenuModel(string title, string view)
        {
            Title = title;
            View = view;
        }
    }

    public class AsideMenuProvider
    {
        public IReadOnlyList<AsideMenuModel> asideMenuModels { get; set; }

        private readonly IReadService _readService;

        public AsideMenuProvider(IReadService readService)
        {
            _readService = readService;
        }

        public IReadOnlyList<AsideMenuModel> Get()
        {
            var tangdaoDict = _readService.Default.AsConfig().SelectAppConfig("Aside").Data;
            IEnumerable<AsideMenuModel> asideMenuModels = tangdaoDict.Select(x => new AsideMenuModel(x.Key, x.Value));
            return asideMenuModels.ToArray();
        }
    }
}