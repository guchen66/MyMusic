using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyMusic.Styles
{
    public class ThemeManager
    {


        public string GetTheme()
        {
            return "Light";
        }
/*        private Uri _themeSource;

        public Uri ThemeSource
        {
            get { return _themeSource; }
            set
            {
                if (_themeSource != value)
                {
                    _themeSource = value;
                   RaisePropertyChanged(nameof(ThemeSource));
                }
            }
        }*/

        //<ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml" />
        //    <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml" />
    }
}
