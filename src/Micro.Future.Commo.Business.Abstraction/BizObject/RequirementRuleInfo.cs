using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 需求的撮合规则
    /// </summary>
    public class RequirementRuleInfo
    {
        /// <summary>
        /// 规则名称，比如：企业类型，注册资本
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 规则对应的值，比如：企业类型要求是国企，则value=“国企”
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 规则名称与值之间的关系，是相等，还是包含
        /// </summary>
        public RequirementRuleOperation OperationType { get; set; }
    }
}
