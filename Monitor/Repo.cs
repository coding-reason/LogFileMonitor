using LogFileMonitor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogFileMonitor.Monitor
{
    public sealed class Repo
    {
        public Repo()
        {
            this.lfiList = new List<LogFileInfo>();
            this.PopulateLogFiles();
            threadController = new MonitorThreadController(this);
            threadController.InitializeMonitors();
        }
        MonitorThreadController threadController;
        public List<LogFileInfo> lfiList { get; set; }

        public void PopulateLogFiles()
        {
            StreamReader sr = new StreamReader(@"config\LogFileNames.txt");
            while (true)
            {
                var read = sr.ReadLine();
                if (read == null)
                    break;
                var lfi = new LogFileInfo { fullName = read };
                lfiList.Add(lfi);

            }
            sr.Dispose();
        }
    }
}
