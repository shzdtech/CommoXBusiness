using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IRequirementManager
    {
        BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(int userId);

        BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId);

        BizTResult<bool> SaveRequirement(RequirementInfo requirement);
    }
}
