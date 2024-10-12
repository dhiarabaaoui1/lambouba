using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Client
{
    // Cette méthode établit une connexion avec le serveur.
    private static Socket SeConnecter()
    {
        // Crée un nouveau socket TCP/IP pour le client, utilisant IPv4 (AddressFamily.InterNetwork), flux de données (SocketType.Stream), et TCP (ProtocolType.Tcp).
        var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Définition de l'adresse IP et du port du serveur auquel le client se connectera.
        var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);

        // Établit la connexion avec le serveur en utilisant l'adresse IP et le port définis.
        client.Connect(serverEndPoint);

        Console.WriteLine("Connecté au serveur.");

        // Retourne le socket du client, maintenant connecté au serveur.
        return client;
    }

    // Cette méthode gère la communication avec le serveur (envoi et réception de messages).
    private static void EcouterEtEnvoyer(Socket client)
    {
        // Crée un thread séparé pour écouter les messages envoyés par le serveur en continu sans bloquer l'envoi de messages par le client.
        Thread thread = new Thread(() =>
        {
            // Boucle infinie pour écouter en permanence les messages du serveur.
            while (true)
            {
                // Crée un buffer (tableau d'octets) pour recevoir les données envoyées par le serveur.
                var buffer = new byte[1024];

                // Reçoit les données du serveur. `client.Receive(buffer)` renvoie le nombre d'octets réellement reçus.
                int receivedBytes = client.Receive(buffer);

                // Convertit le tableau d'octets reçu en chaîne de caractères (UTF-8) et l'affiche.
                var data = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                // Affiche le message reçu du serveur dans la console.
                Console.WriteLine($"Serveur: {data}");
            }
        });

        // Démarre le thread pour écouter les messages entrants du serveur.
        thread.Start();

        // Boucle infinie pour envoyer des messages au serveur.
        while (true)
        {
            // Lit un message depuis la console (entrée utilisateur).
            var message = Console.ReadLine();

            // Convertit le message de la console (chaîne de caractères) en tableau d'octets (ASCII).
            var data = Encoding.ASCII.GetBytes(message);

            // Envoie le tableau d'octets (message) au serveur via le socket connecté.
            client.Send(data);
        }
    }

    // Cette méthode ferme proprement la connexion avec le serveur.
    private static void Deconnecter(Socket socket)
    {
        // Effectue une fermeture propre de la connexion dans les deux directions (envoi et réception).
        socket.Shutdown(SocketShutdown.Both);

        // Ferme définitivement le socket pour libérer les ressources réseau.
        socket.Close();
    }

    // Point d'entrée principal du programme client.
    static void Main(string[] args)
    {
        // Établit la connexion avec le serveur.
        var clientSocket = SeConnecter();

        // Gère l'échange de messages avec le serveur (écoute et envoi).
        EcouterEtEnvoyer(clientSocket);

        // Note : Il n'y a pas de déconnexion prévue ici pour maintenir la communication active.
    }
}
