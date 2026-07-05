namespace Music.System.Services.LoginSign
{
    /// <summary>
    /// 登录服务
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        Task<bool> RegisterAsync();

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        Task<bool> LoginAsync(LoginInputDto loginArgs);

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();
    }
}