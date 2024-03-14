
namespace Music.Shared.Globals.MainSign.PlayListSign
{
    /// <summary>
    /// 播放屏幕
    /// </summary>
    public class PlayScreenArgs
    {
        //屏幕内容
        public string Content { get; set; }

        public string Title { get; set; }

        object DataContext { get; set; }

        /// <summary>
        /// Called when the window is loaded.
        /// </summary>
        event RoutedEventHandler Loaded;

        /// <summary>
        /// Called when the window is closed.
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        /// Called when the window is closing.
        /// </summary>
        event CancelEventHandler Closing;

        /// <summary>
        /// The result of the dialog.
        /// </summary>
      //  IDialogResult Result { get; set; }

        /// <summary>
        /// The window style.
        /// </summary>
        Style Style { get; set; }
    }
}
