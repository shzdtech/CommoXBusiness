using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Xunit;

namespace Micro.Future.Common.Business.xUnit
{
    public class FinancialProductTests : BaseTest
    {
        private IFinancialProductManager _financialManager = null;
        public FinancialProductTests()
        {
            _financialManager = serviceProvider.GetService<IFinancialProductManager>();
        }



        [Fact]
        public void Test_QueryAllProduts()
        {
            var results = _financialManager.QueryAllFinancialProducts();
        }
    }
}
