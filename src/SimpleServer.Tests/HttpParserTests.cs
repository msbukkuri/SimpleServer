using System;
using FubuTestingSupport;
using NUnit.Framework;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_parsing_an_http_message
    {
        private static string _sampleMessage =
            @"GET http://www.jmarshall.com/easy/http/ HTTP/1.1
Accept: text/html, application/xhtml+xml, */*
Accept-Language: en-US
User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
Accept-Encoding: gzip, deflate
Connection: Keep-Alive
Host: www.jmarshall.com
";
        private IHttpRequest _request;

        [SetUp]
        public void before_each()
        {
            _request = new HttpMessageParser().Parse(_sampleMessage);
        }

        [Test]
        public void should_parse_http_method()
        {
            _request
                .HttpMethod
                .ShouldEqual("GET");
        }

        [Test]
        public void should_set_uri()
        {
            _request
                .Url
                .OriginalString
                .ShouldEqual("http://www.jmarshall.com/easy/http/");
        }

        [Test]
        public void should_set_content_type()
        {
            _request
                .ContentType
                .ShouldEqual("text/html");
        }
    }

    public class HttpMessageParser : IHttpMessageParser
    {
        public IHttpRequest Parse(string message)
        {
            return new HttpRequest() { HttpMethod = ParseHttpMethod(message), ContentType = ParseContentType(message), Url = ParseUri(message) };
        }

        private string ParseHttpMethod(string message)
        {
            return message.Split(' ')[0];
        }

        private Uri ParseUri(string message)
        {
            return new Uri(message.Split(' ')[1]);
        }

        private string ParseContentType(string message)
        {
            //TODO: Set ContentType
            return message;
        }
    }
}