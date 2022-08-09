using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessView.Business.Utils;
using ProcessView.Business.Exceptions;
using System.Diagnostics;

namespace ProcessView.Business
{
    internal class ProcessView
    {
        private static ProcessView? _instance = null;
        
        private static readonly object _instanceLock  = new object();
        ProcessView() { }
        public static  ProcessView GetInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if( _instance == null)
                    {
                        _instance = new ProcessView();
                    }
                    return _instance;
                }
                
            }

        }
        
        public void Kill(int id)
        {
            Process.GetProcessById(id).Kill();
            
        }
        public bool IsRunning(string name) => Process
            .GetProcesses()
            .ToList()
            .Exists(p => p.ProcessName.ToUpper().Contains(name.ToUpper()));

        public Process Start(string path) => Process.Start(path);
        
        public ProcessThreadCollection GetThreads(int processId)
        {

            ProcessThreadCollection threads = Process.GetProcessById(processId).Threads;

            if(threads.IsEmptyOrNull()) throw new NotFoundThreadException();

            return threads;
            
        }
        

        
    }
}
