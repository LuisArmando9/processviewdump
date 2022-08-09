using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessView.Business.Utils
{
    internal static class ReadOnlyCollectionBaseUtil
    {
        public static bool IsEmptyOrNull(this ReadOnlyCollectionBase collection) => collection == null || collection.Count == 0;
    }
}
