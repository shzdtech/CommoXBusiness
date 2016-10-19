using Micro.Future.Commo.Business.Abstraction.BizObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 用户常用 支付信息
    /// </summary>
    public class UserPaymentInfo
    {
        public int InfoId { get; set; }

        public int EnterpriseId { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// 货款支付时间
        /// </summary>
        public string Paytime { get; set; }

        /// <summary>
        /// 交易要求
        /// </summary>
        public TradeRequirementType TradeRequirement { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public PaymentMethodType PaymentMethod { get; set; }


        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string BankName { get; set; }


        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccount { get; set; }


        /// <summary>
        /// 银行行号
        /// </summary>
        public string BankId { get; set; }


        /// <summary>
        /// 开户银行地址
        /// </summary>
        public string BankAddress { get; set; }

        /// <summary>
        /// 是否已开通电子票口
        /// </summary>
        public bool IsETicket { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }


    }
}
