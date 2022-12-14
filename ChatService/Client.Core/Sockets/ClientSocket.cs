using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Core.Sockets
{
    public class ClientSocket
    {
        private Socket _socket;
        private byte[] _buffer;
        public bool ConnectServer(string host, int port)
        {
            bool response = false;
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                while (!_socket.Connected)
                {
                    // Bağlanılmak istenilen sunucu ip ve port bilgisini alır ve sunucu ile bağlantı kurulur..
                    _socket.Connect(new IPEndPoint(IPAddress.Parse(host), port));
                }
                StartReceiving();
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }

        public void Send(string data)
        {
            var fullPacket = new List<byte>();
            fullPacket.AddRange(BitConverter.GetBytes(data.Length));
            fullPacket.AddRange(Encoding.Default.GetBytes(data));
            // Sunucuya bağlanan client soket'inden sunuya bir mesaj gönderiliyor..
            _socket.Send(fullPacket.ToArray());
        }

        public void StartReceiving()
        {
            _buffer = new byte[4];
            // Server'dan gelen datayı dinliyor data geldiğinde receiveCollback metodunu çalıştırıyor..
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                _buffer = new byte[BitConverter.ToInt32(_buffer, 0)];
                _socket.Receive(_buffer, _buffer.Length, SocketFlags.None);
                string data = Encoding.Default.GetString(_buffer);
                Console.WriteLine($"\t\t\t {data}");
                // Gelen datayı ekrana bastıktan sonra tekrar dinlemeye geçiyor..
                StartReceiving();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Disconnect()
        {
            _socket.DisconnectAsync(true);
        }
    }
}
