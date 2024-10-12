using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread thread10 = new Thread(FaireUnTravail1); // On crée un thread qui exécutera la méthode FaireUnTravail
        //System.Threading.Thread t = new System.Threading.Thread(MethodA)====  Thread t = new Thread(MethodA);
        thread10.Start(); // On démarre le thread

        // kima zeda tnejem tamel threads avec paramaetre : 
        Thread thread11 = new Thread(FaireUnTravail2);
        thread11.Start("jajjaajaj");

        // ici c'est a dire que la partie principal ne ce lance que quand le thread 10 finit sont travail
        thread10.Join();
        // Le thread principal continue à faire son propre travail
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Travail principal...");
            Thread.Sleep(1000); // Attendre 1 seconde
        }


    }

    static void FaireUnTravail1()
    {
        // Le code exécuté dans le thread séparé
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Travail dans le thread...");
            Thread.Sleep(1000); // Attendre 1 seconde
        }
    }
    static void FaireUnTravail2(object message)
    {
        // Le code exécuté dans le thread séparé
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(message);
            Thread.Sleep(1000); // Attendre 1 seconde
        }
    }
}
