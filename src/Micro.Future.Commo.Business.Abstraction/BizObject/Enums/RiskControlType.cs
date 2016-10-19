using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 支付风控，0:不需自主收付; 1:需自主收付
    /// </summary>
    public enum RiskControlType
    {
        /// <summary>
        /// 不需自主收付
        /// </summary>
        NotIndependentControl = 0,

        /// <summary>
        /// 需自主收付
        /// </summary>
        IndependentControl = 1
    }
}
