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
        /// 新创建/未锁定
        /// </summary>
        OPEN = 0,

        /// <summary>
        /// 已锁定
        /// </summary>
        LOCKED = 1,

        /// <summary>
        /// 已确认
        /// </summary>
        CONFIRMED = 2
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
        EQUAL = 1,
        LESS = 2,
        GREATER = 3,
        IN = 4,
        NOTIN = 5
    }
}
