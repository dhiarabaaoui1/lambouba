using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Ajouter une tâche au pool de threads
        ThreadPool.QueueUserWorkItem(TravailA);
        ThreadPool.QueueUserWorkItem(TravailB);

        Console.WriteLine("Tâches ajoutées au pool de threads.");
        Console.ReadLine(); // Attendre que l'utilisateur appuie sur une touche
    }

    // Méthode exécutée dans un thread du ThreadPool
    static void TravailA(object state)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Travail A - {i + 1}");
            Thread.Sleep(1000); // Simuler une tâche longue
        }
    }

    // Une autre méthode exécutée dans un thread du ThreadPool
    static void TravailB(object state)
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Travail B - {i + 1}");
            Thread.Sleep(1500); // Simuler une tâche longue
        }
    }
}

/*
 Quand Utiliser le ThreadPool ?
Tâches Courtes : Utilise le ThreadPool pour des tâches courtes qui ne nécessitent pas un thread dédié.
Réutilisation des Threads : Lorsque tu veux optimiser l'utilisation des threads en réutilisant les threads existants pour différentes tâches.
Facilité de Gestion : Si tu ne veux pas gérer manuellement la création et la destruction des threads.
 */