using Music.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.ConverterDtoRegister
{
    public class AsideMenuRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<AsideMenu, AsideMenuDto>();//.Map(dest=>dest.ShowContent,table=>table.Content);
        }
    }
}
