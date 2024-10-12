using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ServerFindNumber
{

    static void Main(string[] args)
    {

        int port = 8881;
        int maxConnections = 20;
        if (args.Length >= 1)
        {
            port = int.TryParse(args[0], out int parsedResult) ? parsedResult : port;
        }

        if (args.Length >= 2)
        {
            maxConnections = int.TryParse(args[1], out int parsedResult) ? parsedResult : maxConnections;
        }


        Socket serverSocket;
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipep = new IPEndPoint(ipAddress, port);
        serverSocket.Bind(ipep);
        serverSocket.Listen(maxConnections);


        Console.WriteLine("Serveur en attente de connexions...");

        while (true)
        {
            Socket clientSocket = serverSocket.Accept();

            Console.WriteLine("un nouveau joueur cherche à se connecter");
            GameFindNumber cl = new GameFindNumber();
            Thread clientThread = new Thread(new ParameterizedThreadStart(cl.Game));
            clientThread.Start(clientSocket);
        }
    }

}

