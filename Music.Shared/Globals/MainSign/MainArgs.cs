using Music.Shared.Consts;
using Music.Shared.Globals.MainSign.PlayListSign;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals.MainSign
{
    public class MainArgs : BindableBase
    {
        public static readonly MainArgs Instance = new Lazy<MainArgs>(() => new MainArgs()).Value;
        public MainArgs()
        {
            Init();
        }
        public ObservableCollection<MenuList> ListViewMenus { get; set; }
        public void Init()
        {
            //小项目，左侧标签静态写死
            ListViewMenus = new ObservableCollection<MenuList>()
            {
                  new MenuList(){Icon="Home",Content="首页",NameSpace=MenuNames.HomeView },
                  new MenuList(){Icon="Favorite",Content="收藏" ,NameSpace=MenuNames.FavorView},
                  new MenuList(){Icon="DownLoad",Content="下载",NameSpace=MenuNames.DownLoadView},
                  new MenuList(){Icon="History",Content="最近播放" ,NameSpace=MenuNames.RecentView},
                  new MenuList(){Icon="Settings",Content="设置" ,NameSpace=MenuNames.SetView},

            };
        }

       /* private void Regions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var region = (IRegion)e.NewItems[0];
                region.Views.CollectionChanged += Views_CollectionChanged;
            }
        }

        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ListViewMenus.Add(e.NewItems[0].GetType().Name);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ListViewMenus.Remove(e.OldItems[0].GetType().Name);
            }
        }*/
    }
}
