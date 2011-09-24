using System;

namespace SimpleServer
{
    public class HttpMessageParser : IHttpMessageParser
    {
        public IHttpRequest Parse(string message)
        {
            return new HttpRequest() { HttpMethod = ParseHttpMethod(message), ContentType = ParseContentType(message), Url = ParseUri(message) };
        }

        private string ParseHttpMethod(string message)
        {
            return message.Split(' ')[0];
        }

        private Uri ParseUri(string message)
        {
            return new Uri(message.Split(' ')[1]);
        }

        private string ParseContentType(string message)
        {
            //TODO: Set ContentType
            int startIndexOfContentType = message.IndexOf("Accept") + 8;
            int endIndexOfContentType = message.IndexOf(',', startIndexOfContentType);
            return message.Substring(startIndexOfContentType, endIndexOfContentType-startIndexOfContentType);
        }
    }
}