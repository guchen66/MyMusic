using Music.Shared.Globals.MainSign.MyFavors;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyMusic.Shell.ViewModels.MyFavor
{
    public class SongViewModel:BindableBase
    {

        public ICommand InitCommand { get; set; }
        DispatcherTimer timer { get; set; }
        private FavorArgs _favorArgs;

        public FavorArgs FavorArgs
        {
            get { return _favorArgs; }
            set { SetProperty<FavorArgs>(ref _favorArgs, value); }
        }

        public SongViewModel()
        {
           
           // InitCommand = new DelegateCommand(ExecuteIniting);
        }

        private void ExecuteIniting()
        {
          /*  timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Tick += Timer_Tick;
            timer.Start();
*/
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() => { });
            FavorArgs.Song = DateTime.Now.ToString();
            //  ShowFavorData();
            // await Clear();
        }

    }
}
