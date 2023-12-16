using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals.MainSign.MyFavors
{
    public class FavorArgs : BindableBase
    {

        private string _song;

        public string Song
        {
            get { return _song; }
            set { SetProperty(ref _song, value); }
        }

        private string _playList;

        public string PlayList
        {
            get { return _playList; }
            set { SetProperty(ref _playList, value); }
        }
        private string _album;

        public string Album
        {
            get { return _album; }
            set { SetProperty(ref _album, value); }
        }

        private string _number;

        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }


    }
}
