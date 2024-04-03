using Music.Shared.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Dtos
{
    public class AsideCreateControllerDto:BaseDto
    {
        private string _playListName;

        public string PlayListName
        {
            get => _playListName;
            set => SetProperty(ref _playListName, value);
        }

        private bool _isExistContent;

        public bool IsExistContent
        {
            get => _isExistContent;
            set => SetProperty(ref _isExistContent, value);
        }


        private bool _isDelete;

        public bool IsDelete
        {
            get => _isDelete;
            set => SetProperty(ref _isDelete, value);
        }



        /* public EditPlayListInfo EditPlayListInfos { get; set; }

         public int PlayListUiId { get; set; }

         public PlayListUiInfo PlayListUiInfos { get; set; }*/
    }
}
