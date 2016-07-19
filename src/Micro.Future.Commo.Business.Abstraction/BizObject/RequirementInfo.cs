using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class RequirementInfo
    {
        public int RequirementId { get; set; }
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public int ProductId { get; set; }
        public int ProductPrice { get; set; }
        public decimal ProductQuota { get; set; }
      
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public RequirementState State { get; set; }

        public RequirementType Type { get; set; }

        public IEnumerable<RequirementRuleInfo> Rules { get; set; }
    }
}
