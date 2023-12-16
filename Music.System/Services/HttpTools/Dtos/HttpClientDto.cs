using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.HttpTools.Dtos
{
    public class HttpClientDto
    {
        public string url;
        public byte[] data;
        public string contentType;
        public string referer;
        public string userAgent;
        public string accept;
        public int timeout;
        public CookieContainer cookies;
        public WebHeaderCollection headers;
        public WebProxy proxy;
        public Encoding encoding;
        public bool allowAutoRedirect = true;
        public bool autoCookieMerge = true;
    }
}
