using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessView.Persistence.WinApi.Enums
{
    internal enum ProcessQueryInformation: int
    {
        PROCESS_QUERY_INFORMATION = 0x0400,
        PROCESS_VM_READ = 0x010,
        PROCESS_DUMP = PROCESS_QUERY_INFORMATION | PROCESS_VM_READ
    }
}
