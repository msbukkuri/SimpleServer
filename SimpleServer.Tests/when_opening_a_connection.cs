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
        public void should_parse_message()
        {
            MockFor<IConnection>()
                .Expect(connection => connection.GetStream())
                .Return(MockFor<Stream>());
            
            //Get the stream
            //IStreamReceiver

            //Hand it off to the parser
            //IStreamParser


            ClassUnderTest.Handle(MockFor<IConnection>());
        }

        [Test]
        public void should_close_connection()
        {
            MockFor<IConnection>()
                .Expect(connection => connection.Close());

            ClassUnderTest.Handle(MockFor<IConnection>());
            VerifyCallsFor<IConnection>();
        }
    }

    public interface IConnection
    {
        Stream GetStream();
        void Close();
    }

    public interface IStreamReceiver
    {
        void Receive(Stream stream);
    }

    public interface IStreamParser
    {
        void Parse();
    }

    public interface IHttpRequestPipeLine
    {
        void Execute(IHttpRequest request);
    }

    public interface IHttpRequest
    {
    }
}