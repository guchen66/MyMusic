
using Mapster;
using MapsterMapper;
using Music.Shared.Dtos;
using Music.SqlSugar.IRepositorys;
using Prism.Ioc;
namespace Music.SqlSugar.Services
{
    public class AsideCreateControlService : DataService<AsideCreateController>, IAsideCreateControlService
    {
        private readonly IAsideCreateControlRepository _db;
        public IMapper _mapper;
        public AsideCreateControlService(IAsideCreateControlRepository repository) : base(repository)
        {
            _db = repository;
            _mapper= ContainerLocator.Container.Resolve<IMapper>();
        }

        public async Task<bool> CreatePlatListAsync(AsideCreateControllerDto inputDto)
        {
            var input = inputDto.Adapt<AsideCreateController>();
            return await _db.AddAsync(input);
        }
        /// <summary>
        /// 检查输入参数
        /// </summary>
        /// <param name="sysUser"></param>
        private async Task CheckInput(AsideCreateControllerDto inputInfo)
        {

            //判断歌单重复,直接从redis拿
            /* var accountId = await GetIdByAccount(sysUser.Account);
             if (accountId > 0 && accountId != sysUser.Id)
                 throw Oops.Bah($"存在重复的账号:{sysUser.Account}");*/

            //判断歌单名称不为空，并且必须满足是数字汉字和字母
           /* if (string.IsNullOrEmpty(inputInfo.Name))
            {
                MessageBox.Show("歌单不能为空");
                *//* if (RegexComponent.IsChineseOrNumberOrWord(inputInfo.PlayListName))
                 {
                     inputInfo.PlayListName=
                 }
                 if (!sysUser.Phone.MatchPhoneNumber())//验证手机格式
                     throw Oops.Bah($"手机号码：{sysUser.Phone} 格式错误");
                 var phoneId = await GetIdByPhone(sysUser.Phone);
                 if (phoneId > 0 && sysUser.Id != phoneId)//判断重复
                     throw Oops.Bah($"存在重复的手机号:{sysUser.Phone}");
                 sysUser.Phone = CryptogramUtil.Sm4Encrypt(sysUser.Phone);*//*
            }*/
        }

        public async Task<bool> RemovePlatListById(long? id)
        {
            var ids = id.Value.ToInt();
            return await _db.DeleteAsync(ids);
        }

        public async Task<AsideCreateControllerDto> GetPlatListByNameAsync(string name)
        {

            return new AsideCreateControllerDto();
        }


        public async Task<AsideCreateControllerDto> GetPlayListByIdAsync(long? id)
        {
            return new AsideCreateControllerDto();
        }

        public async Task<bool> GetReNamePlatListAsync(AsideCreateController playListInfo)
        {

            //  var reName = playListInfo.Adapt<PlayListInfo>();//实体转换       父类转子类
            return await UpdateAsync(playListInfo);
        }
    }
}
