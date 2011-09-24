namespace SimpleServer
{
    public class ConnectionHandler : IConnectionHandler
    {
        private readonly IStreamHandler _handler;

        public ConnectionHandler(IStreamHandler handler)
        {
            _handler = handler;
        }

        public void Handle(IConnection connection)
        {
            _handler
                .Handle(connection.Stream());
        }
    }
}