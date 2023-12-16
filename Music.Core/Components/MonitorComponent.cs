using Music.Shared.Common.Root;
using Music.Shared.Common.Root.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Music.Core.Components
{
    public class MonitorComponent
    {

        public event EventHandler<RootJsonFileEventArgs> MonitorCompleted;
        /// <summary>
        /// 监控电脑目录下改动的文件
        /// </summary>
        /// <param name="path"></param>
        FileSystemWatcher watcher=null;
        public async void GetChangeFiles(string monitorPath)
        {
            //监控文件的根目录
            monitorPath = DirectoryComponent.SelectRootDirectory();
            // 创建一个新的 FileSystemWatcher 对象
             watcher = new FileSystemWatcher();

            // 设置要监视的文件夹路径
            watcher.Path = monitorPath;

            // 监视所有文件的创建、修改、删除和重命名事件
            watcher.NotifyFilter = NotifyFilters.Attributes |
                                   NotifyFilters.CreationTime |
                                   NotifyFilters.FileName |
                                   NotifyFilters.LastWrite |
                                   NotifyFilters.Size |
                                   NotifyFilters.Security;

            // 设置是否监视子文件夹
            watcher.IncludeSubdirectories = true;

            // 添加事件处理程序
            watcher.Changed += OnFileChanged;
            watcher.Created += OnFileChanged;
            watcher.Deleted += OnFileChanged;
            watcher.Renamed += OnFileRenamed;
            
            RootJsonFile rootJsonFile = new RootJsonFile();
            OnMonitorCompleted(rootJsonFile);
            // 开始监视
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("正在监视文件的打开情况，程序关闭 停止监视。");
            Console.ReadLine();
        }
        public void OnMonitorCompleted(RootJsonFile rootJsonFile)
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                Console.WriteLine("停止监视文件的变化。");
            }
            MonitorCompleted?.Invoke(this, new RootJsonFileEventArgs(rootJsonFile));
        }

        static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"文件 {e.Name} 在 {DateTime.Now} 被打开或修改了。");
        }

        static void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"文件 {e.OldName} 重命名为 {e.Name}，操作发生在 {DateTime.Now}。");
        }
    }
}
