using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 子订单状态
    /// </summary>
    public enum OrderStateType
    {
        /// <summary>
        /// 未签合同
        /// </summary>
        Contract = 1,

        /// <summary>
        /// 用户已签合同
        /// </summary>
        UserContract = 6,

        /// <summary>
        /// 管理员确认已签合同
        /// </summary>
        ContractConfirmed = 7,


        /// <summary>
        /// 资金
        /// </summary>
        Payment = 2,

        /// <summary>
        /// 资金已确认
        /// </summary>
        PaymentConfirmed = 11,

        /// <summary>
        /// 货物
        /// </summary>
        Product = 3,

        /// <summary>
        /// 货物已确认
        /// </summary>
        ProductConfirmed = 12,

        /// <summary>
        /// 发票
        /// </summary>
        Invoice = 4,

        /// <summary>
        /// 用户已开具发票
        /// </summary>
        UserInvoice = 8,

        /// <summary>
        /// 管理员确认用户已开具发票
        /// </summary>
        InvoiceConfirmed = 9,

        /// <summary>
        /// 尾款
        /// </summary>
        FinalPayment = 5,

        /// <summary>
        /// 尾款已确认
        /// </summary>
        FinalPaymentConfirmed = 13,

        /// <summary>
        /// 交易完成
        /// </summary>
        Finished = 10
    }
}
