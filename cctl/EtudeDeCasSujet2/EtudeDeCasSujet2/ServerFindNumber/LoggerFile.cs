using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public sealed class LoggerFile
{
    private static LoggerFile instance = null;
    private static readonly Semaphore semaphore = new Semaphore(1, 1);
    private string logFilePath;


    private LoggerFile()
    {
        logFilePath = "..\\..\\..\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
    }

    public static LoggerFile Instance
    {
        get
        {

            if (instance == null)
            {
                semaphore.WaitOne();
                {
                    if (instance == null)
                    {
                        instance = new LoggerFile();
                    }
                }
                semaphore.Release();
            }
            return instance;
        }
    }

    // Méthode pour écrire un message dans le fichier log
    public void Log(string message)
    {
        try
        {

            semaphore.WaitOne();
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} - {message}");
                }
            }
            semaphore.Release();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'écriture dans le fichier log : {ex.Message}");
        }
    }
}

