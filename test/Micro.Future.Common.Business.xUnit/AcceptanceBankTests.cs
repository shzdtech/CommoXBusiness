using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Micro.Future.Commo.Business.Abstraction.BizObject;

namespace Micro.Future.Common.Business.xUnit
{
    public class AcceptanceBankTests : BaseTest
    {
        private IAcceptanceBankManager _acceptanceManager = null;

        public AcceptanceBankTests()
        {
            _acceptanceManager = serviceProvider.GetService<IAcceptanceBankManager>();
        }

        [Fact]
        public void Test_QueryAllBanks()
        {
            var results = _acceptanceManager.QueryAllBanks();
        }

        [Fact]
        public void Test_AddAcceptance()
        {
            AcceptanceBankInfo acc = new AcceptanceBankInfo()
            {
                AcceptanceType = 1,
                BankName = "中国银行",
                BankPrice = 0.24,
                BankType = 1,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            var accId = _acceptanceManager.CreateBank(acc);
        }
    }
}
