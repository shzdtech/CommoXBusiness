using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.Handler
{
    public abstract class AbstractRequirementManager : IRequirementManager
    {
        public delegate void RequirementChainChangedEventHandler(IEnumerable<RequirementChainInfo> chain);
        #region IRequirementManager implements


        public abstract BizTResult<IEnumerable<RequirementInfo>> QueryAllRequirements();

        public abstract BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId);

        public abstract BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(int userId);

        public abstract BizTResult<RequirementInfo> AddRequirementInfo(RequirementInfo requirement);

        public abstract BizTResult<bool> UpdateRequirementInfo(RequirementInfo requirement);

        public abstract BizTResult<IEnumerable<RequirementChainInfo>> QueryRequirementChains(int requirementId);

        #endregion
    }
}
