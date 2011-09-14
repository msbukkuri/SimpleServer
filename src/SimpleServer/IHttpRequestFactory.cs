using System.IO;

namespace SimpleServer
{
    public interface IHttpRequestFactory
    {
        IHttpRequest BuildFrom(Stream stream);
    }
}