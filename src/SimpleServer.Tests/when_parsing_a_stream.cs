using System;
using System.IO;
using System.Text;
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

    public class StreamParser : IStreamParser
    {
        public string Parse(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, Convert.ToInt32(stream.Length));
            return new ASCIIEncoding().GetString(bytes);
        }
    }
}