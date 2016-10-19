using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 发票面额, 1:千元版; 2:万元版; 3:十万元版; 4:百万元版; 5:千万元版
    /// </summary>
    public enum InvoiceValueType
    {
        /// <summary>
        /// 千元版
        /// </summary>
        Thousand = 1,

        /// <summary>
        /// 万元版
        /// </summary>
        TenThousand = 2,

        /// <summary>
        /// 十万元版
        /// </summary>
        HundredThousand = 3,

        /// <summary>
        /// 百万元版
        /// </summary>
        Million  = 4,

        /// <summary>
        /// 千万元版
        /// </summary>
        TenMillion = 5
    }
}
