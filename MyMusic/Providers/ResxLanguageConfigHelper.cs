
namespace MyMusic.Providers
{
    internal class ResxLanguageConfigHelper
    {
        public static string resxKey = "resxKey";
        public static void Init()
        {
            I18nManager.Instance.Add(I18nResource.ResourceManager);
            var language = ConfigHelper.ReadConfig(resxKey);
            CultureInfo newCulture = new CultureInfo(language);
            I18nManager.Instance.CurrentUICulture = newCulture;
        }

        public static void SetLanguage(string languageTag)
        {
            CultureInfo currentUICulture = I18nManager.Instance.CurrentUICulture;
            if (languageTag == currentUICulture.Name) return;
            CultureInfo newCulture = new CultureInfo(languageTag);
            I18nManager.Instance.CurrentUICulture = newCulture;
            ConfigHelper.SaveConfig(resxKey, languageTag);
        }
    }
}
