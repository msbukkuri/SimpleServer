using System;

namespace SimpleServer
{
    public interface IHttpListener
    {
        void Listen(int port);
        void Listen(int port, Action continuation);
        void Stop();
    }
}