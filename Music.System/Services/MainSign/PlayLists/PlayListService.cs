
namespace Music.System.Services.MainSign.PlayLists
{

    public class PlayListService : DataRepository<PlayListInfo>, IPlayListService
    {

        public async Task<bool> CreatePlatListAsync(PlayListInputDto input)
        {
            var playList = input.Adapt<PlayListInfo>();//实体转换       父类转子类
            return await InsertAsync(playList);//添加数据
        }
        /// <summary>
        /// 检查输入参数
        /// </summary>
        /// <param name="sysUser"></param>
        private async Task CheckInput(PlayListInputDto inputInfo)
        {

            //判断歌单重复,直接从redis拿
            /* var accountId = await GetIdByAccount(sysUser.Account);
             if (accountId > 0 && accountId != sysUser.Id)
                 throw Oops.Bah($"存在重复的账号:{sysUser.Account}");*/

            //判断歌单名称不为空，并且必须满足是数字汉字和字母
            if (string.IsNullOrEmpty(inputInfo.PlayListName))
            {
                MessageBox.Show("歌单不能为空");
                /* if (RegexComponent.IsChineseOrNumberOrWord(inputInfo.PlayListName))
                 {
                     inputInfo.PlayListName=
                 }
                 if (!sysUser.Phone.MatchPhoneNumber())//验证手机格式
                     throw Oops.Bah($"手机号码：{sysUser.Phone} 格式错误");
                 var phoneId = await GetIdByPhone(sysUser.Phone);
                 if (phoneId > 0 && sysUser.Id != phoneId)//判断重复
                     throw Oops.Bah($"存在重复的手机号:{sysUser.Phone}");
                 sysUser.Phone = CryptogramUtil.Sm4Encrypt(sysUser.Phone);*/
            }
        }

        public async Task<bool> RemovePlatListById(long? id)
        {
            return await DeleteByIdAsync(id);
        }

        public async Task<PlayListInfo> GetPlatListByNameAsync(string name)
        {

            return await GetFirstAsync(x => x.PlayListName == name);
        }

        public async Task<List<PlayListInfo>> GetAllPlayList()
        {
            //  var playList = input.Adapt<PlayListInfo>();//实体转换       父类转子类
            return await GetListAsync();
        }

        public async Task<PlayListInfo> GetPlayListByIdAsync(long? id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<bool> GetReNamePlatListAsync(PlayListInfo playListInfo)
        {

            //  var reName = playListInfo.Adapt<PlayListInfo>();//实体转换       父类转子类
            return await UpdateAsync(playListInfo);
        }
    }
}
