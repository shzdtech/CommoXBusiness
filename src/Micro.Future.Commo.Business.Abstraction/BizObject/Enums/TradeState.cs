using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    public enum TradeState
    {
        /// <summary>
        /// 合同
        /// </summary>
        Contract = 1,

        /// <summary>
        /// 资金
        /// </summary>
        Payment = 2,

        /// <summary>
        /// 货物
        /// </summary>
        Product = 3,

        /// <summary>
        /// 发票
        /// </summary>
        Invoice = 4,

        /// <summary>
        /// 尾款
        /// </summary>
        FinalPayment = 5,
    }
}
