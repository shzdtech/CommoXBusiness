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
    }
}
