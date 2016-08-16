using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 需求类型，买/卖/补贴
    /// </summary>
    public enum RequirementType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 出资
        /// </summary>
        Buy = 1,

        /// <summary>
        /// 出货
        /// </summary>
        Sale = 2,

        /// <summary>
        /// 补贴
        /// </summary>
        Subsidy = 3
    }

    /// <summary>
    /// 需求的状态，
    /// </summary>
    public enum RequirementState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 2,

        /// <summary>
        /// 撮合中
        /// </summary>
        Mapping = 3
    }

    /// <summary>
    /// 需求撮合规则的状态
    /// </summary>
    public enum RequirementRuleState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 2
    }

    /// <summary>
    /// 需求撮合规则选项
    /// </summary>
    public enum RequirementRuleOperation
    {
        /// <summary>
        /// 
        /// </summary>
        MoreThan = 1,

        /// <summary>
        /// 
        /// </summary>
        Equal = 2,

        /// <summary>
        /// 
        /// </summary>
        LessThan = 3
    }
}
