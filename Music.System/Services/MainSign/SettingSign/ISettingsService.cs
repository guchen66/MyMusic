
namespace Music.System.Services.MainSign.SettingSign
{
    /// <summary>
    /// 保存加载用户保存CheckBox和RadioButton的信息
    /// </summary>
    public interface ISettingsService
    {
        [Obsolete("封闭性太差，已弃用，改成SaveDataSateInfos")]
        void SaveSetStateInfos<T>(ObservableCollection<T> stateInfos);

        /// <summary>
        /// 保存页面的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stateInfos"></param>
        void SaveDataStateInfos<T>(ObservableCollection<T> stateInfos);

        /// <summary>
        /// 获取具体的页面的id
        /// </summary>
        /// <param name="stateIffo"></param>
        /// <returns></returns>
        int GetId(object stateIffo);
        // ObservableCollection<T> LoadState<T>();
    }
}
