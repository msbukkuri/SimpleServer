using System;
using System.IO;
using FubuTestingSupport;
using NUnit.Framework;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_parsing_a_stream
    {
        [Test]
        public void should_read_stream()
        {
            var message = Guid.NewGuid().ToString();
            var stream = new MemoryStream();
            writeAndPrepare(stream, message);

            new StreamParser()
                .Parse(stream)
                .ShouldEqual(message);
        }

        private void writeAndPrepare(Stream stream, string message)
        {
            var writer = new StreamWriter(stream);
            writer.Write(message);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);
        }
    }
}