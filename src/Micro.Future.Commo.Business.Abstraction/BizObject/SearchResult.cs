using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class SearchResult<T>
    {
        public int TotalCount { get; set; }

        public IList<T> Result { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}
