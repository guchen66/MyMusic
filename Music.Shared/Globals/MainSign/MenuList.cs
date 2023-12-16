using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals.MainSign
{
    public class MenuList : BindableBase
    {
        public string Icon { get; set; }
        public string Content { get; set; }
        public string NameSpace { get; set; }

        public ObservableCollection<MenuList> Children { get; set; }

    }
}
