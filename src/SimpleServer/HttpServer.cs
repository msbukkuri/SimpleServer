namespace SimpleServer
{
    public class HttpServer : IHttpServer
    {
        private HttpServerSettings _settings;
        private readonly IHttpListener _listener;

        public HttpServer(HttpServerSettings settings, IHttpListener listener)
        {
            _settings = settings;
            _listener = listener;
        }

        public void Start()
        {
            _listener.Listen(_settings.Port);
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}