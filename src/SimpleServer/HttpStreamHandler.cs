using System.IO;

namespace SimpleServer
{
    public class HttpStreamHandler : IStreamHandler
    {
        private readonly IHttpRequestFactory _factory;
        private readonly IHttpPipeline _pipeline;

        public HttpStreamHandler(IHttpRequestFactory factory, IHttpPipeline pipeline)
        {
            _factory = factory;
            _pipeline = pipeline;
        }

        public void Handle(Stream stream)
        {
            _pipeline.Execute(_factory.BuildFrom(stream));
        }
    }
}