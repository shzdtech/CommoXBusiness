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
        event Action<IList<RequirementChainInfo>> OnChainChanged;

        BizTResult<IEnumerable<RequirementInfo>> QueryAllRequirements();

        BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(string userId);

        BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId);

        BizTResult<RequirementInfo> AddRequirementInfo(RequirementInfo requirement);

        BizTResult<IEnumerable<RequirementChainInfo>> QueryRequirementChains(int requirementId);
    }
}
