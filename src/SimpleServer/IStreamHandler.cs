using System.IO;

namespace SimpleServer
{
    public interface IStreamHandler
    {
        void Handle(Stream stream);
    }
}