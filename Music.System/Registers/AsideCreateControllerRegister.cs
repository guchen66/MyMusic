using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Registers
{
    /// <summary>
    /// Mapster识别IRegister，自动注入
    /// </summary>
    public class AsideCreateControllerRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<AsideCreateController, AsideCreateControllerDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest=>dest.PlayListName, src=>src.Name)
                .Map(dest=>dest.IsExistContent,src=>src.IsFullContent);

            config.ForType<AsideCreateControllerDto, AsideCreateController>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.Name, src => src.PlayListName)
               .Map(dest => dest.IsFullContent, src => src.IsExistContent);
        }     
    }
}
