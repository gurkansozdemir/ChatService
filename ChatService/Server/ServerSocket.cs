using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class ServerSocket
    {
        private readonly Socket _listenerSocket;
        private Socket _acceptedSocket;
        private readonly string _host;
        private readonly int _port;
        private readonly IPEndPoint _localEndPoint;
        private byte[] _buffer;

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
            StartReceiving();
            _listenerSocket.BeginAccept(AcceptCallback, _listenerSocket);
        }

        public void StartReceiving()
        {
            _buffer = new byte[4];
            // Client'tan gelen datayı dinliyor data geldiğinde receiveCollback metodunu çalıştırıyor..
            _acceptedSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            _buffer = new byte[BitConverter.ToInt32(_buffer, 0)];
            _acceptedSocket.Receive(_buffer, _buffer.Length, SocketFlags.None);
            string data = Encoding.Default.GetString(_buffer);
            Console.Write($"İstemci Mesaji: {data}");
            Send(data);
            // Gelen datayı ekrana bastıktan sonra tekrar dinlemeye geçiyor..
            StartReceiving();
        }

        public void Send(string data)
        {
            var fullPacket = new List<byte>();
            fullPacket.AddRange(BitConverter.GetBytes(data.Length));
            fullPacket.AddRange(Encoding.Default.GetBytes(data));

            // Sunucuya gelen data tekrar client'a iletiliyor..
            _acceptedSocket.Send(fullPacket.ToArray());
        }
    }
}
