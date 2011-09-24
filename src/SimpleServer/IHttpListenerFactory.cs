namespace SimpleServer
{
    public interface IHttpListenerFactory
    {
        IHttpListener Create();
    }
    
    public class HttpListenerFactory : IHttpListenerFactory
    {
        public IHttpListener Create()
        {
            throw new System.NotImplementedException();
        }
    }
}