using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class SearchCriteria
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }
        
        public IList<OrderByInfo> OrderByFields { get; set; }
    }
}
