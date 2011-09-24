using System.IO;
using System.Net.Sockets;

namespace SimpleServer
{
    public class Connection : IConnection
    {
        private readonly Socket _socket;

        public Connection(Socket socket)
        {
            _socket = socket;
        }

        public Stream Stream()
        {
            var buffer = new byte[_socket.ReceiveBufferSize];
            _socket.Receive(buffer);
            return new MemoryStream(buffer);
        }
    }
}