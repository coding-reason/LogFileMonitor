﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using LogFileMonitor.Interfaces;
using LogFileMonitor.Models;
using System.Linq;

namespace LogFileMonitor.Monitor
{
    public class MonitorThreadController
    {
        //public static event EventHandler<FileChanged> fileChanged;

        public MonitorThreadController(Repo repo)
        {
            this.repo = repo;
            notify = new INotifySurface();
        }
        private INotifySurface notify;
        private Repo repo;

        public bool RetainThreading { get; set; }
        public void InitializeMonitors()
        {
            RetainThreading = true;
            int i = 0;
            foreach (var l in repo.lfiList)
            {
                Console.WriteLine("starting monitor thread");
                ThreadPool.QueueUserWorkItem(StartMonitor, l.index);
                i++;
            }
        }

        public void StartMonitor(object state)
        {
            object array = state as object;
            int fileId = Convert.ToInt32(state);
            var lfi = repo.lfiList.First(a => a.index == fileId);
            string logFileName = lfi.fullName;
            long fileLength = lfi.length;

            FileStream fs;


            Console.WriteLine("Monitor started:");
            while (RetainThreading)
            {
                fs = null;
                lock (Program.fileLock)
                {
                    using (fs = new FileStream(logFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        fs.Seek(fileLength, 0);
                        if (fileLength != fs.Length)
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                bool hasData = true;
                                var lines = new List<string>();
                                while (hasData)
                                {
                                    var line = sr.ReadLine();
                                    if (line == null)
                                    {
                                        hasData = false;
                                        break;
                                    }
                                    else
                                    {
                                        lines.Add(line);
                                    }
                                }
                                Console.WriteLine("notifying change:");
                                notify.Notify(fileId, lines.ToArray());
                                fileLength = fs.Length;
                            }
                        }
                        Thread.Sleep(200);
                    }
                }
            }
        }
    }
}


