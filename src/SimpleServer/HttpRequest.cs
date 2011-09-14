using System;

namespace SimpleServer
{
    public class HttpRequest : IHttpRequest
    {
        public Uri Url { get; set; }
        public string HttpMethod { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
    }
}