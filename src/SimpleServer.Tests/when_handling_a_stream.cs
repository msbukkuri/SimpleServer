using System;
using System.IO;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_handling_a_stream : InteractionContext<HttpStreamHandler>
    {
        [Test]
        public void should_build_http_request_and_execute_pipeline()
        {
            var stream = new MemoryStream();
            var request = new NulloHttpRequest();

            MockFor<IHttpRequestFactory>()
                .Expect(factory => factory.BuildFrom(stream))
                .Return(request);

            MockFor<IHttpPipeline>()
                .Expect(pipeline => pipeline.Execute(request));

            ClassUnderTest
                .Handle(stream);

            VerifyCallsFor<IHttpPipeline>();
        }
    }

    // ConnectionHandler already uses IStreamHandler.
    // This is our entry point into the Http pipeline
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

    public interface IHttpPipeline
    {
        void Execute(IHttpRequest request);
    }

    public interface IHttpRequestFactory
    {
        IHttpRequest BuildFrom(Stream stream);
    }

    public interface IHttpRequest
    {
        Uri Url { get; }
        string HttpMethod { get; }
        int ContentLength { get; }
        string ContentType { get; }
    }

    public class NulloHttpRequest : IHttpRequest
    {
        public Uri Url { get; set; }
        public string HttpMethod { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
    }
}