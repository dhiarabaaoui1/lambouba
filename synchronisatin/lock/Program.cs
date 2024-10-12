using System;
using System.Threading;

class CompteBancaire
{
    private object verrou = new object(); // Objet utilisé pour le verrouillage
    private int solde = 0;

    public void Deposer(int montant)
    {

        lock (verrou) // Verrouille la section critique : blougha okhra andek clé , eli yodkhel lel section hethi ysaker mefteh bel clé 
        {
           // yekhdem  khedmtou 
            solde += montant;
            Console.WriteLine($"Déposé {montant}, nouveau solde: {solde}");
        } // ki ykamel khedmtou yhel beb " devrouille "
    }

    public void Retirer(int montant)
    {
        lock (verrou) // Verrouille la section critique
        {
            if (solde >= montant)
            {
                solde -= montant;
                Console.WriteLine($"Retiré {montant}, nouveau solde: {solde}");
            }
            else
            {
                Console.WriteLine("Solde insuffisant pour retirer");
            }
        } // Déverrouille automatiquement à la fin du bloc
    }
}

class Program
{
    static void Main()
    {
        CompteBancaire compte = new CompteBancaire();

        // Crée et lance plusieurs threads qui accèdent au compte bancaire en même temps
        Thread t1 = new Thread(() => compte.Deposer(100));
        Thread t2 = new Thread(() => compte.Retirer(50));
        Thread t3 = new Thread(() => compte.Deposer(200));

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();
    }
}
/*
 . Quand Utiliser lock ?
Accès aux ressources partagées : Lorsque plusieurs threads doivent accéder ou modifier une ressource partagée, comme une variable globale, un fichier, ou une base de données.
Section critique de code : Lorsque tu as une section de code qui ne doit être exécutée que par un seul thread à la fois.
Résumé sur lock :
Simple à utiliser : lock est une façon simple de s'assurer que seul un thread à la fois peut accéder à une ressource partagée.
Protège les sections critiques : lock empêche les autres threads d'interférer avec une section de code critique.
Automatique : lock gère automatiquement le verrouillage et le déverrouillage à la fin du bloc.
 */