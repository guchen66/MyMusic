
namespace MyMusic.Utils
{
    public class WaitingUtil
    {
       /* public static void SetTile(string title)
        {
            if (SplashScreenManager.DefaultLoading != null) 
            {
               // SplashScreenManager.ShowSplashScreen();
            }
           
        }*/
    }
    public class SplashScreenManager
    {
        static SplashWindow splash = new SplashWindow();
        /// <summary>
        /// 显示等待窗口
        /// </summary>
        /// <returns></returns>
        public static void ShowSplashScreen()
        {
            splash.Show();
        }

        /// <summary>
        /// 关闭等待窗口
        /// </summary>
        /// <returns></returns>
        public static void CloseSplashScreen()
        {
            splash.Close();
        }
    }
}
