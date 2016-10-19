using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 用户常用 发票信息
    /// </summary>
    public class UserInvoiceInfo
    {
        public int InfoId { get; set; }

        public int EnterpriseId { get; set; }

        public string UserId { get; set; }


        /// <summary>
        /// 发票面额
        /// </summary>
        public string InvoiceValue { get; set; }


        /// <summary>
        /// 开票量
        /// </summary>
        public string InvoiceAmount { get; set; }


        /// <summary>
        /// 开票要求
        /// </summary>
        public string InvoiceRequirement { get; set; }


        /// <summary>
        /// 发票开具时间
        /// </summary>
        public string InvoiceIssueDateTime { get; set; }

        /// <summary>
        /// 发票交接方式
        /// </summary>
        public string InvoiceTransferMode { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
