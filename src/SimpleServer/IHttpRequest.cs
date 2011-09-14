using System;

namespace SimpleServer
{
    public interface IHttpRequest
    {
        Uri Url { get; }
        string HttpMethod { get; }
        string ContentType { get; }
    }
}