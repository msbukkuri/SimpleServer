using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_starting_the_http_server : InteractionContext<HttpServer>
    {
        [Test]
        public void should_build_and_start_http_listener()
        {
            var settings = new HttpServerSettings() { Port = 3000 };
            Container.Inject(settings);

            MockFor<IHttpListenerFactory>()
                .Expect(listener => listener.Create())
                .Return(MockFor<IHttpListener>());

            MockFor<IHttpListener>()
                .Expect(listener => listener.Listen(settings.Port));

            ClassUnderTest.Start();

            VerifyCallsFor<IHttpListener>();
        }
    }
}