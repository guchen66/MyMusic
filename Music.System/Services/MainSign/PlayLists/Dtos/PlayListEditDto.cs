
namespace Music.System.Services.MainSign.PlayLists.Dtos
{
    public class PlayListEditDto : PlayListInputDto
    {
        /// <summary>
        /// 重命名
        /// </summary>
        private string _reNameItem;

        public string ReNameItem
        {
            get { return _reNameItem; }
            set { SetProperty(ref _reNameItem, value); }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private string _removeItem;

        public string RemoveItem
        {
            get { return _removeItem; }
            set { SetProperty(ref _removeItem, value); }
        }
    }
}
