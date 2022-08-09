using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessView.Business.Exceptions
{
    internal class NotFoundThreadException: Exception
    {

        public NotFoundThreadException(): base(ExceptionMessages.NOT_FOUND_THREADS_WITH_PROCESS_ID.ToString()){}
    }
}
