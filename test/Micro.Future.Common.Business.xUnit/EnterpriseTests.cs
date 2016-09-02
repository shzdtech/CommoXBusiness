using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Future.Common.Business.xUnit
{
    public class EnterpriseTests : BaseTest
    {
        private IEnterpriseManager _enterpriseManager = null;
        public EnterpriseTests()
        {
            _enterpriseManager = serviceProvider.GetService<IEnterpriseManager>();
        }


        [Fact]
        public void Test_QueryEnterpriseInfo()
        {
            int enterpriseId = 100;
            var enterpriseInfo =  _enterpriseManager.QueryEnterpriseInfo(enterpriseId);

            Assert.NotNull(enterpriseInfo);
        }

        [Fact]
        public void Test_UpdateEnterpriseInfo()
        {
            int enterpriseId = 100;
            var enterpriseInfo = _enterpriseManager.QueryEnterpriseInfo(enterpriseId);
            enterpriseInfo.Address = "111111";

            var result =  _enterpriseManager.UpdateEnterprise(enterpriseInfo);

            Assert.True(result.Result);

            enterpriseInfo = _enterpriseManager.QueryEnterpriseInfo(enterpriseId);

            Assert.Equal<string>("111111", enterpriseInfo.Address);
        }

        [Fact]
        public void Test_UpdateEnterpriseState()
        {
            int enterpriseId = 100;
            var result = _enterpriseManager.UpdateEnterpriseState(enterpriseId, Commo.Business.Abstraction.BizObject.EnterpriseStateType.REJECTED);

            var enterpriseInfo = _enterpriseManager.QueryEnterpriseInfo(enterpriseId);

            Assert.Equal<Commo.Business.Abstraction.BizObject.EnterpriseStateType>(Commo.Business.Abstraction.BizObject.EnterpriseStateType.REJECTED, enterpriseInfo.EnterpriseState);
        }
    }
}
