namespace SimpleServer
{
    public interface IHttpListener
    {
        void Listen(int port);
        void Stop();
    }
}