namespace SimpleServer
{
    public interface IHttpPipeline
    {
        void Execute(IHttpRequest request);
    }
}