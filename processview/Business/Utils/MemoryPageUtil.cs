using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessView.Persistence.WinApi.Enums;
using ProcessView.Persistence.WinApi.Structures;
namespace ProcessView.Business.Utils
{
    internal static class MemoryPageUtil
    {

        public static bool IsValidPageForReading(this MEMORY_BASIC_INFORMATION64 memoryInformation)
            => memoryInformation.Protect == (int)AllocationProtect.PAGE_READWRITE ||
            memoryInformation.Protect == (int)AllocationProtect.MEM_COMMIT;    
    }
}
