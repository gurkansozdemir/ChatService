using Client.Core.Sockets;

public class Program
{
    private static TimeOnly _lastSendMessageTime = TimeOnly.MinValue;
    private static bool isWarned = false;
    public static void Main(String[] args)
    {
        ClientSocket clientSocket = new ClientSocket();
        Console.WriteLine("Suncu Bağlantısı Yapılıyor...");
        clientSocket.ConnectServer("127.0.0.1", 1234);
        Console.WriteLine("Sunucu Bağlantısı Hazır.");
        while (true)
        {
            string message = Console.ReadLine();
            TimeSpan duration = TimeOnly.FromDateTime(DateTime.Now) - _lastSendMessageTime;
            if (duration.Seconds < 1)
            {
                if (!isWarned)
                {
                    Console.WriteLine("Lütfen mesajlar arasında bir saniye bekleyin!");
                    isWarned = true;
                }
                else
                {
                    clientSocket.Disconnect();
                    Console.WriteLine("Sunucu ile bağlantı kesildi.");
                    break;
                }
            }
            else
            {
                clientSocket.Send(message);
                _lastSendMessageTime = TimeOnly.FromDateTime(DateTime.Now);
            }
        }
        Console.ReadKey();
    }
}