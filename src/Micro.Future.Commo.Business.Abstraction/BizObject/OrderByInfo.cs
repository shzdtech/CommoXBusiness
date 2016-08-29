using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class OrderByInfo
    {
        /// <summary>
        /// sort field
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// asc / desc
        /// </summary>
        public string OrderBy { get; set; }
    }
}
