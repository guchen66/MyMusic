using System.Windows;


namespace Music.ToolKit.I18nResource;
public static class I18nProvider
{

    public static readonly ComponentResourceKey Input_Name = new ComponentResourceKey(typeof(I18nResource), nameof(I18nResource.Input_Name));
    public static readonly ComponentResourceKey Input_PassWord = new ComponentResourceKey(typeof(I18nResource), nameof(I18nResource.Input_PassWord));
    public static readonly ComponentResourceKey Login = new ComponentResourceKey(typeof(I18nResource), nameof(I18nResource.Login));
    public static readonly ComponentResourceKey Logout = new ComponentResourceKey(typeof(I18nResource), nameof(I18nResource.Logout));
    public static readonly ComponentResourceKey PassWord = new ComponentResourceKey(typeof(I18nResource), nameof(I18nResource.PassWord));
    public static readonly ComponentResourceKey UserName = new ComponentResourceKey(typeof(I18nResource), nameof(I18nResource.UserName));
}
