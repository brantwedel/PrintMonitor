using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var jobs = new Dictionary<int, System.Printing.PrintJobStatus>();
            while (true)
            {
                System.Threading.Thread.Sleep(100);
                System.Printing.PrintServer ps = new System.Printing.PrintServer(System.Printing.PrintSystemDesiredAccess.EnumerateServer);
                foreach (var pq in ps.GetPrintQueues())
                {
                    if (pq == null) continue;
                    try
                    {
                        pq.Refresh();
                        var jc = pq.GetPrintJobInfoCollection();
                        foreach (var job in jc)
                        {
                            if (!jobs.ContainsKey(job.JobIdentifier))
                            {
                                jobs[job.JobIdentifier] = job.JobStatus;
                                Console.WriteLine(pq.FullName);
                                Console.WriteLine("Printing: " + job.JobName +" [" + job.JobIdentifier + "]");
                                Console.Beep(2500, 1000);
                                Console.Beep(2500, 1000);
                            }
                        }

                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine("Printing: Unknown; [" +
                        e.ParamName + "] " + e.Message
);
                    }
                }
            }
        }
    }
}
