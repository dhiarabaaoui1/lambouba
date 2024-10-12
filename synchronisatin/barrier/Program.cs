/*
 1. Principe de Barrier
Imagine que tu organises une course entre trois amis, et vous devez tous faire un arrêt à un point donné avant de continuer vers la ligne d'arrivée. Une fois que chacun de vous a atteint cet arrêt, vous repartez ensemble pour la suite du parcours. C'est exactement ce que fait une Barrier en programmation multithread.

Barrier est un mécanisme de synchronisation qui force un groupe de threads à atteindre un point spécifique de leur exécution (appelé "barrière") avant de continuer. Tous les threads doivent attendre que les autres threads du groupe aient atteint cette même barrière.

2. Comment Fonctionne une Barrier ?
Création :

Une Barrier est créée avec un nombre de "participants" qui correspond au nombre de threads qui doivent attendre à cette barrière avant de continuer.
Attente des Threads :

Lorsque chaque thread atteint la barrière, il appelle la méthode SignalAndWait(). Cette méthode indique que le thread a atteint la barrière et qu'il attend que tous les autres threads participants l'atteignent également.
Continuation :

Une fois que tous les threads ont appelé SignalAndWait(), ils peuvent tous continuer leur exécution au-delà de la barrière.

 */
using System;
using System.Threading;

class Program
{
    // On crée une `Barrier` pour synchroniser trois threads
    static Barrier barrier = new Barrier(3);

    static void Main()
    {
        // Créer et démarrer 3 threads
        for (int i = 1; i <= 3; i++)
        {
            Thread thread = new Thread(Travail);
            thread.Start(i);
        }
    }

    static void Travail(object idThread)
    {
        // Phase 1 : Chaque thread effectue sa première tâche
        Console.WriteLine($"Thread {idThread} commence la phase 1.");
        Thread.Sleep(1000 * (int)idThread); // Simule le travail de la phase 1
        Console.WriteLine($"Thread {idThread} atteint la barrière après la phase 1.");

        // Attendre que tous les threads aient terminé la phase 1
        barrier.SignalAndWait();

        // Phase 2 : Après la barrière, chaque thread effectue la deuxième tâche
        Console.WriteLine($"Thread {idThread} commence la phase 2.");
        Thread.Sleep(1000 * (int)idThread); // Simule le travail de la phase 2
        Console.WriteLine($"Thread {idThread} termine la phase 2.");
    }
}

/*
 4. Quand Utiliser une Barrier ?
Coordination de Phases :
Utilise une Barrier lorsque tu as plusieurs threads qui doivent être synchronisés à différents points de leur exécution.
Attente de Tous les Participants :
Dans des scénarios où tu veux t'assurer que tous les threads ont terminé une étape spécifique avant de continuer à la suivante.
Résumé de Barrier :
Synchronisation Multi-threads :

Barrier permet de synchroniser plusieurs threads en s'assurant qu'ils atteignent tous un point d'attente (la barrière) avant de continuer ensemble.
Utilisation :

Pratique pour des tâches en plusieurs phases, où chaque phase doit être synchronisée entre tous les threads.
Exemple Visuel :

Imagine une course où tous les coureurs doivent atteindre un checkpoint avant de pouvoir continuer ensemble à la prochaine étape. C'est exactement ce que fait une Barrier.
 */
