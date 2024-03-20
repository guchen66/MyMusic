using Music.Shared.Attributes;
using Music.System.Services.HttpTools.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.HttpTools
{
    [Scanning(RegisterType = "Singleton")]
    public class HttpClientService : IHttpClientService
    {
        public HttpClient CreateClient()
        {
            return new HttpClient();
        }

        public async Task<byte[]> GetAsync(string url, HttpClientDto httpClientArgs)
        {
            httpClientArgs.headers = new WebHeaderCollection();
            //处理Http的响应
            HttpClientHandler handler = new HttpClientHandler();
            //重定向自动跟随
            handler.AllowAutoRedirect = true;

            //启用cookie自动处理
            handler.UseCookies = true;
            handler.CookieContainer = new CookieContainer();
            //忽略SSL证书
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            HttpClient client = new HttpClient(handler);
            //模拟浏览器的标头
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");

            HttpResponseMessage response = await client.GetAsync(url);
            //  response.EnsureSuccessStatusCode();
            byte[] result = await response.Content.ReadAsByteArrayAsync();
            return result;
        }

        /// <summary>
        /// 处理普通的歌曲
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<byte[]> PostAsync(string url, byte[] data)
        {
            //处理Http的响应
            HttpClientHandler handler = new HttpClientHandler();

            //重定向自动跟随
            handler.AllowAutoRedirect = true;

            //启用cookie自动处理
            handler.UseCookies = true;
            handler.CookieContainer = new CookieContainer();
            HttpClient client = new HttpClient(handler);
            //模拟浏览器的标头
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
            HttpContent content = new ByteArrayContent(data);

            //设置Post请求内容的类型
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            byte[] result = await response.Content.ReadAsByteArrayAsync();
            string s = Encoding.UTF8.GetString(result);
            return result;
        }

        /// <summary>
        /// 处理VIP歌曲
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<byte[]> PostAsync(string url, byte[] data, HttpClientDto httpClientDto)
        {
            httpClientDto.headers = new WebHeaderCollection();

            //处理Http的响应
            HttpClientHandler handler = new HttpClientHandler();

            //重定向自动跟随
            handler.AllowAutoRedirect = true;

            //启用cookie自动处理
            handler.UseCookies = true;
            handler.CookieContainer = new CookieContainer();
            HttpClient client = new HttpClient(handler);
            //模拟浏览器的标头
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
            HttpContent content = new ByteArrayContent(data);

            //设置Post请求内容的类型
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            byte[] result = await response.Content.ReadAsByteArrayAsync();
            string s = Encoding.UTF8.GetString(result);
            return result;
        }
    }
}
