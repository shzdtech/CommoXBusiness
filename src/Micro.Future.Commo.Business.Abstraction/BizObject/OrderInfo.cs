using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class OrderInfo
    {
        public int OrderId { get; set; }

        /// <summary>
        /// 关联交易
        /// </summary>
        public int TradeId { get; set; }
        /// <summary>
        /// 在本次交易链中所在的次序，如TradeChain：A-B-C-D， B的sequence为2
        /// </summary>
        public int TradeSequence { get; set; }
        /// <summary>
        /// 关联用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 关联用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 关联企业ID
        /// </summary>
        public int EnterpriseId { get; set; }
        /// <summary>
        /// 关联企业名
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 关联需求ID
        /// </summary>
        public int RequirementId { get; set; }
        /// <summary>
        /// 需求类型:出资；出货；贸易量
        /// </summary>
        public string RequirementType { get; set; }
        /// <summary>
        /// 需求具体的条件：包装成一个字段（此字段仅供查询，不可修改）
        /// 如产品=铜；价格=190；企业=国企
        /// </summary>
        public string RequirementFilters { get; set; }
        /// <summary>
        /// 需求具体备注
        /// </summary>
        public string RequirementRemarks { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 订单完成时间
        /// </summary>
        public DateTime CompleteTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public String OrderState { get; set; }

        /// <summary>
        /// 执行者用户名
        /// </summary>
        public string ExecuteUsername { get; set; }

    }
}
