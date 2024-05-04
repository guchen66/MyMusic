using Downloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Managers
{
    public static class DownLoadManager
    {
        public static DownloadConfiguration CreateConfig()
        {
            var downloadOpt = new DownloadConfiguration()
            {
                BufferBlockSize = 10240, // 通常，主机最大支持8000字节，默认值为8000。
                ChunkCount = 8, // 要下载的文件分片数量，默认值为1
                MaximumBytesPerSecond = 1024 * 1024, // 下载速度限制为1MB/s，默认值为零或无限制
                MaxTryAgainOnFailover = int.MaxValue, // 失败的最大次数
                //   OnTheFlyDownload = false, // 是否在内存中进行缓存？ 默认值是true
                ParallelDownload = true, // 下载文件是否为并行的。默认值为false
                //    TempDirectory = "C:\\temp", // 设置用于缓冲大块文件的临时路径，默认路径为Path.GetTempPath()。
                Timeout = 1000, // 每个 stream reader  的超时（毫秒），默认值是1000
                RequestConfiguration = // 定制请求头文件
                {
                    Accept = "*/*",
                    AutomaticDecompression =
                        DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer = new CookieContainer(), // Add your cookies
                    Headers = new WebHeaderCollection(), // Add your custom headers
                    KeepAlive = false,
                    ProtocolVersion = HttpVersion.Version11, // Default value is HTTP 1.1
                    UseDefaultCredentials = false,
                    UserAgent =
                        $"DownloaderSample/{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}"
                }
            };
            return downloadOpt;
        }

        /// <summary>
        /// 对于网络问题，或者断电，应该先引入一个临时文件
        /// 确保url这是文件的直接下载链接
        /// </summary>
        /// <returns></returns>
        public static async Task ExecuteDownLoad(string url)
        {
            var downloader = new DownloadService(CreateConfig());
            var file = @"E:\MyFolder\最终文件.html";
            await downloader.DownloadFileTaskAsync(url, file);
            // Provide `FileName` and `TotalBytesToReceive` at the start of each downloads
            // 在每次下载开始时提供 "文件名 "和 "要接收的总字节数"。
            //   downloader.DownloadStarted += OnDownloadStarted;

            // Provide any information about chunker downloads, like progress percentage per chunk, speed, total received bytes and received bytes array to live streaming.
            // 提供有关分块下载的信息，如每个分块的进度百分比、速度、收到的总字节数和收到的字节数组，以实现实时流。
            //  downloader.ChunkDownloadProgressChanged += OnChunkDownloadProgressChanged;

            // Provide any information about download progress, like progress percentage of sum of chunks, total speed, average speed, total received bytes and received bytes array to live streaming.
            // 提供任何关于下载进度的信息，如进度百分比的块数总和、总速度、平均速度、总接收字节数和接收字节数组的实时流。
            //   downloader.DownloadProgressChanged += OnDownloadProgressChanged;

            // Download completed event that can include occurred errors or cancelled or download completed successfully.
            // 下载完成的事件，可以包括发生错误或被取消或下载成功。
            //  downloader.DownloadFileCompleted += OnDownloadFileCompleted;

        }
    }
}
