using IT.Tangdao.Core.Abstractions.FileAccessor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Mah.Models
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
}