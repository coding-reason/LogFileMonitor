using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogFileMonitor.Hubs;
using LogFileMonitor.Interfaces;

namespace LogFileMonitor.Monitor
{
    public class NotifySurface : INotifySurface
    {
        public void Notify(int fileId, string[] data)
        {
            var lch = new LogChangeHub();
            lch.AddLines(fileId, data);
        }
    }
}
