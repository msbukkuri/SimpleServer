using System.IO;

namespace SimpleServer
{
    public interface IStreamParser
    {
        string Parse(Stream stream);
    }
}