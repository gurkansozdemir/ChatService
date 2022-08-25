using Client.Core.Sockets;
using Xunit;

namespace ChatService.Test
{
    public class ClientTest
    {
        [Fact]
        public void ConnectionTest()
        {
            ClientSocket clientSocket = new ClientSocket();
            bool result = clientSocket.ConnectServer("127.0.0.1", 1234);
            Assert.Equal(true, result);
        }
    }
}