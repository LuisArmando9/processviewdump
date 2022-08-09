using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessView.Business.Interfaces
{
    internal interface IProcessDumpMemory
    {
        ulong StartAddress { get; set; }
        ulong EndAddress { get; set; }
        byte[] memoryContent { get; set; }

    }
}
