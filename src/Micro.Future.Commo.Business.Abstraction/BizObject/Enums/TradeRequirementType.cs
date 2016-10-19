using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 交易要求， 0=不限， 1=先货后款， 2=款到出货
    /// </summary>
    public enum TradeRequirementType
    {
        /// <summary>
        /// 不限
        /// </summary>
        None = 0,

        /// <summary>
        /// 先货后款
        /// </summary>
        GoodsFirst = 1,

        /// <summary>
        /// 款到出货
        /// </summary>
        MoneyFirst = 2
    }
}
