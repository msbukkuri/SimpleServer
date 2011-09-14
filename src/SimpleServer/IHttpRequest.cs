using System;

namespace SimpleServer
{
    public interface IHttpRequest
    {
        Uri Url { get; }
        string HttpMethod { get; }
        int ContentLength { get; }
        string ContentType { get; }
    }
}