using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Mah.Shell.ViewModels.Aside
{
    public class FavoriteViewModel : BindableBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public FavoriteViewModel()
        {
            Name = "Hello";
        }
    }
}