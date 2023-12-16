
namespace Music.System.Services.MainSign.HeaderSign
{
    public interface IStateService
    {
        string GetHeader(string key);

        Task<bool> SaveState(List<MusicSourceArgs> musicSourceList);

        Task<bool> RemoveState(MusicSourceDto musicSource);
    }
}
