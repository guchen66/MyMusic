using Music.Shared.Globals.MainSign.MyFavors;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyMusic.Shell.ViewModels.MyFavor
{
    public class FavorViewModel : BindableBase
    {
        DispatcherTimer timer;
        private FavorArgs _favorArgs;
        public FavorArgs FavorArgs
        {
            get { return _favorArgs; }
            set { SetProperty<FavorArgs>(ref _favorArgs, value); }
        }

        public FavorViewModel() 
        {
            FavorArgs = new FavorArgs();
            // FavorArgs.Song = "4444";
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
            
            ShowFavorData();
           
        }


        private void ShowFavorData() 
        {

            int number = FavorArgs.Number.ToInt();
            // 使用 LINQ 截取数字部分并拼接回去
            string newNumberPart = string.Concat(number.ToString().Select(c => char.IsDigit(c) ? c.ToString() : ""));
            string newName = FavorArgs.Song + newNumberPart;
            FavorArgs.Song = newName;
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
