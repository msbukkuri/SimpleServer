using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleServer.Tests
{
    public class ConnectionHandler: IConnectionHandler
    {
        public Stream Handle(IConnection connection)
        {
            throw new NotImplementedException();
        }
    }

    public interface IConnectionHandler
    {
        Stream Handle(IConnection connection);
    }
}
