using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessView.Business.Interfaces;
namespace ProcessView.Business.Models
{
    internal class ProcessDumpMemoryModel : IProcessDumpMemory
    {
        public ulong StartAddress { get; set; }
        public ulong EndAddress { get; set; }
        public byte[] memoryContent { get; set; }
    }
}
