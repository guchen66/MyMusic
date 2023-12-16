
namespace MyMusic.Providers
{
    public class I18NService : II18NService
    {
        private static readonly string resxKey = "resxKey";
        private readonly string _defaultLanguageTag = "zh-CN";

        public void Init()
        {
            I18nManager.Instance.Add(I18nResource.ResourceManager);
            var language = ConfigHelper.ReadConfig(resxKey);
            if (string.IsNullOrEmpty(language))
            {
                language = _defaultLanguageTag;
                ConfigHelper.SaveConfig(resxKey, language);
            }
            CultureInfo newCulture = new CultureInfo(language);
            I18nManager.Instance.CurrentUICulture = newCulture;
        }

        public void SetLanguage(string languageTag)
        {
            CultureInfo currentUICulture = I18nManager.Instance.CurrentUICulture;
            if (languageTag == currentUICulture.Name) return;
            CultureInfo newCulture = new CultureInfo(languageTag);
            I18nManager.Instance.CurrentUICulture = newCulture;
            ConfigHelper.SaveConfig(resxKey, languageTag);
        }
    }

    public interface II18NService
    {
        void Init();
        void SetLanguage(string languageTag);
    }
}
