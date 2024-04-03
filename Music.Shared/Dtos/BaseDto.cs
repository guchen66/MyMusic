
namespace Music.Shared.Dtos
{
    public class BaseDto:BindableBase
    {
		private long _id;

		public long Id
		{
			get => _id;
			set => SetProperty(ref _id, value);
		}

	}
}
