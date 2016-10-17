using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PaymentMethodType
    {
        /// <summary>
        /// 电汇（现金）
        /// </summary>
        Cash = 1,

        /// <summary>
        /// 银行电子汇票
        /// </summary>
        BankEDraft = 2,

        /// <summary>
        /// 信用证 
        /// </summary>
        CreditLetter = 3,

        /// <summary>
        /// 商业承兑汇票
        /// </summary>
        Acceptance = 4,
    }
}
