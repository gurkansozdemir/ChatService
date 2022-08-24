using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class ClientSocket
    {
        private Socket _socket;
        public void ConnectServer(string host, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (!_socket.Connected)
            {
                // Bağlanılmak istenilen sunucu ip ve port bilgisini alır ve sunucu ile bağlantı kurulur..
                _socket.Connect(new IPEndPoint(IPAddress.Parse(host), port));
            }
        }

        public void Send(string data)
        {
            var fullPacket = new List<byte>();
            fullPacket.AddRange(BitConverter.GetBytes(data.Length));
            fullPacket.AddRange(Encoding.Default.GetBytes(data));

            // Sunucuya bağlanan client soket'inden sunuya bir mesaj gönderiliyor..
            _socket.Send(fullPacket.ToArray());
        }
    }
}
