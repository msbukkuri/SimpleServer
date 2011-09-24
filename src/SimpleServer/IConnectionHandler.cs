namespace SimpleServer
{
    public interface IConnectionHandler
    {
        void Handle(IConnection connection);
    }
}