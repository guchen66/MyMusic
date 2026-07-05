namespace Music.Shared.Dtos
{
    public class SettingDto : BaseDto
    {
        private string _setTitle;

        public string SetTitle
        {
            get => _setTitle;
            set => SetProperty(ref _setTitle, value);
        }
    }
}