using Music.Shared.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Dtos
{
    public class AsideCreateControlDto:BaseDto
    {
       
        public virtual string? PlayListName { get; set; }

        public int PlayListArgsId { get; set; }

        public EditPlayListInfo EditPlayListInfos { get; set; }

        public int PlayListUiId { get; set; }

        public PlayListUiInfo PlayListUiInfos { get; set; }
    }
}
