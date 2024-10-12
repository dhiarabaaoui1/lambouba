using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    // Cette méthode initialise un socket serveur, le lie à une adresse IP et un port, puis le met en mode écoute.
    private static Socket SeConnecter()
    {
        // Initialise un nouveau socket en utilisant le protocole TCP/IP (Stream).
        var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Définition de l'adresse IP et du port sur lesquels le serveur va écouter les connexions.
        // IPAddress.Any permet d'écouter sur toutes les interfaces réseau disponibles.
        var localEndPoint = new IPEndPoint(IPAddress.Any, 11000);

        // Associe le socket à l'adresse IP et au port définis dans `localEndPoint`.
        server.Bind(localEndPoint);

        // Met le socket en mode écoute pour accepter les connexions entrantes.
        // `Listen(10)` définit un backlog de 10, c'est-à-dire que 10 connexions peuvent être en attente.
        server.Listen(10);

        Console.WriteLine("Serveur démarré et en attente de connexions...");

        // Retourne l'objet socket serveur configuré.
        return server;
    }

    // Cette méthode accepte une connexion entrante et retourne le socket client connecté.
    private static Socket AccepterConnexion(Socket socket)
    {
        // Attend et accepte une nouvelle connexion client, créant un nouveau socket pour cette connexion spécifique.
        var client = socket.Accept();

        // Récupère les informations de connexion du client, telles que son adresse IP et son port.
        var clientEndPoint = (IPEndPoint)client.RemoteEndPoint;

        Console.WriteLine($"Connexion établie avec {clientEndPoint.Address}:{clientEndPoint.Port}");

        // Retourne le socket représentant la connexion avec le client.
        return client;
    }

    // Cette méthode gère l'échange de messages entre le serveur et le client.
    private static void EcouterEtEnvoyer(Socket client)
    {
        // Crée un nouveau thread pour écouter les messages entrants du client en parallèle, sans bloquer l'envoi de messages.
        Thread thread = new Thread(() =>
        {
            // Boucle infinie pour écouter en continu les messages du client.
            while (true)
            {
                // Crée un buffer pour stocker les données reçues.
                var buffer = new byte[1024];

                // Reçoit les données du client et renvoie le nombre d'octets reçus.
                int receivedBytes = client.Receive(buffer);

                // Convertit les données reçues (tableau d'octets) en chaîne de caractères.
                var data = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                // Affiche le message reçu du client dans la console du serveur.
                Console.WriteLine($"Client: {data}");
            }
        });

        // Démarre le thread pour que l'écoute des messages s'exécute en parallèle.
        thread.Start();

        // Boucle principale pour envoyer des messages au client.
        while (true)
        {
            // Attend que l'utilisateur entre un message dans la console du serveur.
            var message = Console.ReadLine();

            // Convertit le message en tableau d'octets pour l'envoyer au client.
            var data = Encoding.ASCII.GetBytes(message);

            // Envoie les données (message) au client.
            client.Send(data);
        }
    }

    // Cette méthode ferme la connexion avec le client.
    private static void Deconnecter(Socket socket)
    {
        // Ferme le socket, libérant ainsi les ressources réseau utilisées par la connexion.
        socket.Close();
    }

    // Point d'entrée principal du programme serveur.
    static void Main(string[] args)
    {
        // Démarre le serveur, le lie à l'adresse IP et au port, et le met en mode écoute pour les connexions.
        var serverSocket = SeConnecter();

        // Accepte une connexion entrante avec un client.
        var clientSocket = AccepterConnexion(serverSocket);

        // Lance la gestion de la communication (envoi et réception de messages) avec le client.
        EcouterEtEnvoyer(clientSocket);

        // Note : Le serveur continue d'échanger des messages avec le client indéfiniment.
        // Il n'y a pas de déconnexion prévue dans ce scénario, mais cela pourrait être ajouté.
    }
}
