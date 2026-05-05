using IT.Tangdao.Core.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.LoginSign
{
    /// <summary>
    /// 用于权限认证
    /// </summary>
    public interface IAuthorService
    {
        Task<ResponseResult> VerifyAsync(LoginInputDto loginArgs);

        Task<ResponseResult> ReflushTokenAsync();
    }
}