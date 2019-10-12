using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LogFileMonitor.Interfaces;

namespace LogFileMonitor.Hubs
{
    public class LogChangeHub : Hub<ILogChangeHub>
    {
        
      
        public Task AddLines(int fileId, string[] lines)
        {
            
            return Clients.All.AddLines(fileId, lines);
        }
     
    }
}
