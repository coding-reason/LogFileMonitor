using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoxExtensions;

namespace LogFileMonitor.Test
{
    public sealed class TestRepo
    {
        public TestRepo()
        {
            ThreadPool.QueueUserWorkItem(beginWriting, 1);
        }
        public void beginWriting(object o)
        {
            var fs = new FileStream(Environment.CurrentDirectory + "\\Test\\testFile.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            fs.SetLength(0);
            int cnt = 0;
            int sleepX = 100;
            while (true)
            {
                fs.Write($"write {DateTime.Now} \r\n".ToBytes());
                Thread.Sleep(sleepX);
                cnt++;
                if (cnt == 20)
                {
                    sleepX = 30000;
                }

            }
            


        }
    }
}
