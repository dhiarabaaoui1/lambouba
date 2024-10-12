using System;
using System.Threading;
/// <summary>
/// ///////////////////////////// monitor howa lock mais avec plus de fonctionaliés 
/// </summary>
class CompteBancaire
{
    private object verrou = new object();
    private int solde = 0;

    public void Deposer(int montant)
    {
        Monitor.Enter(verrou); // Verrouille l'accès à la section critique
        try
        {
            solde += montant;
            Console.WriteLine($"Déposé {montant}, nouveau solde: {solde}");
        }
        finally
        {
            Monitor.Exit(verrou); // Libère le verrou
        }
    }

    public void Retirer(int montant)
    {
        Monitor.Enter(verrou); // Verrouille l'accès à la section critique
        try
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
        }
        finally
        {
            Monitor.Exit(verrou); // Libère le verrou
        }
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
 3. Avantages de Monitor par Rapport à lock
Contrôle Manuel : Monitor te permet de contrôler manuellement le verrouillage et le déverrouillage, ce qui est utile dans des scénarios complexes où tu as besoin de plus de flexibilité.
Attente avec Timeout : Avec Monitor, tu peux spécifier un délai d'attente pour essayer de verrouiller un objet, ce qui n'est pas possible avec lock.
Tentative de Verrouillage : Monitor.TryEnter te permet de tenter de verrouiller un objet sans bloquer si le verrou n'est pas immédiatement disponible.
 
4. Scénarios d'utilisation

Utilise lock :

Lorsque tu as besoin d'une synchronisation simple et sûre.
Quand tu veux éviter les erreurs de déverrouillage manuel (comme oublier d'appeler Monitor.Exit).
Pour la majorité des cas où une simple exclusion mutuelle est suffisante.

Utilise Monitor :

Lorsque tu as besoin de fonctionnalités supplémentaires, comme la possibilité d'essayer d'obtenir un verrou sans bloquer, ou de spécifier un délai d'attente.
Dans des scénarios complexes où tu as besoin d'un contrôle plus fin sur la synchronisation.
Quand tu veux gérer le verrouillage et le déverrouillage manuellement en dehors d'un bloc structuré.
 */