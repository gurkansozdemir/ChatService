using Client;

public class Program
{
    public static void Main(String[] args)
    {
        ClientSocket clientSocket = new ClientSocket();
        Console.WriteLine("Suncu Bağlantısı Yapılıyor...");
        clientSocket.ConnectServer("127.0.0.1", 1234);
        Console.WriteLine("Sunucu Bağlantısı Hazır.");
        while (true)
        {
            Console.Write("Mesaj:");
            clientSocket.Send(Console.ReadLine());
        }
    }
}