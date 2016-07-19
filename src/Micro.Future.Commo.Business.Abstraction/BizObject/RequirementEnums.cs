using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 需求类型，买/卖/
    /// </summary>
    public enum RequirementType
    {
        NONE = 0,
        BUY = 1,
        SALE = 2
    }

    public enum RequirementState
    {
        Normal = 1,

    }

    public enum RequirementRuleState
    {
        Normal = 1,

    }

    public enum RequirementRuleOperation
    {
        MoreThan = 1,
        Equal = 2,
        LessThan = 3
    }
}
