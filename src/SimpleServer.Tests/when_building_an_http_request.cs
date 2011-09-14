using System.IO;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_building_an_http_request : InteractionContext<HttpRequestFactory>
    {
        [Test]
        public void should_parse_message_and_parse_request()
        {
            var stream = new MemoryStream();
            var msg = "test";
            var request = new HttpRequest();

            MockFor<IStreamParser>()
                .Expect(p => p.Parse(stream))
                .Return(msg);

            MockFor<IHttpMessageParser>()
                .Expect(p => p.Parse(msg))
                .Return(request);

            ClassUnderTest
                .BuildFrom(stream)
                .ShouldEqual(request);
        }
    }

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

    public interface IHttpMessageParser
    {
        IHttpRequest Parse(string message);
    }

    public interface IStreamParser
    {
        string Parse(Stream stream);
    }
}