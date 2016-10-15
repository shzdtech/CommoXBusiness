using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 理财产品
    /// </summary>
    public class FinancialProductInfo
    {
        /// <summary>
        /// 编号/主键
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 银行所在地
        /// </summary>
        public string BankAddress { get; set; }

        /// <summary>
        /// 产品期限(单位 天)
        /// </summary>
        public int ProductTerm { get; set; }

        /// <summary>
        /// 年华收益率，单位%
        /// </summary>
        public double ProductYield { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后一次修改事件
        /// </summary>
        public DateTime UpdatedTime { get; set; }
    }
}
