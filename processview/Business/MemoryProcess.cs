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
    internal class MemoryProcess
    {
        private readonly SYSTEM_INFO systemInfo;
        private MEMORY_BASIC_INFORMATION64 memoryBasicInformation;
        private readonly int memoryBasicInformationSize;
        MemoryProcess(SYSTEM_INFO systemInfoParam, MEMORY_BASIC_INFORMATION64 memoryBasicInformationParam)
        {
            systemInfo = systemInfoParam;
            memoryBasicInformation = memoryBasicInformationParam;
            memoryBasicInformationSize = Marshal.SizeOf(memoryBasicInformation);
        }
        public IntPtr getProcessHandle(int processId) => Functions.OpenProcess((int)ProcessQueryInformation.PROCESS_DUMP, false, processId);
        
        public IProcessDumpMemory? ReadMemoryChunk(IntPtr processHandle, IntPtr ptrMinAddress)
        {
           
            _ = Functions.VirtualQueryEx(processHandle, ptrMinAddress, ref memoryBasicInformation, memoryBasicInformationSize);
            if (!this.memoryBasicInformation.IsValidPageForReading()) return null;
            byte[] buffer = new byte[memoryBasicInformation.RegionSize];
            _ = Functions.ReadProcessMemory(processHandle, memoryBasicInformation.BaseAddress, buffer, memoryBasicInformation.RegionSize, out _);
            return new ProcessDumpMemoryModel() 
            {
                memoryContent = buffer,
                StartAddress = memoryBasicInformation.BaseAddress,
                EndAddress = memoryBasicInformation.BaseAddress + memoryBasicInformation.RegionSize,

            };

        }
        public List<IProcessDumpMemory?> GetDump(int processId)
        {
            IntPtr processHandle = getProcessHandle(processId);
           
            long minAddress = (long)systemInfo.lpMinimumApplicationAddress;
            long maxAddress = (long)systemInfo.lpMaximumApplicationAddress;
            IntPtr ptrMinAddress = systemInfo.lpMinimumApplicationAddress;
            List<IProcessDumpMemory?> processDumpMemories = new ();

            while(minAddress < maxAddress)
            {
                processDumpMemories.Add(ReadMemoryChunk(processHandle, ptrMinAddress));
                minAddress += (long)memoryBasicInformation.RegionSize;
                ptrMinAddress = new IntPtr(minAddress);
            }
           
            return processDumpMemories;
        }
         



       
    }
}
