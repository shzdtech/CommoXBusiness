using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Requirement.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Common.Business.xUnit
{
    public class RequirmentTests
    {
        public RequirmentTests()
        {
            IRequirementManager manager = new RequirementManager();
            manager.AddRequirementInfo(new Commo.Business.Abstraction.BizObject.RequirementInfo());
        }
    }
}
