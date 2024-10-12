using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        // Exercice 1 : Lancer explorer.exe
        LancerEtSurveillerProcessus("explorer.exe", @"C:\Windows");

        // Exercice 1 : Lancer notepad.exe
        string cheminFichier = @"C:\Users\dhiar\OneDrive\Bureau\Day.txt";
        LancerEtSurveillerProcessus("notepad.exe", cheminFichier);

        // Exercice 4 : Utilisation des verbes du Shell pour explorer un dossier et éditer un fichier
        UtiliserVerbeShell("explore", @"C:\Windows");
        UtiliserVerbeShell("edit", cheminFichier);
    }

    // Cette méthode lance un processus et surveille sa fin avec un gestionnaire d'événements
    static void LancerEtSurveillerProcessus(string nomProcessus, string arguments)
    {
        Process process = new Process();
        process.StartInfo.FileName = nomProcessus;
        process.StartInfo.Arguments = arguments;

        // Activer la détection des événements pour le processus
        process.EnableRaisingEvents = true;

        // Attacher un gestionnaire d'événements pour réagir lorsque le processus se termine
        process.Exited += new EventHandler(Process_Exited);

        // Démarrer le processus
        process.Start();
        Console.WriteLine($"Processus {nomProcessus} lancé avec l'argument {arguments}.");

        // Vérifier si le processus est encore actif avant de quitter
        if (!process.HasExited)
        {
            Console.WriteLine($"Processus {nomProcessus} n° {process.Id} est encore actif.");
        }

        // Attendre la fin du processus
        process.WaitForExit();
    }

    // Gestionnaire d'événements appelé lorsque le processus se termine
    static void Process_Exited(object sender, EventArgs e)
    {
        Process process = (Process)sender;
        Console.WriteLine($"Processus {process.StartInfo.FileName} n° {process.Id} est terminé.");
    }

    // Cette méthode utilise les verbes du Shell pour exécuter des actions spécifiques sur des fichiers ou répertoires
    static void UtiliserVerbeShell(string verbe, string chemin)
    {
        Process process = new Process();
        process.StartInfo.FileName = chemin;
        process.StartInfo.Verb = verbe;
        process.StartInfo.UseShellExecute = true;
        process.Start();

        Console.WriteLine($"Processus avec le verbe '{verbe}' pour le chemin {chemin} est lancé.");
    }

   
}
