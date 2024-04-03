
namespace MyMusic.Shell.ViewModels.MyFavor
{
    public class FavorViewModel : BindableBase
    {
        DispatcherTimer timer;
      

        public FavorViewModel() 
        {
           
          //  InitCommand = new DelegateCommand(ExecuteIniting);
        }
        public ICommand InitCommand { get; set; }



        #region 方法

        private void ExecuteIniting()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            ShowFavorData();
           
        }


        private void ShowFavorData() 
        {

          /*  int number = FavorArgs.Number.ToInt();
            // 使用 LINQ 截取数字部分并拼接回去
            string newNumberPart = string.Concat(number.ToString().Select(c => char.IsDigit(c) ? c.ToString() : ""));
            string newName = FavorArgs.Song + newNumberPart;
            FavorArgs.Song = newName;*/
            /*  lock (FavorArgs.Song)
              {
                  FavorArgs.Song= "4444";
                *//*  *//* if (ShowDataTem.Song.TestDatas.Count > 100)
                  {
                      ShowDataTem.Song.TestDatas.RemoveRange(0, ShowDataTem.Song.TestDatas.Count - 100);
                  }
                  *//**//*

              }*/
        }
      
        #endregion
    }
}
