using Music.System.Services.HttpTools.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Music.System.Services.HttpTools
{
    public interface IHttpClientService// : IHttpClientFactory
    {
        public HttpClient CreateClient();

        /// <summary>
        /// Get方法请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<byte[]> GetAsync(string url, HttpClientDto httpClientArgs);

        /// <summary>
        /// Post方法请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="httpClientArgs"></param>
        /// <returns></returns>
        public Task<byte[]> PostAsync(string url, byte[] data, HttpClientDto httpClientArgs);


        /// <summary>
        /// Post方法请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="httpClientArgs"></param>
        /// <returns></returns>
        public Task<byte[]> PostAsync(string url, byte[] data);
    }
}
