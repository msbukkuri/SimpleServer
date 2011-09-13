using System.IO;

namespace SimpleServer
{
    public interface IConnection
    {
        Stream Stream();
    }
}