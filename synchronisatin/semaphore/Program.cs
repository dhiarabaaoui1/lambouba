using System;
using System.Threading;

class Program
{
    // Crée un Semaphore qui permet à 3 threads maximum d'accéder simultanément à la section critique
    static Semaphore semaphore = new Semaphore(3, 3);

    static void Main()
    {
        // Créer et démarrer 10 threads
        for (int i = 1; i <= 10; i++)
        {
            Thread thread = new Thread(AccederAuService);
            thread.Start(i);
        }
    }

    static void AccederAuService(object idClient)
    {
        Console.WriteLine($"Client {idClient} attend pour accéder au service.");

        semaphore.WaitOne(); // Le client attend son tour pour entrer

        try
        {
            Console.WriteLine($"Client {idClient} est en train d'utiliser le service.");
            Thread.Sleep(2000); // Simule l'utilisation du service
        }
        finally
        {
            Console.WriteLine($"Client {idClient} quitte le service.");
            semaphore.Release(); // Libère le Semaphore pour qu'un autre client puisse entrer
        }
    }
    
}
/*
 5. Quand Utiliser Semaphore ?
Contrôle de Concurrence : Lorsque tu veux limiter le nombre de threads pouvant accéder à une ressource simultanément.
Synchronisation Inter-processus : Utilise Semaphore (et non SemaphoreSlim) lorsque tu as besoin de synchroniser des threads entre plusieurs processus.
Version allégée : Utilise SemaphoreSlim pour les scénarios où tu n'as besoin que de synchronisation intra-processus, pour des performances optimisées.
 */