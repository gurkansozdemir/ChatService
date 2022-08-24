using System.Net;
using System.Net.Sockets;

namespace Server.Sockets
{
    internal class ServerSocket
    {
        private readonly Socket _listenerSocket;
        private Socket _acceptedSocket;
        private ReceiveSocket _receiveSocket;
        private readonly string _host;
        private readonly int _port;
        private readonly IPEndPoint _localEndPoint;
        public static List<Socket> clients = new List<Socket>();

        public ServerSocket(string host, int port)
        {
            _host = host;
            _port = port;
            IPHostEntry ipHost = Dns.GetHostEntry(_host);
            // Belirtilen ip ve port üzerinde bir soket oluşturuluyor..
            _listenerSocket = new Socket(ipHost.AddressList[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _localEndPoint = new IPEndPoint(ipHost.AddressList[0], _port);
        }

        public void StartListening()
        {
            _listenerSocket.Bind(_localEndPoint);
            _listenerSocket.Listen(10);
            // Server soket üzerine gelen bağlantı taleplerini dinliyor..
            _listenerSocket.BeginAccept(AcceptCallback, _listenerSocket);
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Server'a bağlanan client'lar yakalanır..
            _acceptedSocket = _listenerSocket.EndAccept(ar);
            Console.WriteLine("Bir İstemci Bağlandı");
            ClientController.AddClient(_acceptedSocket);
            _receiveSocket = new ReceiveSocket(_acceptedSocket);
            _receiveSocket.StartReceiving();
            _listenerSocket.BeginAccept(AcceptCallback, _listenerSocket);
        }
    }
}
