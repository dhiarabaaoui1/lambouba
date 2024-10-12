using System;
using System.IO;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex(); // Crée un Mutex global
    private static int var = 0;

    static void Main()
    {
        for (int i = 0; i < 2; i++)
        {
            Thread thread = new Thread(()=> EcrireDansFichier("T"+ (i + 1)));
            thread.Start();

        }
        for (int i = 0; i < 2; i++)
        {
            int localI = i;  // Capture la valeur de i dans une variable locale
            Thread thread = new Thread(() => EcrireDansFichier("T" + (localI + 1)));
            thread.Start();
        }

    }

    static void EcrireDansFichier(string name_thread )
    {
        mutex.WaitOne(); // Verrouille le Mutex
        try
        {
            for(int i = 0; i < 3; i++)
                {
                var++;
                Console.WriteLine($"Thread -> {name_thread} -- var -> {var}");
                Thread.Sleep(2000); // Simule un travail


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