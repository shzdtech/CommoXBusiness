using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 交易要求
    /// </summary>
    public enum PaymentRequirementType
    {
        /// <summary>
        /// 不限
        /// </summary>
        None = 0,

        /// <summary>
        /// 先货后款
        /// </summary>
        GoodsFirst = 2,

        /// <summary>
        /// 款到出货
        /// </summary>
        MoneyFirst = 2
    }
}
