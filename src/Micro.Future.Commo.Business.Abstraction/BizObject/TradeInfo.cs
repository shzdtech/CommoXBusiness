using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class TradeInfo
    {
        public int TradeId { get; set; }
        /// <summary>
        /// 交易标题：方便交易信息显示，如 20160205铜交易
        /// </summary>
        public string TradeTitle { get; set; }
        /// <summary>
        /// 交易费
        /// </summary>
        public double TradeFee { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// 交易资金量
        /// </summary>
        public double TradeAmount { get; set; }
        /// <summary>
        /// 交易货物量
        /// </summary>
        public double TradeQuota { get; set; }
        /// <summary>
        /// 交易补贴量
        /// </summary>
        public double TradeSubsidy { get; set; }
        /// <summary>
        /// 参与企业数
        /// </summary>
        public int ParticipatorCount { get; set; }
        /// <summary>
        /// 当前交易的状态：如到 订单一 或 订单二
        /// 或显示  出资  出货 中间商 
        /// 数据来自requirementType
        /// </summary>
        public string CurrentState { get; set; }
        
        
        ///// <summary>
        ///// 相关的订单
        ///// </summary>
        public List<OrderInfo> Orders { get; set; }

    }
}
