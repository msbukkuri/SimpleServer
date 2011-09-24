using System;
using System.IO;
using System.Text;

namespace SimpleServer
{
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