using System;
using System.IO;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex(); // Crée un Mutex global

    static void Main()
    {
        for (int i = 0; i < 3; i++)
        {
            Thread thread = new Thread(EcrireDansFichier);
            thread.Start();
        }
    }

    static void EcrireDansFichier()
    {
        mutex.WaitOne(); // Verrouille le Mutex

        try
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} écrit dans le fichier à {DateTime.Now}");
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} écrit dans le fichier.");
            }
        }
        finally
        {
            mutex.ReleaseMutex(); // Libère le Mutex
        }
    }
}
/*
 
Différences entre Mutex et lock/Monitor
--Portée :

Mutex peut être utilisé pour synchroniser des threads entre différents processus.
lock et Monitor ne fonctionnent que pour synchroniser des threads au sein d'un même processus.

--Performance :

Mutex est plus lourd que lock et Monitor en termes de performance car il nécessite des appels système qui sont plus coûteux.
lock et Monitor sont plus rapides et mieux adaptés pour la synchronisation au sein d'un même processus.

--Interopérabilité :

Mutex peut être partagé entre plusieurs processus, ce qui le rend idéal pour des scénarios où des processus indépendants doivent synchroniser leurs actions.

5. Quand Utiliser Mutex ?
Synchronisation inter-processus : Lorsque tu as besoin de synchroniser des ressources partagées entre différents processus.
Exclusion Mutuelle : Lorsque tu as besoin de garantir qu'une seule instance d'une opération est en cours, même si plusieurs processus tentent de l'exécuter.
 */