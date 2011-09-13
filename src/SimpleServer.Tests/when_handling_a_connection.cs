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
}