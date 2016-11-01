using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class AcceptanceBillInfo
    {
        public int AcceptanceBillId { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public int EnterpriseId { get; set; }
        /// <summary>
        /// 出票企业
        /// </summary>
        public string DrawerName { get; set; }
        /// <summary>
        /// 出票账号
        /// </summary>
        public string DrawerAccount { get; set; }
        /// <summary>
        /// 出票银行Id
        /// </summary>
        public int DrawerBankId { get; set; }
        /// <summary>
        /// 收款企业
        /// </summary>
        public string PayeeName { get; set; }
        /// <summary>
        /// 收款账号
        /// </summary>
        public string PayeeAccount { get; set; }
        /// <summary>
        /// 收款银行Id
        /// </summary>
        public int PayeeBankId { get; set; }
        /// <summary>
        /// 出票金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 汇票到期日
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 承兑协议编号
        /// </summary>
        public string AgreementNumber { get; set; }
        /// <summary>
        /// 出票日期
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
