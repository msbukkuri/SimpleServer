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
            var settings = new HttpServerSettings() {Port = 3000};
            Container.Inject(settings);

            //Arrange
            MockFor<IHttpListenerFactory>()
                .Expect(listener => listener.Create())
                .Return(MockFor<IHttpListener>());
            MockFor<IHttpListener>()
                .Expect(listener => listener.Listen(settings.Port));
            
            //Act
            ClassUnderTest.Start();
            VerifyCallsFor<IHttpListener>();

            //Assert
            
        }
    }

    public interface IHttpListener
    {
        void Listen(int port);
    }

    public interface IHttpListenerFactory
    {
        IHttpListener Create();
    }

    public class HttpServer : IHttpServer
    {
        private HttpServerSettings _settings;
        private IHttpListenerFactory _factory;
        public HttpServer(HttpServerSettings settings, IHttpListenerFactory factory)
        {
            _settings = settings;
            _factory = factory;
        }   

        public void Start()
        {
            _factory.Create().Listen(_settings.Port);
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }

    public class HttpServerSettings
    {
        public int Port { get; set; }
    }
}