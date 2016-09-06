using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class RequirementSearchCriteria : SearchCriteria
    {
        public int EnterpriseId { get; set; }

        /// <summary>
        /// 需求状态
        /// </summary>
        public RequirementState? RequirementState { get; set; }

        /// <summary>
        /// 需求类型：买/卖/补贴
        /// </summary>
        public RequirementType? RequirementType { get; set; }

        /// <summary>
        /// 货物名称，
        /// </summary>
        public string ProductName { get; set; }


        /// <summary>
        /// 货物类型：有色、化工等
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 起始贸易量
        /// </summary>
        public decimal? StartTradeAmount { get; set; }

        /// <summary>
        /// 结束贸易量
        /// </summary>
        public decimal? EndTradeAmount { get; set; }
    }
}
