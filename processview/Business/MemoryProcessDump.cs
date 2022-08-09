using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProcessView.Business.Interfaces;
using ProcessView.Persistence.WinApi;
using ProcessView.Persistence.WinApi.Enums;
using ProcessView.Persistence.WinApi.Structures;
using ProcessView.Business.Utils;
using ProcessView.Business.Models;
namespace ProcessView.Business
{
    internal class MemoryProcessDump
    {
        private readonly SYSTEM_INFO systemInfo; 
        
        
        MemoryProcessDump(SYSTEM_INFO systemInfoParam) 
        { 
            systemInfo = systemInfoParam; 
        }
        public IntPtr getProcessHandle(int processId) => Functions.OpenProcess((int)ProcessQueryInformation.PROCESS_DUMP, false, processId); 
        
        public List<IProcessDumpMemory> getDump(int processId)
        {
            IntPtr processHandle = getProcessHandle(processId);
            MEMORY_BASIC_INFORMATION64 memory = new();
            int bytesRead = 0;
            long minAddress = (long)systemInfo.lpMinimumApplicationAddress;
            long maxAddress = (long)systemInfo.lpMaximumApplicationAddress;
            IntPtr ptrMinAddress = systemInfo.lpMinimumApplicationAddress;
            List<IProcessDumpMemory> memoryList = new ();

            while(minAddress < maxAddress)
            {
                Functions.VirtualQueryEx(processHandle, ptrMinAddress, out memory, Marshal.SizeOf(memory));
                if (memory.IsValidPageForReading())
                {
                    byte[] buffer = new byte[memory.RegionSize];
                    Functions.ReadProcessMemory(processHandle, memory.BaseAddress, buffer, memory.RegionSize, out bytesRead);
                    memoryList.Add(new ProcessDumpMemoryModel()
                    {
                        memoryContent = buffer,
                        StartAddress = memory.BaseAddress,
                        EndAddress = memory.BaseAddress,
                    });
                    
                }
                minAddress += (long)memory.RegionSize;
                ptrMinAddress = new IntPtr(minAddress);



            }
           
            return memoryList;
        }
         



       
    }
}
