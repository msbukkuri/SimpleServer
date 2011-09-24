namespace SimpleServer
{
    public interface IHttpRequestPolicy
    {
        bool Matches(IHttpRequest request);
        void Execute(IHttpRequest request);
    }
}