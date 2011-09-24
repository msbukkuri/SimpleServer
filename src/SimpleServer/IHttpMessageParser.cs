namespace SimpleServer
{
    public interface IHttpMessageParser
    {
        IHttpRequest Parse(string message);
    }
}