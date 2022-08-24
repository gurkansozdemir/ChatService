using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class ServerSocket
    {
        private readonly Socket _listenerSocket;
        private readonly string _host;
        private readonly int _port;
        private readonly IPEndPoint _localEndPoint;

        public ServerSocket(string host, int port)
        {
            _host = host;
            _port = port;
            IPHostEntry ipHost = Dns.GetHostEntry(_host);
            _listenerSocket = new Socket(ipHost.AddressList[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _localEndPoint = new IPEndPoint(ipHost.AddressList[0], _port);
        }

        public void StartListening()
        {
            _listenerSocket.Bind(_localEndPoint);
            _listenerSocket.Listen(10);
            _listenerSocket.BeginAccept(AcceptCallback, _listenerSocket);
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            Socket acceptedSocket = _listenerSocket.EndAccept(ar);
            _listenerSocket.BeginAccept(AcceptCallback, _listenerSocket);
        }
    }
}
