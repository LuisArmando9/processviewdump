using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessView.Business.Utils;
using ProcessView.Business.Exceptions;
using System.Diagnostics;
using ProcessView.Persistence.WinApi.Structures;
using ProcessView.Business.Interfaces;

namespace ProcessView.Business
{
    internal class ProcessView
    {
       
        private readonly MemoryProcess memoryProcess;
       

        ProcessView(MemoryProcess memoryProcessParam) 
        {
            memoryProcess = memoryProcessParam;

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

        public List<IProcessDumpMemory?> ReadAllVirtualMemory(int processId) => memoryProcess.GetDump(processId);
        

        
    }
}
