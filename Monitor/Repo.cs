using LogFileMonitor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace LogFileMonitor.Monitor
{
    public sealed class Repo
    {
        public Repo()
        {
            lfiList = new List<LogFileInfo>();
        

        }

        
        public void start()
        {
            Thread newThread = new Thread(LogFileMonitor.Monitor.Repo.PopulateLogFiles);
            newThread.Start(42);
            threadController = new MonitorThreadController(this);
            threadController.InitializeMonitors(lfiList);
        }
        MonitorThreadController threadController;
        public static List<LogFileInfo> lfiList { get; set; }

        public static void PopulateLogFiles(Object a)
        {
            var st = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Repo {st}");
            Thread.Sleep(800000);
        
            lock (Program.fileLock)
            {
                StreamReader sr = new StreamReader(@"config\LogFileNames.txt");
                while (true)
                {
                    var read = sr.ReadLine();
                    if (read == null)
                        break;
                    if (read[0] == '.')
                        read = Environment.CurrentDirectory + read.Substring(1);

                    var lfi = new LogFileInfo { fullName = read };
                    lfiList.Add(lfi);

                }
                sr.Dispose();
            }
        }
    }
}
