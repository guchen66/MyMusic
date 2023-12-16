
namespace MyMusic.Providers
{
    public class I18nProvider
    {
        public static string ReadConfig(string key)
        {
            Configuration congfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return congfig.AppSettings.Settings[key].Value.ToString();
        }

        public static void SaveConfig(string key, string value)
        {
            Configuration congfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            congfig.AppSettings.Settings[key].Value = value;
            congfig.Save();
        }
    }
   

}
