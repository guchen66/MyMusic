namespace MyMusic.Providers
{
    public class ConfigHelper
    {
        public static string ReadConfig(string key)
        {
            Configuration congfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return congfig.AppSettings.Settings[key].Value.ToString();
        }

        public static void SaveConfig(string key, string value)
        {
            Configuration congfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            congfig.AppSettings.Settings[key].Value = value;
            congfig.Save();
        }
    }
}