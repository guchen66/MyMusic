using Mapster;
using Music.Shared.Dtos;
using Music.Shared.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core.Account
{
    public class UserRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .ForType<LoginInputDto, LoginInputInfo>()
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.LoginTime, src => src.LoginTime);

            config
                .ForType<LoginInputInfo, LoginInputDto>()
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.LoginTime, src => src.LoginTime);
        }
    }
}