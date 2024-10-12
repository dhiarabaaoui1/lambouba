using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

class GuessingGameClient
{
    static void Main()
    {
        try
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8881));

            ThreadPool.QueueUserWorkItem(state => ReceiveMessages(clientSocket));

            while (true)
            {
                string guess = Console.ReadLine();
                byte[] guessBytes = Encoding.ASCII.GetBytes(guess);
                clientSocket.Send(guessBytes);
            }
            clientSocket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la connexion au serveur : {ex.Message}");
        }
    }
    static void ReceiveMessages(Socket clientSocket)
    {
        int nbMessage = 0;
        while (true)
        {
            string message = ReceiveData(clientSocket);

            if (!string.IsNullOrEmpty(message))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"({nbMessage++})-> {message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Thread.Sleep(100);
        }
    }
    static string ReceiveData(Socket socket)
    {
        byte[] buffer = new byte[1024];
        int bytesRead = socket.Receive(buffer);
        return Encoding.ASCII.GetString(buffer, 0, bytesRead);
    }
}
