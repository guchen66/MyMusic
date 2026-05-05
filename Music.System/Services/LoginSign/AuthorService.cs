using IT.Tangdao.Core.Abstractions.Results;
using IT.Tangdao.Core.Helpers;

namespace Music.System.Services.LoginSign
{
    [AutoRegister(Mode = RegisterMode.Singleton)]
    public class AuthorService : IAuthorService
    {
        public Task<ResponseResult> ReflushTokenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult> VerifyAsync(LoginInputDto loginArgs)
        {
            if (loginArgs == null) return ResponseResult.Failure("验证失败，密码错误");
            LoginInputDto canLoginArgs = await IsCanLoginAsync();
            var result = canLoginArgs.UserName == loginArgs.UserName && canLoginArgs.Password == loginArgs.Password;
            if (result)
            {
                return ResponseResult.Success();
            }
            return ResponseResult.Failure("验证失败，密码错误");
        }

        private static async Task<LoginInputDto> IsCanLoginAsync()
        {
            var json = await TangdaoJsonFileHelper.GetJsonContentAsync("loginAccount.json", "Account");
            LoginInputDto loginDto = JsonConvert.DeserializeObject<LoginInputDto>(json);
            return loginDto;
        }
    }
}