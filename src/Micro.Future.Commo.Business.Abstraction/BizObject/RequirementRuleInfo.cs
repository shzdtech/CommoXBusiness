using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class RequirementRuleInfo
    {
        public int RuleId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public RequirementRuleOperation OperationType { get; set; }

        public RequirementRuleState State { get; set; }
    }
}
