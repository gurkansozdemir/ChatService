using System.Net.Sockets;
using System.Text;

namespace Server.Sockets
{
    internal class ReceiveSocket
    {
        private byte[] _buffer;
        private readonly Socket _receiveSocket;

        public ReceiveSocket(Socket receiveSocket)
        {
            _receiveSocket = receiveSocket;
        }
        public void StartReceiving()
        {
            _buffer = new byte[4];
            // Client'tan gelen datayı dinliyor data geldiğinde receiveCollback metodunu çalıştırıyor..
            _receiveSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                _buffer = new byte[BitConverter.ToInt32(_buffer, 0)];
                _receiveSocket.Receive(_buffer, _buffer.Length, SocketFlags.None);
                string data = Encoding.Default.GetString(_buffer);
                Console.Write($"İstemci Mesajı: {data} \n");
                // Gelen datayı server'a bağlanan tüm client'lara gönderiyor..
                Send(data);
                StartReceiving();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Send(string data)
        {
            var fullPacket = new List<byte>();
            fullPacket.AddRange(BitConverter.GetBytes(data.Length));
            fullPacket.AddRange(Encoding.Default.GetBytes(data));
            // Sunucuya gelen data tekrar client'a iletiliyor..
            foreach (var receiver in ClientController.Clients)
            {
                // Gelen datayı server'a bağlanan tüm client'lara gönderiyor..
                receiver.Send(fullPacket.ToArray());
            }
        }
    }
}
