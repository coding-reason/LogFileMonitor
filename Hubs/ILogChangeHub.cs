using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogFileMonitor.Hubs
{
    public interface ILogChangeHub
    {
        Task AddLines(int fileId, string[] lines);
    }
}
