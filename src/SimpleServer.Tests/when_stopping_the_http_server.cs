using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_stopping_the_http_server : InteractionContext<HttpServer>
    {
        [Test]
        public void should_build_and_start_http_listener()
        {
            MockFor<IHttpListener>()
                .Expect(listener => listener.Stop());

            ClassUnderTest.Stop();

            VerifyCallsFor<IHttpListener>();
        }
    }
}