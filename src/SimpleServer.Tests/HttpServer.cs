namespace SimpleServer.Tests
{
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