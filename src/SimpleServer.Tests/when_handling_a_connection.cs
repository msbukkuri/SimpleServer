using System.IO;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_handling_a_connection : InteractionContext<ConnectionHandler>
    {
        [Test]
        public void should_get_stream_and_delegate_to_stream_handler()
        {
            var stream = new MemoryStream();
            MockFor<IConnection>()
                .Expect(c => c.Stream())
                .Return(stream);

            MockFor<IStreamHandler>()
                .Expect(handler => handler.Handle(stream));

            ClassUnderTest
                .Handle(MockFor<IConnection>());

            VerifyCallsFor<IStreamHandler>();
        }
    }

    public interface IStreamHandler
    {
        void Handle(Stream stream);
    }

    public interface IConnection
    {
        Stream Stream();
    }

    // we only add interfaces when we get consumed by something. We'll eventually be consumed 
    // but for now, there's no need (YAGNI)
    public class ConnectionHandler
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