using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoxExtensions;
using LogFileMonitor;
namespace LogFileMonitor.Test
{
    public sealed class TestRepo
    {
        public TestRepo()
        {
            
        }

        public void start()
        {
            ThreadPool.QueueUserWorkItem(beginWriting, 1);
        }
        public void beginWriting(object o)
        {
            var st = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"{st}");
            var fn = Environment.CurrentDirectory + "\\Test\\testFile5.txt";
            File.Delete(fn);
            var x = File.Create(fn);
            x.Close();
            int xcnt = 0;
            while (true)
            {
                //lock (Program.fileLock)
                //{
                    using (var fs = new FileStream(fn, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                    {

                        int cnt = 0;
                        if (cnt == 0)
                            fs.SetLength(0);
                        int sleepX = 100;

                        fs.Write($"write {DateTime.Now} \r\n".ToBytes());
                        fs.Flush();

                        Thread.Sleep(sleepX);
                        cnt++;
                        if (cnt == 20)
                        {
                            sleepX = 2000;
                        }
                        Console.WriteLine($"writing test file {xcnt++}");
                    }
                //}

            }
            


        }
    }
}
