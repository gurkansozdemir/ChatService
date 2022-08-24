using Server;

public class Program
{
    public static void Main(String[] args)
    {
        Console.WriteLine("Sunucu Oluşturuluyor...");
        ServerSocket serverSocket = new ServerSocket("127.0.0.1", 1234);
        Console.WriteLine("Sunucu Hazırlandı.");
        serverSocket.StartListening();
        Console.ReadLine();
    }
}