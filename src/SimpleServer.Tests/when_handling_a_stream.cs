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
            var request = new HttpRequest();

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
}