
using Music.Shared.Dtos;

namespace Music.System.Services.MainSign.PlayLists.ShowData
{
    public class PlayListSignValue : BindableBase
    {
        public event EventHandler<PlayListArgs> CalculationCompleted;

        private void OnCalculationCompleted(PlayListInputDto result)
        {
            CalculationCompleted?.Invoke(this, new PlayListArgs(result));
        }

        public DataRepository<PlayListInfo> db = new DataRepository<PlayListInfo>();
        // 测得值数据
        public async Task<List<PlayListInfo>> GetResistanceData()
        {
            var result = await db.GetListAsync();//.Select(e => e.InsulResistive).ToList();
            return result;
        }

        // 计算数据的方法
        public async void CalculateData(object obj, Action backCall)
        {
            // 获取计算数据
            /*if ( GetResistanceData().Result == 0)
            {
               // OnCalculationCompleted(new ElectronicData());
            }
            else
            {
                
            }*/
            OnCalculationCompleted(new PlayListInputDto());

        }

    }



    public class PlayListArgs : EventArgs
    {
        public PlayListInputDto GetplayListInputDto { get; set; }

        public PlayListArgs(PlayListInputDto playListInputDto)
        {
            GetplayListInputDto = playListInputDto;
        }

    }
}
