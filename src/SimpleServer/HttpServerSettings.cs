namespace SimpleServer
{
    public class HttpServerSettings
    {
        public HttpServerSettings()
        {
            Port = 8181;
        }

        public int Port { get; set; }
    }
}