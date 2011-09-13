namespace SimpleServer
{
    public interface IHttpListenerFactory
    {
        IHttpListener Create();
    }
}