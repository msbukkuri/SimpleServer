using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleServer
{
    public class HttpListener : IHttpListener
    {
        private readonly IConnectionHandler _handler;
        private Socket _listener;
        private Thread _listenerThread;
        private readonly ManualResetEvent _allDone = new ManualResetEvent(false);
        private readonly object _lock = new object();
        private bool _continue = true;

        public HttpListener(IConnectionHandler handler)
        {
            _handler = handler;
        }

        public void Listen(int port)
        {
            Listen(port, () => { });
        }

        public void Listen(int port, Action continuation)
        {
            var ipLocal = new IPEndPoint(IPAddress.Any, port);

            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _listener.Bind(ipLocal);
                _listener.Listen(100);
                continuation();

                while (shouldContinue())
                {
                    _allDone.Reset();
                    _listener.BeginAccept(AcceptCallback, _listener);
                    _allDone.WaitOne();
                }
            }
            catch(Exception e)
            {
                throw new HttpListenerException(404, e.Message);
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                _continue = false;
                _allDone.Set();
            }
        }

        private bool shouldContinue()
        {
            lock(_lock)
            {
                return _continue;
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            _allDone.Set();

            // Get the socket that handles the client request.
            var listener = (Socket)ar.AsyncState;
            var handler = listener.EndAccept(ar);

            // Create the state object.
            var state = new StateObject {WorkSocket = handler};
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            var socket = ((StateObject)ar.AsyncState).WorkSocket;
            _handler.Handle(new Connection(socket));
            socket.Disconnect(true);
        }

    }

    public class StateObject
    {
        // Client  socket.
        public Socket WorkSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
}
