using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Requirement.Handler;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Micro.Future.Business.Common;

namespace Micro.Future.Common.Business.xUnit
{
    public class RequirmentTests
    {
        public RequirmentTests()
        {
            IRequirementManager manager = new RequirementManager();
            manager.AddRequirementInfo(new Commo.Business.Abstraction.BizObject.RequirementInfo());
        }

        [Fact]
        public void RegisterUsersTest()
        {
            UserInfo userA = new UserInfo();
            userA.Name = "testuserA";
            userA.Password = "111111";
            userA.Phone = "66666666";

            UserManager userManagerA = new UserManager();
            BizTResult<bool> isRegistered = userManagerA.Register(userA);
            Assert.True(isRegistered.Result);
                    
        }
    }

   
}
