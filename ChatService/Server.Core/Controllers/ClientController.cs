using System.Net.Sockets;

namespace Server.Core.Controllers
{
    public class ClientController
    {
        public static List<Socket> Clients = new List<Socket>();
        public static void AddClient(Socket socket)
        {
            Clients.Add(socket);
        }
    }
}
