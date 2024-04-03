
namespace Music.System.Services.MainSign.HeaderSign.Dtos
{
    public class MusicSourceDto : BaseDto
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        private string _source;

        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

    }
}
