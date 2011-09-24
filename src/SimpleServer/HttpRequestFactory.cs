using System.IO;

namespace SimpleServer
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        private readonly IStreamParser _streamParser;
        private readonly IHttpMessageParser _messageParser;

        public HttpRequestFactory(IStreamParser streamParser, IHttpMessageParser messageParser)
        {
            _streamParser = streamParser;
            _messageParser = messageParser;
        }

        public IHttpRequest BuildFrom(Stream stream)
        {
            return _messageParser.Parse(_streamParser.Parse(stream));
        }
    }
}